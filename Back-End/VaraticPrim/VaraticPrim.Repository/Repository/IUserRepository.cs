using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Repository;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    public Task<UserEntity?> GetByEmail(string email);
}