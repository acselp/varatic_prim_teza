using VaraticPrim.Service.Models;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Interfaces;

public interface ITokenGeneratorService
{
    public AccessTokenModel Generate(UserModel user);
}