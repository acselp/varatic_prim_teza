using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;
using Serilog.Core;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Models.ContactModels;
using VaraticPrim.Models.UserModels;
using VaraticPrim.Repository.Repository;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]/{id?}")]
public class UserController : ApiBaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateModel> _userValidator;
    private readonly IValidator<ContactCreateModel> _contactValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IConfiguration _config;
    public UserController(
        IUserRepository userRepository, 
        IMapper mapper, 
        IValidator<UserCreateModel> userValidator, 
        IValidator<ContactCreateModel> contactValidator, 
        ILogger<UserController> logger,
        IConfiguration config)
    {
        _config = config;
        _userValidator = userValidator;
        _contactValidator = contactValidator;
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var user = await _userRepository.GetById(id);
        
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateModel userModel)
    {
        try
        {
            _logger.LogInformation("Creating user...");
            await _userValidator.ValidateAndThrowAsync(userModel);
            await _contactValidator.ValidateAndThrowAsync(userModel.Contact);

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
        catch (PostgresException e)
        {
            return DBError(e);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to crate user");
            throw;
        }
    }
}