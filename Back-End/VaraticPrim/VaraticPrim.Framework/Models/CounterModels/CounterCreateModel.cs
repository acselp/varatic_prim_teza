using VaraticPrim.Framework.Models.LocationModels;

namespace VaraticPrim.Framework.Models.CounterModels;

public class CounterCreateModel
{
    public string Barcode { get; set; }
    public int LocationId { get; set; }
}