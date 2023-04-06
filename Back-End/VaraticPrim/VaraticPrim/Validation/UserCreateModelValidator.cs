using System.Text.RegularExpressions;
using FluentValidation;
using VaraticPrim.Models.UserModels;

namespace VaraticPrim.Validation;

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
            .Custom((list, context) =>
            {
                var rx = new Regex("^((373|0)([0-9]){8})$");
                var matches = rx.Matches(list.ToString());
                
                if (matches.Count == 0)
                {
                    context.AddFailure("This is not a moldova mobile number");
                }
            });


        RuleFor(user => user.Contact.Phone)
            .NotEmpty()
            .MaximumLength(255)
            .MinimumLength(3);
    }
}