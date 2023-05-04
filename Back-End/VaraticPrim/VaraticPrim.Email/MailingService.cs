using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace VaraticPrim.Email;

public class MailingService : IMailingService
{
    private IMailProvider _mailProvider;
    private IOptions<EmailOptions> _options;

    public MailingService(IMailProvider mailProvider, IOptions<EmailOptions> options)
    {
        _mailProvider = mailProvider;
        _options = options;
    }

    public string SendEmail(string email, string subject, string body)
    {
        var client = _mailProvider.GetSmtpClient();
        
        var fromEmail = new MailAddress(_options.Value.UserName, "Virgiliu Plesca");
        var toEmail = new MailAddress(email, "Vasile");
        
        var mailMessage = new MailMessage()
        {
            From = fromEmail,
            Subject = subject,
            Body = body
        };
        mailMessage.To.Add(toEmail);

        try
        {
            client.Send(mailMessage);
            
            return "Successfuly sent email";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}