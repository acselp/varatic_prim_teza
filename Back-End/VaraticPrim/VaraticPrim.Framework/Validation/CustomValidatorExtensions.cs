using FluentValidation;

namespace VaraticPrim.Service.Validation;

public static class CustomValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsMoldovaMobile<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(list => ValidatorRegex.MoldovaMobileRegex.Match(list.ToString()).Groups.Count != 0)
            .WithMessage("This is not a moldova mobile format");
    }
}