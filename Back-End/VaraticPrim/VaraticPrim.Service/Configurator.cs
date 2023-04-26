using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Service.Interfaces;
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
}