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
    
    public UserEntity GetUser(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<UserEntity> GetAllUsers()
    {
        return _context.Users;
    }

    public UserEntity Add(UserEntity user)
    {
        _context.Add(user);
        _context.SaveChanges();

        return user;
    }

    public UserEntity Update(UserEntity userChanges)
    {
        var user = _context.Users.Attach(userChanges);
        user.State = EntityState.Modified;
        _context.SaveChanges();

        return userChanges;
    }

    public UserEntity Delete(int id)
    {
        UserEntity user = _context.Users.Find(id);

        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        return user;
    }
}