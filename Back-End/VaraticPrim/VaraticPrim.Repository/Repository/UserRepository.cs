using Microsoft.EntityFrameworkCore;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim.Repository.Repository;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}