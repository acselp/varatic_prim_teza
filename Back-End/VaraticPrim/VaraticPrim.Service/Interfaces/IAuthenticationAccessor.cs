using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Service.Authentication.Interfaces;

public interface IAuthenticationAccessor
{
    Task<UserEntity?> LoggedIdentity();
}