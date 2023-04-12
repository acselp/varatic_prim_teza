namespace VaraticPrim.JwtAuth;

public class JwtConfiguration
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpirationTime { get; set; }
}