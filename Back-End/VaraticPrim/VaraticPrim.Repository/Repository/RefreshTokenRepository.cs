using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;

namespace VaraticPrim.Repository.Repository;

public class RefreshTokenRepository : GenericRepository<RefreshTokenEntity>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
    }

}