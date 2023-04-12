using AutoMapper;
using Microsoft.Extensions.Logging;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models;
using VaraticPrim.Framework.Models.LoginModel;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Framework.TokenGenerator;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Framework;

public class AuthenticationManager
{
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthenticationManager> _logger;

    public AuthenticationManager(ITokenGeneratorService tokenGeneratorService, IMapper mapper, IUserRepository userRepository, ILogger<AuthenticationManager> logger)
    {
        _tokenGeneratorService = tokenGeneratorService;
        _mapper = mapper;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<AccessTokenModel> Login(LoginModel loginModel)
    {
        try
        {
            _logger.LogInformation("Start authenticating user");
            var currentUser = await _userRepository.GetByEmail(loginModel.Email);

            if ((currentUser == null) || currentUser?.PasswordHash != loginModel.Password) 
                throw new EmailOrPasswordNotFoundException("Wrong email or password");
            
            var userModel = _mapper.Map<UserModel>(currentUser);       
            return _tokenGeneratorService.Generate(userModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to login");
            throw;
        }
    }
}