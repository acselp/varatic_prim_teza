using AutoMapper;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Models.UserModels;

namespace VaraticPrim.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, UserCreateModel>();
        CreateMap<UserCreateModel, UserEntity>();
        CreateMap<UserEntity, UserModel>();
        CreateMap<UserModel, UserEntity>();
    }
}