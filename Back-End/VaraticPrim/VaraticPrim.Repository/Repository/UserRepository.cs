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

    public async Task<UserEntity?> GetByEmail(string email)
    {
        return await Table.FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim());
    }

    public async Task<bool> EmailExists(string email)
    {
        return Table.Any(it => it.Email == email.ToLower().Trim());
    }
}