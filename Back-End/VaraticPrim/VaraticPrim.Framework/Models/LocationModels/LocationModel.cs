using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Framework.Models.LocationModels;

public class LocationModel
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Bloc { get; set; }
    public string Apartment { get; set; }
    public int NrPers { get; set; }
    public virtual UserModel User { get; set; }
}