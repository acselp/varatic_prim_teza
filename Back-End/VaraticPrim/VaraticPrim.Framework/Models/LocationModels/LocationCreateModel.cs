namespace VaraticPrim.Framework.Models.LocationModels;

public class LocationCreateModel
{
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string Bloc { get; set; }
    public string Apartment { get; set; }
    public int NrPers { get; set; }
    public int UserId { get; set; }
}