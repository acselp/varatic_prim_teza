using Microsoft.Extensions.DependencyInjection;

namespace VaraticPrim.Email;

public static class Configurator
{
    public static void AddMailing(this IServiceCollection services)
    {
        services.AddSingleton<IMailingService, MailingService>();
    }
}