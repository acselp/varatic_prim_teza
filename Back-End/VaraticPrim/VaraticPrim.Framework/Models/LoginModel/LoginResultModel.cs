namespace VaraticPrim.Framework.Models.LoginModel;

public class LoginResultModel
{
    public string    AccessToken                { get; set; }
    public string    RefreshToken               { get; set; }
    public string    TokenType                  { get; set; }
    public DateTime  ExpiresIn                  { get; set; }
    public DateTime? RefreshTokenExpirationTime { get; set; }
}