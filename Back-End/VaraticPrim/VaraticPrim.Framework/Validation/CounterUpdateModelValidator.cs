using FluentValidation;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.Models.CounterModels;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Repository.Repository.Implementations;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Framework.Validation;

public class CounterUpdateModelValidator: AbstractValidator<CounterUpdateModel>
{
    public CounterUpdateModelValidator(ICounterRepository counterRepository)
    {
    }
}