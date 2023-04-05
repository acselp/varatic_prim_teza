using System.ComponentModel.DataAnnotations;

namespace VaraticPrim.Repository.Entity;

public class User
{
    [Key] 
    public int Id { get; set; }
    public string Username { get; set; }
}