using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Framework.AutoMapperProfiles;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Framework.TokenGenerator;
using VaraticPrim.Framework.Validation;

namespace VaraticPrim.Framework;

public static class Configurator 
{
    public static void AddFramework(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(Configurator));
        serviceCollection.AddScoped<AuthenticationManager>();
        serviceCollection.AddScoped<UserManager>();
        serviceCollection.AddScoped<LocationManager>();
        serviceCollection.AddScoped<CounterManager>();
        serviceCollection.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        
        //Validators
        serviceCollection.AddValidatorsFromAssemblyContaining<UserCreateModelValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<UserUpdateModelValidator>();
        
        serviceCollection.AddValidatorsFromAssemblyContaining<LocationCreateModelValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<LocationUpdateModelValidator>();
        
        serviceCollection.AddValidatorsFromAssemblyContaining<CounterCreateModelValidator>();
        serviceCollection.AddValidatorsFromAssemblyContaining<CounterUpdateModelValidator>();
    }
}