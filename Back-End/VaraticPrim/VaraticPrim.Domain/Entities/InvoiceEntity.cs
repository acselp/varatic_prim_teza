using System.Text.Json;

namespace VaraticPrim.Domain.Entities;

public class InvoiceEntity : BaseEntity
{
    public int LocationId { get; set; }
    public virtual LocationEntity Location { get; set; }
    public int ServiceId { get; set; }
    public virtual ServiceEntity Service { get; set; }
    public bool PaymentStatus { get; set; }
    public int Amount { get; set; }
    public JsonDocument Data { get; set; }
}