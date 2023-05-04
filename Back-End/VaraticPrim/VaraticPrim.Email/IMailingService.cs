namespace VaraticPrim.Email;

public interface IMailingService
{
    string SendEmail(string ToEmail, string Subject, string Body);
}