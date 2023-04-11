using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Interfaces;

public interface ITokenGeneratorService
{
    public string Generate(UserModel user);
}