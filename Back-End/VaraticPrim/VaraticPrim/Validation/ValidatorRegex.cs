using System.Text.RegularExpressions;

namespace VaraticPrim.Validation;

public static class ValidatorRegex
{
    private static readonly string MoldovaMobileRegex = "^((373|0)([0-9]){8})$";

    public static bool IsMoldovaMobileRegex(string pattern)
    {
        var rx = new Regex(ValidatorRegex.MoldovaMobileRegex);
        
        return rx.Matches(pattern).Count == 0;
    }
}