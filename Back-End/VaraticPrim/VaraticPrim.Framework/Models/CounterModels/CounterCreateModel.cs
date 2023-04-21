using VaraticPrim.Framework.Models.LocationModels;

namespace VaraticPrim.Framework.Models.CounterModels;

public class CounterCreateModel
{
    public string BarCode { get; set; }
    public int Value { get; set; }
    public int LocationId { get; set; }
    public virtual LocationModel Location { get; set; }
}