using Microsoft.Extensions.DependencyInjection;
using VaraticPrim.Email.Gmail;

namespace VaraticPrim.Email;

public static class Configurator
{
    public static void AddGmail(this IServiceCollection services)
    {
        services.AddSingleton<IMailProvider, GmailProvider>();
    }
}