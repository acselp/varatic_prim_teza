using VaraticPrim.Repository.Entity;

namespace VaraticPrim.Repository.Repository;

public interface IUserRepository
{
    public User GetUser(int id);
    public IEnumerable<User> GetAllUsers();
    public User Add(User user);
    public User Update(User user);
    public User Delete(int id);
}