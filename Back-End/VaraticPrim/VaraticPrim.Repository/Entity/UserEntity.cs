﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaraticPrim.Repository.Entity;

public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int ContactId { get; set; }
    public int RoleId { get; set; }
}