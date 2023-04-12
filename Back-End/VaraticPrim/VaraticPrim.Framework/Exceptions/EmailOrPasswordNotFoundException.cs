namespace VaraticPrim.Framework.Exceptions;

public class EmailOrPasswordNotFoundException : Exception
{
    public EmailOrPasswordNotFoundException(string message) : base(message)
    {
    }
}