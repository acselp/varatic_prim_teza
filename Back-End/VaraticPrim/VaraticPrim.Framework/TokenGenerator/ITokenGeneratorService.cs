using VaraticPrim.Framework.Models;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Framework.TokenGenerator;

public interface ITokenGeneratorService
{
    public AccessTokenModel Generate(UserModel user);
}