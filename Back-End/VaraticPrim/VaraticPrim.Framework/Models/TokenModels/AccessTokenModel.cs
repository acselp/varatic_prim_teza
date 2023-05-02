namespace VaraticPrim.Framework.Models.TokenModels;

public class AccessTokenModel
{
    public DateTime ExpirationTime { get; set; }
    public string   AccessToken    { get; set; }
    public string   TokenType      { get; set; }
}