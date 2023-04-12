using System.Text.RegularExpressions;

namespace VaraticPrim.Framework.Validation;

public static class ValidatorRegex
{
    public static Regex MoldovaMobileRegex = new Regex("^((373|0)([0-9]){8})$");
}