using Microsoft.EntityFrameworkCore;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Entity.Configurations;

namespace VaraticPrim.Repository.Persistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}