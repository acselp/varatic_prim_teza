﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaraticPrim.Repository.Entity;

public class UserEntity : BaseEntity
{
    public string Username { get; set; }
}