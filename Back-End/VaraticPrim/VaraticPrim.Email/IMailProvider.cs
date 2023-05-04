using System.Net.Mail;

namespace VaraticPrim.Email;

public interface IMailProvider
{
    SmtpClient GetSmtpClient();
}