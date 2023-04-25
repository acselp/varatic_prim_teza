﻿using AutoMapper;
using VaraticPrim.Domain.Entity;
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
        CreateMap<CounterEntity, CounterModel>();
    }
}