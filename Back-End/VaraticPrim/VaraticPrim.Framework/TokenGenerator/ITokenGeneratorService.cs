using VaraticPrim.Framework.Models.TokenModels;

namespace VaraticPrim.Framework.TokenGenerator;

public interface ITokenGeneratorService
{
    AccessTokenModel GenerateAccessToken(int userId);
    RefreshToken GenerateRefreshToken();
}