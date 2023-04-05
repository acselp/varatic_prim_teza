using System.ComponentModel.DataAnnotations;

namespace VaraticPrim.Repository.Entity;

public class BaseEntity
{
    [Key] 
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }    
}