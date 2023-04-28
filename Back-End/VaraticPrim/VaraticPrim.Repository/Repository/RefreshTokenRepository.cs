using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim.Repository.Repository;

public class RefreshTokenRepository : GenericRepository<RefreshTokenEntity>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
    }

    public new async Task Insert(RefreshTokenEntity entity)
    {
        entity.CreatedOnUtc = DateTime.UtcNow;
        entity.UpdatedOnUtc = DateTime.UtcNow;
        await base.Insert(entity);
    }
    
    public new async Task Update(RefreshTokenEntity entity)
    {
        entity.UpdatedOnUtc = DateTime.UtcNow;
        await base.Update(entity);
    }

    public async Task<RefreshTokenEntity?> GetByEmail(string email)
    {
        return await Table.FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim());
    }
}