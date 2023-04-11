using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models.UserModels;
using VaraticPrim.Service.Services;
using VaraticPrim.Service.Validation;

namespace VaraticPrim.Service;

public static class Configurator
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IValidator<UserCreateModel>, UserCreateModelValidator>();
    }
}