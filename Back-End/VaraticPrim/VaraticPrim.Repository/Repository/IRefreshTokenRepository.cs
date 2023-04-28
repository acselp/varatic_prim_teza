using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Repository;

public interface IRefreshTokenRepository : IGenericRepository<RefreshTokenEntity>
{
    Task<RefreshTokenEntity?> GetUserByToken(string token);
}