namespace VaraticPrim.Framework.Models.TokenModels;

public class RefreshToken
{
    public string   Token   { get; set; }
    public DateTime Expires { get; set; }
}