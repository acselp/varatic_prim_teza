using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Framework.Validation;

namespace VaraticPrim.Framework;

public static class Configurator 
{
    public static void AddFramework(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IValidator<UserCreateModel>, UserCreateModelValidator>();
    }
}