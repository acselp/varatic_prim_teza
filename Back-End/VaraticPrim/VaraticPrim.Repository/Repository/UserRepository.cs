using Microsoft.EntityFrameworkCore;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim.Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public User GetUser(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users;
    }

    public User Add(User user)
    {
        _context.Add(user);
        _context.SaveChanges();

        return user;
    }

    public User Update(User userChanges)
    {
        var user = _context.Users.Attach(userChanges);
        user.State = EntityState.Modified;
        _context.SaveChanges();

        return userChanges;
    }

    public User Delete(int id)
    {
        User user = _context.Users.Find(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        return user;
    }
}