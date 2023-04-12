using VaraticPrim.Service.Models;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Authentication;

public interface ITokenGeneratorService
{
    public AccessTokenModel Generate(UserModel user);
}