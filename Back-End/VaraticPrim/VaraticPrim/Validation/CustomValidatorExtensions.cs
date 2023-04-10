using System.Text.RegularExpressions;
using FluentValidation;

namespace VaraticPrim.Validation;

public static class CustomValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsMoldovaMobile<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(list => !(ValidatorRegex.IsMoldovaMobileRegex(list.ToString())))
            .WithMessage("This is not a moldova mobile format");
    }
}