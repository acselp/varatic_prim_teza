using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Framework.Managers;

public class LocationManager
{
    private readonly ILocationRepository _locationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationManager> _logger;
    private readonly IValidator<LocationCreateModel> _locationCreateValidator;
    private readonly IValidator<LocationUpdateModel> _locationUpdateValidator;

    public LocationManager(
        IUserRepository userRepository,
        ILocationRepository locationRepository,
        IMapper mapper,
        ILogger<LocationManager> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<LocationModel> Create(LocationCreateModel locationCreateModel)
    {
        try
        {
            _logger.LogInformation("Creating location...");

            var locationEntity = _mapper.Map<LocationEntity>(locationCreateModel);
            await _locationCreateValidator.ValidateAndThrowAsync(locationCreateModel);

            await _locationRepository.Insert(locationEntity);

            var locationModel = _mapper.Map<LocationModel>(locationEntity);
            var userEntity = await _userRepository.GetById(locationEntity.UserId);
            
            locationModel.User = _mapper.Map<UserModel>(userEntity);
            _logger.LogInformation("Location created.");
              
            return locationModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create location");
            throw;
        }
    }
    
     public async Task<LocationModel> GetById(int id)
    {
        try
        {
            var locationEntity = await _locationRepository.GetById(id);
            if (locationEntity == null)
            {
                _logger.LogWarning($"Location with id = {id} not found");
                throw new LocationNotFoundException($"Location with id = {id} not found");
            }

            return _mapper.Map<LocationModel>(locationEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get location");
            throw;
        }
    }
    
    public async Task DeleteById(int id)
    {
        try
        {
            var location = await _locationRepository.GetById(id);
            
            if (location == null)
            {
                _logger.LogWarning($"Location with id = {id} not found");
                throw new LocationNotFoundException($"Location with id = {id} not found");
            }
            
            await _locationRepository.Delete(location);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete location");
            throw;
        }
    }
    
    public async Task<LocationModel> Update(LocationUpdateModel locationUpdateModel, int id)
    {
        try
        {
            var locationFromDb = await _locationRepository.GetById(id);
            await _locationUpdateValidator.ValidateAndThrowAsync(locationUpdateModel);
            
            if (locationFromDb == null)
            {
                _logger.LogWarning($"Location with id = {id} not found");
                throw new LocationNotFoundException($"Location with id = {id} not found");
            }

            var locationEntity = _mapper.Map<LocationEntity>(locationUpdateModel);

            await _locationRepository.Update(locationEntity);

            return _mapper.Map<LocationModel>(locationFromDb);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to update location");
            throw;
        }
    }
}