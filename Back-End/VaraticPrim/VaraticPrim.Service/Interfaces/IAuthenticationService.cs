using VaraticPrim.Service.Models.LoginModel;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Interfaces;

public interface IAuthenticationService
{
    public Task<UserModel> Authenticate(LoginModel loginModel);
}