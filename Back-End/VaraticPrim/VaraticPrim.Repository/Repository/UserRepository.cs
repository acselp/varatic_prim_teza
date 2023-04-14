using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim.Repository.Repository;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public new async Task Update(UserEntity entity)
    {
        entity.Contact.CreatedOnUtc = DateTime.UtcNow;
        entity.Contact.UpdatedOnUtc = DateTime.UtcNow;
        await base.Update(entity);
    }

    public new async Task Insert(UserEntity entity)
    {
        entity.Contact.CreatedOnUtc = DateTime.UtcNow;
        entity.Contact.UpdatedOnUtc = DateTime.UtcNow;
        await base.Insert(entity);
    }

    public async Task<UserEntity?> GetByEmail(string email)
    {
        return await Table.FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim());
    }

    public async Task<bool> EmailExists(string email)
    {
        return await Table.AnyAsync(it => it.Email == email.ToLower().Trim());
    }
}