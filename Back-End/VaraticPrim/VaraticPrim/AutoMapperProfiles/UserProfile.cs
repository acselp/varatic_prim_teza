using AutoMapper;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Models.ContactModels;
using VaraticPrim.Models.UserModels;

namespace VaraticPrim.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateModel, UserEntity>()
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact));
        
        CreateMap<UserModel, UserEntity>();
        CreateMap<UserEntity, UserModel>();
        CreateMap<ContactModel, ContactEntity>();
        CreateMap<ContactEntity, ContactModel>();
    }
}