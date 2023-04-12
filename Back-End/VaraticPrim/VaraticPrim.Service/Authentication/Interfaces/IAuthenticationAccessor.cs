using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Service.Authentication;

public interface IAuthenticationAccessor
{
    Task<UserEntity?> LoggedIdentity();
}