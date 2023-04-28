namespace VaraticPrim.Domain.Entities;

public class RefreshTokenEntity : BaseEntity
{
    public         int        UserId         { get; set; }
    public virtual UserEntity UserEntity     { get; set; }
    public         string     RefreshToken   { get; set; }
    public         DateTime   ExpirationTime { get; set; }
}