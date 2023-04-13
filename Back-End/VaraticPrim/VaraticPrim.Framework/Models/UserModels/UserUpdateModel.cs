using VaraticPrim.Framework.Models.ContactModels;

namespace VaraticPrim.Framework.Models.UserModels;

public class UserUpdateModel
{
    public string Email { get; set; }
    public ContactModel Contact { get; set; }
}