using FluentValidation;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Framework.Validation;

public class LocationUpdateModelValidator: AbstractValidator<LocationUpdateModel>
{
    public LocationUpdateModelValidator()
    {
    }
}