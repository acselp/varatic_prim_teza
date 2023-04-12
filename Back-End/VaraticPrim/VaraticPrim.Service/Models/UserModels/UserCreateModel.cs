using VaraticPrim.Service.Models.ContactModels;

namespace VaraticPrim.Service.Models.UserModels;

public class UserCreateModel
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ContactCreateModel Contact { get; set; }
}