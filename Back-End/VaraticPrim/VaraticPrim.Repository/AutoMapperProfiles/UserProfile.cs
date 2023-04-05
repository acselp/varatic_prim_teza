using AutoMapper;
using VaraticPrim.Repository.Entity;
using VaraticPrim.Repository.Models.UserModels;

namespace VaraticPrim.Repository.AutoMapperProfiles;

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