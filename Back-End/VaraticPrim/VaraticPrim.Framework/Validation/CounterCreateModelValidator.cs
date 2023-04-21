using FluentValidation;
using VaraticPrim.Framework.Models.CounterModels;
using VaraticPrim.Framework.Models.LocationModels;

namespace VaraticPrim.Framework.Validation;

public class CounterCreateModelValidator: AbstractValidator<CounterCreateModel>
{
    public CounterCreateModelValidator()
    {
    }
}