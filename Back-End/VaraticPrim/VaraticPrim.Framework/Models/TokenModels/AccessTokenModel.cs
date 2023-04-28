using System.Globalization;

namespace VaraticPrim.Framework.Models;

public class AccessTokenModel
{
    public DateTime RefreshTokenExpirationTime { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}