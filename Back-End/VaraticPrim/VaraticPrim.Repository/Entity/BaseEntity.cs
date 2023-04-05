using System.ComponentModel.DataAnnotations;

namespace VaraticPrim.Repository.Entity;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime UpdatedOnUtc { get; set; }    
}