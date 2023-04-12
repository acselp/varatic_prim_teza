using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Service.Authentication;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models.UserModels;
using VaraticPrim.Service.Validation;

namespace VaraticPrim.Service;

public static class Configurator
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        serviceCollection.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        serviceCollection.AddScoped<IAuthenticationAccessor, HttpAuthenticationAccessor>();
        serviceCollection.AddScoped<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddScoped<IValidator<UserCreateModel>, UserCreateModelValidator>();
    }
}