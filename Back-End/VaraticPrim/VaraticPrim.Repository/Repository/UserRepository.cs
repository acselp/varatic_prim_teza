using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;

namespace VaraticPrim.Repository.Repository;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public new async Task Update(UserEntity entity)
    {
        await base.Update(entity);
    }

    public new async Task Insert(UserEntity entity)
    {
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