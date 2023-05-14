using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.TokenGenerator;
using VaraticPrim.Framework.Validation;

namespace VaraticPrim.Framework.Managers;

public static class Configurator 
{
    public static void AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<AuthenticationManager>();
        serviceCollection.AddScoped<UserManager>();
        serviceCollection.AddScoped<LocationManager>();
        serviceCollection.AddScoped<CounterManager>();
        serviceCollection.AddScoped<ServiceManager>();
    }
}