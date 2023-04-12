﻿using VaraticPrim.Framework.Models.ContactModels;

namespace VaraticPrim.Framework.Models.UserModels;

public class UserCreateModel
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ContactCreateModel Contact { get; set; }
}