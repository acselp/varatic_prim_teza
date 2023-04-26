using Microsoft.Extensions.DependencyInjection;

namespace Infeastructure.Migrations.Evolve;

public static class Configurator
{
    public static void AddMigrations(this IServiceCollection services, string? connectionString)
    {
        services.AddSingleton<IMigrationService>(new MigrationService(connectionString));
        using (ServiceProvider serviceProvider = services.BuildServiceProvider())
        {
            var migrationService = serviceProvider.GetRequiredService<IMigrationService>();
            migrationService.Migrate();
        }
    }
}