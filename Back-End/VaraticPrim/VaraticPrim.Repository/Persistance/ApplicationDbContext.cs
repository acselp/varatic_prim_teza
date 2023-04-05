using Microsoft.EntityFrameworkCore;
using VaraticPrim;
using VaraticPrim.Domain.Entity.Configurations;

namespace VaraticPrim.Repository.Persistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}