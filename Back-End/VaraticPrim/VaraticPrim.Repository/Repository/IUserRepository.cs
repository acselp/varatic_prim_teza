using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Paged;

namespace VaraticPrim.Repository.Repository;

public interface IUserRepository : IGenericRepository<UserEntity>
{
    public Task<UserEntity?> GetByEmail(string email);
    public Task<bool> EmailExists(string email);
    Task<PagedList<UserEntity>> GetAll(UserFilter filter);
}