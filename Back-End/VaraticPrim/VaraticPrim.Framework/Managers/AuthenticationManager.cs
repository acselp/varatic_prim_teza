using Microsoft.Extensions.Logging;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.LoginModel;
using VaraticPrim.Framework.TokenGenerator;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Framework.Managers;

public class AuthenticationManager
{
    private readonly ITokenGeneratorService         _tokenGeneratorService;
    private readonly IUserRepository                _userRepository;
    private readonly ILogger<AuthenticationManager> _logger;
    private readonly IHashService                   _hashService;
    private readonly IRefreshTokenRepository        _refreshRepository;


    public AuthenticationManager(
        IRefreshTokenRepository        refreshRepository,
        ITokenGeneratorService         tokenGeneratorService,
        IUserRepository                userRepository,
        ILogger<AuthenticationManager> logger,
        IHashService                   hashService)
    {
        _refreshRepository     = refreshRepository;
        _hashService           = hashService;
        _tokenGeneratorService = tokenGeneratorService;
        _userRepository        = userRepository;
        _logger                = logger;
    }

    public async Task<LoginResultModel> Login(LoginModel loginModel)
    {
        try
        {
            _logger.LogInformation("Start authenticating user");
            var currentUser = await _userRepository.GetByEmail(loginModel.Email);

            if (currentUser == null || !_hashService.PasswordHashMatches(currentUser.PasswordHash,
                    loginModel.Password, currentUser.PasswordSalt))
            {
                _logger.LogWarning("Wrong email or password");
                throw new EmailOrPasswordNotFoundException("Wrong email or password");
            }
            
            var accessToken  = _tokenGeneratorService.GenerateAccessToken(currentUser.Id);
            var refreshToken = _tokenGeneratorService.GenerateRefreshToken();
         
            await _refreshRepository.Insert(
                new RefreshTokenEntity
                {
                    RefreshToken   = refreshToken.Token,
                    UserId         = currentUser.Id,
                    ExpirationTime = refreshToken.Expires
                });

            return new LoginResultModel
            {
                AccessToken                = accessToken.AccessToken,
                ExpiresIn                  = accessToken.ExpirationTime,
                TokenType                  = accessToken.TokenType,
                RefreshToken               = refreshToken.Token,
                RefreshTokenExpirationTime = refreshToken.Expires
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to login");
            throw;
        }
    }

    public async Task<LoginResultModel> LoginByRefreshToken(string token)
    {
        var currentUser  = await GetUserByRefreshToken(token);
        var accessToken  = _tokenGeneratorService.GenerateAccessToken(currentUser.Id);
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken();
         
        await _refreshRepository.Insert(
            new RefreshTokenEntity
            {
                RefreshToken   = refreshToken.Token,
                UserId         = currentUser.Id,
                ExpirationTime = refreshToken.Expires
            });

        return new LoginResultModel
        {
            AccessToken                = accessToken.AccessToken,
            ExpiresIn                  = accessToken.ExpirationTime,
            TokenType                  = accessToken.TokenType,
            RefreshToken               = refreshToken.Token,
            RefreshTokenExpirationTime = refreshToken.Expires
        };
    }
    
    private async Task<UserEntity> GetUserByRefreshToken(string token)
    {
        var entity = await _refreshRepository.GetUserByToken(token);

        if (entity == null || entity.IsExpired)
        {
            throw new InvalidAccessTokenOrRefreshTokenException();
        }

        return entity.UserEntity ?? throw new InvalidAccessTokenOrRefreshTokenException();
    }
}
