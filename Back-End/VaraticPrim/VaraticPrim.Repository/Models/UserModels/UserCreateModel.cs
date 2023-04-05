namespace VaraticPrim.Repository.Models.UserModels;

public class UserCreateModel : BaseUserModel
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int ContactId { get; set; }
    public int RoleId { get; set; }
}