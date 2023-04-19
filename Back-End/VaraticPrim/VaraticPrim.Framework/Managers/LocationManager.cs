using AutoMapper;
using Microsoft.Extensions.Logging;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Framework.Managers;

public class LocationManager
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationManager> _logger;

    public LocationManager(
        ILocationRepository locationRepository,
        IMapper mapper,
        ILogger<LocationManager> logger)
    {
        _logger = logger;
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<LocationModel> Create(LocationCreateModel location)
    {
        try
        {
            _logger.LogInformation("Creating location...");
            // await _userValidator.ValidateAndThrowAsync(userCreateModel);

            var locationEntity = _mapper.Map<LocationEntity>(location);

            await _locationRepository.Insert(locationEntity);

            var validLocationModel = _mapper.Map<LocationModel>(location);
            _logger.LogInformation("Location created.");
              
            return validLocationModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create location");
            throw;
        }
    }
}