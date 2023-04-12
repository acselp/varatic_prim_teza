using AutoMapper;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper, ILogger<AuthenticationService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<UserModel> Authenticate(LoginModel loginModel)
    {
        try
        {
            _logger.LogInformation("Start authenticating user");
            var currentUser = await _userRepository.GetByEmail(loginModel.Email);

            if ((currentUser == null) || currentUser?.PasswordHash != loginModel.Password) 
                throw new EmailOrPasswordNotFoundException("Wrong email or password");
            
            return _mapper.Map<UserModel>(currentUser);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to authenticate user");
            throw;
        }
    }
}