﻿using System.Text.RegularExpressions;
using FluentValidation;

namespace VaraticPrim.Framework.Validation;

public static class CustomValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> IsMoldovaMobile<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(input => Regex.IsMatch(input, ValidatorRegex.MoldovaMobileRegex))
            .WithMessage("This is not a moldova mobile format");
    }
}