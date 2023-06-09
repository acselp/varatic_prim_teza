﻿using AutoMapper;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.Models.ContactModels;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Framework.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateModel, UserEntity>();
        CreateMap<UserCreateModel, UserModel>();
        CreateMap<UserModel, UserEntity>();
        CreateMap<UserEntity, UserModel>();
        CreateMap<UserEntity, UserCreateModel>();
        CreateMap<ContactCreateModel, ContactEntity>();
        CreateMap<ContactEntity, ContactCreateModel>();
        CreateMap<ContactEntity, ContactModel>();
        CreateMap<ContactCreateModel, ContactModel>();
        CreateMap<ContactModel, ContactCreateModel>();
        CreateMap<ContactModel, ContactEntity>();
        CreateMap<ContactEntity, ContactCreateModel>();
    }
}