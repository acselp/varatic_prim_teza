using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace VaraticPrim.Email.Gmail;

public class GmailProvider : IMailProvider
{
    private readonly IOptions<EmailOptions> _options;
    private SmtpClient Client { get; set; }
    
    public GmailProvider(IOptions<EmailOptions> options)
    {
        _options = options;
        Client = new SmtpClient();
    }

    public SmtpClient GetSmtpClient()
    {
        return new SmtpClient()
        {
            Host = _options.Value.Host,
            Port = _options.Value.Port,
            EnableSsl = _options.Value.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = _options.Value.UseDefaultCredentials,
            Credentials = new NetworkCredential()
            {
                UserName = _options.Value.UserName,
                Password = _options.Value.Password
            }
        };
    }
    
}