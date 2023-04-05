using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaraticPrim.Repository.Entity;

[Table("VaraticPrim.User")]
public class User : BaseEntity
{
    public string Username { get; set; }
}