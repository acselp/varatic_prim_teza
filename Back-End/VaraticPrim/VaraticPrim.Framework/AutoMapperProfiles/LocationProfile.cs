using AutoMapper;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;
using LocationCreateModel = VaraticPrim.Framework.Models.CounterModels.LocationCreateModel;

namespace VaraticPrim.Framework.AutoMapperProfiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<LocationCreateModel, LocationEntity>();
        CreateMap<LocationEntity, LocationCreateModel>();
        CreateMap<LocationModel, LocationEntity>();
        CreateMap<UserModel, UserEntity>();
        CreateMap<UserCreateModel, UserEntity>();
        CreateMap<UserEntity, UserModel>();
    }
}