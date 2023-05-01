using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Service.Interfaces;

public interface IAuthenticationAccessor
{
    Task<UserEntity?> LoggedIdentity();
}