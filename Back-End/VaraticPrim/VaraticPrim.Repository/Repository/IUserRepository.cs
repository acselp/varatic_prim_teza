using VaraticPrim.Repository.Entity;

namespace VaraticPrim.Repository.Repository;

public interface IUserRepository
{
    public UserEntity GetUser(int id);
    public IEnumerable<UserEntity> GetAllUsers();
    public UserEntity Add(UserEntity user);
    public UserEntity Update(UserEntity user);
    public UserEntity Delete(int id);
}