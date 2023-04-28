using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VaraticPrim.Framework.Models;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.JwtAuth;
using VaraticPrim.Service.Authentication;

namespace VaraticPrim.Framework.TokenGenerator;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly IOptions<JwtConfiguration> _options;

    public TokenGeneratorService(IOptions<JwtConfiguration> options)
    {
        _options = options;
    }
    
    public AccessTokenModel GenerateAccessToken(UserModel user)
    {
        var securityKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimsTypes.UserId, user.Id.ToString()),
        };

        var expirationTime = DateTime.Now.AddMinutes(_options.Value.AccessTokenExpirationTimeMin);
        var token = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            expires: expirationTime,
            signingCredentials: credentials);

        return new AccessTokenModel()
        {
            RefreshTokenExpirationTime = expirationTime,
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}