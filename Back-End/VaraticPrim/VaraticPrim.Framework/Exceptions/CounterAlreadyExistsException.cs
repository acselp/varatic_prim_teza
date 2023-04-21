namespace VaraticPrim.Framework.Exceptions;

public class CounterAlreadyExistsException : Exception
{
    public CounterAlreadyExistsException(string message) : base(message)
    {
    }
}