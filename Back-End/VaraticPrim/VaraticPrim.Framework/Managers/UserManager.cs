using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Framework.Managers;

public class UserManager
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateModel> _userValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserManager> _logger;
    private readonly IHashService _hashService;
    
    public UserManager(
        IUserRepository userRepository, 
        IMapper mapper, 
        IValidator<UserCreateModel> userValidator, 
        ILogger<UserManager> logger, 
        IHashService hashService)
    {
        _hashService = hashService;
        _userValidator = userValidator;
        _logger = logger;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> Create(UserCreateModel userCreateModel)
    {
        try
        {
            _logger.LogInformation("Creating user...");
            await _userValidator.ValidateAndThrowAsync(userCreateModel);
            
            var user = await _userRepository.GetByEmail(userCreateModel.Email);

            if (user != null)
            {
                _logger.LogWarning("User with email = " + userCreateModel.Email + " already exists");
                throw new UserAlreadyExistsException("User with email = " + userCreateModel.Email + " already exists");
            }

            var userEntity = _mapper.Map<UserEntity>(userCreateModel);
            var passwordSalt = _hashService.GenerateSalt();

            userEntity.PasswordHash = _hashService.Hash(userCreateModel.Password, passwordSalt);
            userEntity.PasswordSalt = passwordSalt;

            await _userRepository.Insert(userEntity);

            var validUserModel = _mapper.Map<UserModel>(userEntity);
            _logger.LogInformation("User created.");
              
            return validUserModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create user");
            throw;
        }
    }

    public async Task<UserModel> GetById(int id)
    {
        try
        {
            var userEntity = await _userRepository.GetById(id);
            if (userEntity == null)
            {
                _logger.LogWarning("User with id = " + id + " not found");
                throw new UserNotFoundException("User with id = " + id + " not found");
            }

            return _mapper.Map<UserModel>(userEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get user");
            throw;
        }
    }
    
    public async Task<UserModel> DeleteById(int id)
    {
        try
        {
            var user = await _userRepository.GetById(id);
            
            if (user == null)
            {
                _logger.LogWarning("User with id = " + id + " not found");
                throw new UserNotFoundException("User with id = " + id + " not found");
            }

            var userModel = _mapper.Map<UserModel>(user);
            
            await _userRepository.Delete(user);

            return userModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete user");
            throw;
        }
    }
    
    public async Task<UserModel> Update(UserUpdateModel user, int id)
    {
        try
        {
            var userFromDb = await _userRepository.GetById(id);

            if (await _userRepository.EmailExists(user.Email))
                throw new UserAlreadyExistsException("User with email = " + user.Email + " already exists");

            if (userFromDb == null)
            {
                _logger.LogWarning("User with id = " + id + " not found");
                throw new UserNotFoundException("User with id = " + id + " not found");
            }

            userFromDb.Contact = _mapper.Map<ContactEntity>(user.Contact);
            userFromDb.Email = user.Email;
            
            await _userRepository.Update(userFromDb);

            return _mapper.Map<UserModel>(userFromDb);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update user");
            throw;
        }
    }
}