using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Repository;

public interface IRefreshTokenRepository : IGenericRepository<RefreshTokenEntity>
{
    Task<RefreshTokenEntity?> GetByEmail(string email);
}