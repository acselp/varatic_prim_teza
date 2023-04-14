using FluentValidation;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Framework.Validation;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(user => user.Contact.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(user => user.Contact.LastName)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);

        RuleFor(user => user.Contact.Mobile)
            .IsMoldovaMobile();

        RuleFor(user => user.Contact.Phone)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);
    }
}