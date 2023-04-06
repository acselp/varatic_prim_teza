using FluentValidation;
using VaraticPrim.Models.UserModels;

namespace VaraticPrim.Validation;

public class UserValidator : AbstractValidator<UserCreateModel>
{
    public UserValidator()
    {
        RuleFor(user => user.Email).NotEmpty().EmailAddress();
    }
}