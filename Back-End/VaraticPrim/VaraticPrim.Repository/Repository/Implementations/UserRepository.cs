using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Paged;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Repository.Repository.Implementations;

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

    public async Task<PagedList<UserEntity>> GetAll(UserFilter filter)
    {
        var pagedAsync = await Table.ToPagedAsync(filter.PageIndex, filter.PageSize);
        return pagedAsync;
    }
}