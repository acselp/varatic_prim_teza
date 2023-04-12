using VaraticPrim.Framework.Models;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Service.Interfaces;

public interface ITokenGeneratorService
{
    public AccessTokenModel Generate(UserModel user);
}