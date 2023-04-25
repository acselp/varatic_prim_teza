using AutoMapper;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;
using LocationCreateModel = VaraticPrim.Framework.Models.LocationModels.LocationCreateModel;

namespace VaraticPrim.Framework.AutoMapperProfiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<LocationCreateModel, LocationEntity>();
        CreateMap<LocationCreateModel, LocationModel>();
        CreateMap<LocationEntity, LocationCreateModel>();
        CreateMap<LocationEntity, LocationModel>();
        CreateMap<LocationModel, LocationEntity>();
    }
}