﻿namespace VaraticPrim.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public int ContactId { get; set; }
    public virtual ContactEntity Contact { get; set; }
    public int RoleId { get; set; }
}