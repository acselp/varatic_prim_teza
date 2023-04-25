using VaraticPrim.Framework.Models.ContactModels;

namespace VaraticPrim.Framework.Models.UserModels;

public class LocationUpdateModel
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Bloc { get; set; }
    public string Apartment { get; set; }
    public int NrPers { get; set; }
    public int UserId { get; set; }
}