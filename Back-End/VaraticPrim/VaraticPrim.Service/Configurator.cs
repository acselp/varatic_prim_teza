using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Migrations;
using VaraticPrim.Service.Services;

namespace VaraticPrim.Service;

public static class Configurator
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        serviceCollection.AddScoped<IAuthenticationAccessor, HttpAuthenticationAccessor>();
        serviceCollection.AddScoped<IHashService, HashService>();
    }
    
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