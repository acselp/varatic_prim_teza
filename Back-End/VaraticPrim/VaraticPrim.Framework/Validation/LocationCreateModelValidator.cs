using FluentValidation;
using VaraticPrim.Framework.Models.CounterModels;

namespace VaraticPrim.Framework.Validation;

public class LocationCreateModelValidator: AbstractValidator<LocationCreateModel>
{
    public LocationCreateModelValidator()
    {
    }
}