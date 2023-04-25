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
        serviceCollection.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        serviceCollection.AddValidation();
        serviceCollection.AddManagers();
    }
}