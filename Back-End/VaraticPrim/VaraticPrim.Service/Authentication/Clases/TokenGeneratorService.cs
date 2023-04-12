using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VaraticPrim.JwtAuth;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Authentication;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IOptions<JwtConfiguration> _options;

    public TokenGeneratorService(IOptions<JwtConfiguration> options)
    {
        _options = options;
    }
    
    public AccessTokenModel Generate(UserModel user)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimsTypes.UserId, user.Id.ToString()),
        };

        var expirationTime = DateTime.Now.AddMinutes(_options.Value.ExpirationTime);
        var token = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            expires: expirationTime,
            signingCredentials: credentials);

        return new AccessTokenModel()
        {
            TokenExpirationTime = expirationTime,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }
}