using Infrastructure.Migrations.Evolve.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Migrations.Evolve;

public static class Configurator
{
    public static void AddMigrations(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IMigrationService>(new MigrationService(connectionString));
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var migrationService = serviceProvider.GetRequiredService<IMigrationService>();
            migrationService.Migrate();
        }
    }
}