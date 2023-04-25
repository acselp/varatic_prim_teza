namespace VaraticPrim.Domain.Entity;

public class CounterEntity : BaseEntity
{
    public string Barcode { get; set; }
    public int Value { get; set; }
    public int LocationId { get; set; }
    public virtual LocationEntity Location { get; set; }
}