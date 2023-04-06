using VaraticPrim.Models.ContactModels;

namespace VaraticPrim.Models.UserModels;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ContactModel Contact { get; set; }
}