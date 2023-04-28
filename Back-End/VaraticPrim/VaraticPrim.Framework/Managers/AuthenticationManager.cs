using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models;
using VaraticPrim.Framework.Models.LoginModel;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Framework.TokenGenerator;
using VaraticPrim.JwtAuth;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Framework.Managers;

public class AuthenticationManager
{
    private readonly ITokenGeneratorService _tokenGeneratorService;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthenticationManager> _logger;
    private readonly IHashService _hashService;
    private readonly IRefreshTokenRepository _refreshRepository;
    private readonly IConfiguration _conf;
    private readonly IOptions<JwtConfiguration> _options;


    public AuthenticationManager(
        IOptions<JwtConfiguration> options,
        IRefreshTokenRepository refreshRepository,
        ITokenGeneratorService tokenGeneratorService, 
        IMapper mapper, 
        IUserRepository userRepository, 
        ILogger<AuthenticationManager> logger,
        IHashService hashService)
    {
        _options = options;
        _refreshRepository = refreshRepository;
        _hashService = hashService;
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

            if ((currentUser == null) || !_hashService.PasswordHashMatches(currentUser.PasswordHash,
                    loginModel.Password, currentUser.PasswordSalt))
            {
                _logger.LogWarning("Wrong email or password");
                throw new EmailOrPasswordNotFoundException("Wrong email or password");
            }
            
            var userModel = _mapper.Map<UserModel>(currentUser); 
            var tokenModel = _tokenGeneratorService.GenerateAccessToken(userModel);
            tokenModel.RefreshToken = _tokenGeneratorService.GenerateRefreshToken();

            var refreshFromDb = await _refreshRepository.GetByEmail(loginModel.Email);
            if (refreshFromDb != null)
            {
                await _refreshRepository.Delete(refreshFromDb);
            }
            
            RefreshTokenEntity refreshToken = new()
            {
                Email = currentUser.Email,
                RefreshToken = tokenModel.RefreshToken,
                RefreshTokenExpirationTime = DateTime.UtcNow.AddMinutes(_options.Value.AccessTokenExpirationTimeMin)
            };

            await _refreshRepository.Insert(refreshToken);
            
            return tokenModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to login");
            throw;
        }
    }
    
    public async Task<AccessTokenModel> RefreshToken(AccessTokenModel tokenModel)
    {
        try
        {
            if (tokenModel is null)
            {
                throw new NullReferenceException("Access model is null");
            }
        
            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = _tokenGeneratorService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                throw new InvalidAccessTokenOrRefreshTokenException("Invalid access token or refresh token");
            }
        
            var userIdClaim = principal.Claims.ToList()[0].Value; //Get user id
            int userId = Int32.Parse(userIdClaim);
            
            var currentUser = _userRepository.GetById(userId);
            var userRefreshToken = await _refreshRepository.GetByEmail(currentUser.Result.Email);
            
            await _refreshRepository.Delete(userRefreshToken); //INstead of update, problems with update. TODO fix update
            
            var userModel = _mapper.Map<UserModel>(currentUser.Result);
            
            if (userRefreshToken == null || userRefreshToken.RefreshToken != refreshToken || userRefreshToken.RefreshTokenExpirationTime <= DateTime.Now)
            {
                throw new InvalidAccessTokenOrRefreshTokenException("Invalid access token or refresh token");
            }

            
            var newAccessToken = _tokenGeneratorService.GenerateAccessToken(userModel);
            newAccessToken.RefreshToken = _tokenGeneratorService.GenerateRefreshToken();
            newAccessToken.RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(_options.Value.RefreshTokenExpirationTimeDays);

            userRefreshToken.RefreshToken = newAccessToken.RefreshToken;
            userRefreshToken.RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(_options.Value.RefreshTokenExpirationTimeDays);
            
            await _refreshRepository.Insert(userRefreshToken);

            return newAccessToken;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to refresh token");
            throw;
        }
    }
}
