﻿namespace VaraticPrim.Models.UserModels;

public class UserCreateModel
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int ContactId { get; set; }
    public int RoleId { get; set; }
}