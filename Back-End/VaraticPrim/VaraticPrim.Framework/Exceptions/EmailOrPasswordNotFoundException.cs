namespace VaraticPrim.Service.Exceptions;

public class EmailOrPasswordNotFoundException : Exception
{
    public EmailOrPasswordNotFoundException(string message) : base(message)
    {
    }
}