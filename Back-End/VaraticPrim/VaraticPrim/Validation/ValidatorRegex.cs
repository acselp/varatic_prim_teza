﻿using System.Text.RegularExpressions;

namespace VaraticPrim.Validation;

public static class ValidatorRegex
{
    public static Regex MoldovaMobileRegex = new Regex("^((373|0)([0-9]){8})$");
}