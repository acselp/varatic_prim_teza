namespace VaraticPrim.Framework.Exceptions;

public class InvalidAccessTokenOrRefreshTokenException : Exception
{
    public InvalidAccessTokenOrRefreshTokenException(string message) : base(message)
    {
    }
}