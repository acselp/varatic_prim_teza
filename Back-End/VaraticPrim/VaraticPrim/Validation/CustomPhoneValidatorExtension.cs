using System.Text.RegularExpressions;
using FluentValidation;

namespace VaraticPrim.Validation;

public static class CustomPhoneValidatorExtension
{
    public static IRuleBuilderOptions<T, string> IsMoldovaMobile<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var rx = new Regex("^((373|0)([0-9]){8})$");

        return ruleBuilder
            .Must(list => !(rx.Matches(list.ToString()).Count == 0))
            .WithMessage("This is not a moldova mobile format");
    }
}