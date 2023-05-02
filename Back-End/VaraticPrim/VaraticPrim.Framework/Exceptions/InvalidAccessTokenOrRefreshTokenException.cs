namespace VaraticPrim.Framework.Exceptions;

public class InvalidAccessTokenOrRefreshTokenException : Exception
{
    public InvalidAccessTokenOrRefreshTokenException()
    {
    }
    public InvalidAccessTokenOrRefreshTokenException(string message) : base(message)
    {
    }
}