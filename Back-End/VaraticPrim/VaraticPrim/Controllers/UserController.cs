using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Authentication;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models.ContactModels;
using VaraticPrim.Service.Models.UserModels;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]/{id?}")]
public class UserController : ApiBaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateModel> _userValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IAuthenticationAccessor _authenticationAccessor;
    
    public UserController(
        IUserRepository userRepository, 
        IMapper mapper, 
        IValidator<UserCreateModel> userValidator, 
        ILogger<UserController> logger, IAuthenticationAccessor authenticationAccessor)
    {
        _userValidator = userValidator;
        _logger = logger;
        _authenticationAccessor = authenticationAccessor;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await _userRepository.GetById(id);
        await _authenticationAccessor.LoggedIdentity();
        
        /*
        _logger.LogInformation("Token: " + token);*/
        
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateModel userModel)
    {
        try
        {
            _logger.LogInformation("Creating user...");
            await _userValidator.ValidateAndThrowAsync(userModel);

            var userEntity = _mapper.Map<UserEntity>(userModel);

            await _userRepository.Insert(userEntity);

            var validUserModel = _mapper.Map<UserModel>(userEntity);
            _logger.LogInformation("User created.");

            return Ok(validUserModel);
        }
        catch (ValidationException e)
        {
            return ValidationError(e);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to crate user");
            throw;
        }
    }
}