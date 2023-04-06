using VaraticPrim.Models.ContactModels;

namespace VaraticPrim.Models.UserModels;

public class UserCreateModel
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ContactCreateModel Contact { get; set; }
}