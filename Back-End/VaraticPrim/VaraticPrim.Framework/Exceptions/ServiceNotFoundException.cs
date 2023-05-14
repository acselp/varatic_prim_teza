namespace VaraticPrim.Framework.Exceptions;

public class ServiceNotFoundException : Exception
{
    public ServiceNotFoundException(string message) : base(message)
    {
    }
}