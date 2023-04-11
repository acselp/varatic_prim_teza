using AutoMapper;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Exceptions;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models.LoginModel;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Service.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public UserModel Authenticate(LoginModel loginModel)
    {
        var currentUser = _userRepository.GetByEmail(loginModel.Email);

        if (currentUser != null)
            if (currentUser.PasswordHash == loginModel.Password)
            {
                var userModel = _mapper.Map<UserModel>(currentUser);
                
                return userModel;
            }
        
        throw new EmailOrPasswordNotFoundException("Wrong email or password");
    }
}