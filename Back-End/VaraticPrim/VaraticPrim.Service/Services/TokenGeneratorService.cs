using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VaraticPrim.JwtAuth;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Services;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IOptions<JwtConfiguration> _options;

    public TokenGeneratorService(IOptions<JwtConfiguration> options)
    {
        _options = options;
    }
    
    public string Generate(UserModel user)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Email),
        };

        var token = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}