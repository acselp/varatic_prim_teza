﻿namespace VaraticPrim.Service.Models;

public class AccessTokenModel
{
    public DateTime TokenExpirationTime { get; set; }
    public string Token { get; set; }
}