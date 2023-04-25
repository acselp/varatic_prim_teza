using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Framework.Validation;

namespace VaraticPrim.Framework.Validation;

public static class Configurator 
{
    public static void AddValidation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssemblyContaining<UserCreateModelValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<UserUpdateModelValidator>();
        
        serviceCollection.AddValidatorsFromAssemblyContaining<LocationCreateModelValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<LocationUpdateModelValidator>();
        
        serviceCollection.AddValidatorsFromAssemblyContaining<CounterCreateModelValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<CounterUpdateModelValidator>();
    }
}