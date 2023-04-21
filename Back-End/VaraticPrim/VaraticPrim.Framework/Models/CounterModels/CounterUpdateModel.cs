using VaraticPrim.Framework.Models.LocationModels;

namespace VaraticPrim.Framework.Models.CounterModels;

public class CounterUpdateModel
{
    public string BarCode { get; set; }
    public int Value { get; set; }
    public int LocationId { get; set; }
}