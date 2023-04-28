namespace VaraticPrim.Domain.Entity;

public class RefreshTokenEntity : BaseEntity
{
    public string Email { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpirationTime { get; set; }
}