using System.Data;
using FluentValidation;
using VaraticPrim.Models.ContactModels;

namespace VaraticPrim.Validation;

public class ContactValidator : AbstractValidator<ContactCreateModel>
{
    public ContactValidator()
    {
        RuleFor(contact => contact.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(contact => contact.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(contact => contact.Mobile).
            NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        
        RuleFor(contact => contact.Phone)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
    }
}