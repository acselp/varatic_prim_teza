using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Service.Interfaces;

public interface IAuthenticationAccessor
{
    Task<UserEntity?> LoggedIdentity();
}