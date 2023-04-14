using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VaraticPrim.Repository.Persistance.Configurations;

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
        // modelBuilder.ApplyUtcDateTimeConverter();
    }
}