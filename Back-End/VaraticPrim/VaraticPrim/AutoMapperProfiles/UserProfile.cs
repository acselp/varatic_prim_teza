using System.ComponentModel.DataAnnotations;
using AutoMapper;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Service.Models.ContactModels;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateModel, UserEntity>();
        CreateMap<UserModel, UserEntity>();
        CreateMap<UserEntity, UserModel>();
        CreateMap<ContactCreateModel, ContactEntity>();
        CreateMap<ContactEntity, ContactCreateModel>();
        CreateMap<ContactEntity, ContactModel>();
        CreateMap<ContactCreateModel, ContactModel>();
        CreateMap<ContactModel, ContactCreateModel>();
    }
}