using VaraticPrim.Framework.Models.ContactModels;

namespace VaraticPrim.Framework.Models.UserModels;

public class UserModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public ContactModel Contact { get; set; }
}