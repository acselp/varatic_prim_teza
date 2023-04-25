using VaraticPrim.Framework.Models.LocationModels;

namespace VaraticPrim.Framework.Models.CounterModels;

public class CounterModel
{
    public int Id { get; set; }
    public string Barcode { get; set; }
    public int Value { get; set; }
    public int LocationId { get; set; }
    public virtual LocationModel Location { get; set; }
}