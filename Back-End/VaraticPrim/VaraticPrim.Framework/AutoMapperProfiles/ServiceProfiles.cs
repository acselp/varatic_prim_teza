using AutoMapper;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.Models.ServiceModels;

namespace VaraticPrim.Framework.AutoMapperProfiles;

public class ServiceProfiles : Profile
{
    public ServiceProfiles()
    {
        CreateMap<ServiceEntity, ServiceModel>();
        CreateMap<ServiceModel, ServiceEntity>();
    }
}