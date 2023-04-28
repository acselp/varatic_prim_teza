using System.Security.Claims;
using VaraticPrim.Framework.Models;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Framework.TokenGenerator;

public interface ITokenGeneratorService
{
    public AccessTokenModel GenerateAccessToken(UserModel user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}