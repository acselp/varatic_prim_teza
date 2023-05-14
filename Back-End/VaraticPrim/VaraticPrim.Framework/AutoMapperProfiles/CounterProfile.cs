using AutoMapper;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.Models.CounterModels;
using VaraticPrim.Framework.Models.LocationModels;
using LocationCreateModel = VaraticPrim.Framework.Models.LocationModels.LocationCreateModel;

namespace VaraticPrim.Framework.AutoMapperProfiles;

public class CounterProfile : Profile
{
    public CounterProfile()
    {
        CreateMap<CounterCreateModel, CounterEntity>();
        CreateMap<CounterUpdateModel, CounterEntity>();
        CreateMap<CounterEntity, CounterUpdateModel>();
        CreateMap<CounterEntity, CounterModel>();
    }
}