using Microsoft.EntityFrameworkCore;
using VaraticPrim.Repository.Entity;

namespace VaraticPrim.Repository.Persistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}