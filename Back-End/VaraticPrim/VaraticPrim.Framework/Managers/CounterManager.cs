using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.CounterModels;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Repository.Repository;

namespace VaraticPrim.Framework.Managers;

public class CounterManager
{
    private readonly ILocationRepository            _locationRepository;
    private readonly ICounterRepository             _counterRepository;
    private readonly IMapper                        _mapper;
    private readonly ILogger<CounterManager>        _logger;
    private readonly IValidator<CounterCreateModel> _counterCreateValidator;
    private readonly IValidator<CounterUpdateModel> _counterUpdateValidator;

    public CounterManager(
        IValidator<CounterCreateModel> counterCreateValidator,
        IValidator<CounterUpdateModel> counterUpdateValidator,
        ILocationRepository locationRepository,
        ICounterRepository counterRepository,
        IMapper mapper,
        ILogger<CounterManager> logger)
    {
        _counterUpdateValidator = counterUpdateValidator;
        _counterCreateValidator = counterCreateValidator;
        _locationRepository = locationRepository;
        _logger = logger;
        _counterRepository = counterRepository;
        _mapper = mapper;
    }

    public async Task<CounterModel> Create(CounterCreateModel counter)
    {
        try
        {
            _logger.LogInformation("Creating counter");
            await _counterCreateValidator.ValidateAndThrowAsync(counter);
            
            var counterEntity = _mapper.Map<CounterEntity>(counter);

            if (await _counterRepository.CounterExists(counter.Barcode))
            {
                _logger.LogWarning($"Counter with barcode = {counter.Barcode} already exists.");
                throw new CounterAlreadyExistsException($"Counter with barcode = {counter.Barcode} already exists.");
            }
            
            await _counterRepository.Insert(counterEntity);

            var location = await _locationRepository.GetById(counter.LocationId);
            
            var counterModel = _mapper.Map<CounterModel>(counterEntity);
            counterModel.Location = _mapper.Map<LocationModel>(location);
            _logger.LogInformation("Counter created.");
              
            return counterModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create counter");
            throw;
        }
    }
    
    public async Task<CounterModel> GetById(int id)
    {
        try
        {
            var counterEntity = await _counterRepository.GetById(id);
            if (counterEntity == null)
            {
                _logger.LogWarning($"Counter with id = {id} not found", id);
                throw new LocationNotFoundException($"Counter with id = {id} not found");
            }

            return _mapper.Map<CounterModel>(counterEntity);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get counter");
            throw;
        }
    }
    
    public async Task<CounterModel> DeleteById(int id)
    {
        try
        {
            var counter = await _counterRepository.GetById(id);
            
            if (counter == null)
            {
                _logger.LogWarning($"Counter with id = {id} not found", id);
                throw new CounterNotFoundException($"Counter with id = {id} not found");
            }
    
            var counterModel = _mapper.Map<CounterModel>(counter);
            
            await _counterRepository.Delete(counter);
    
            return counterModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to delete counter");
            throw;
        }
    }
    
     public async Task<CounterModel> Update(CounterUpdateModel counter, int id)
     {
         try
         {
             var counterFromDb = await _counterRepository.GetById(id);
             await _counterUpdateValidator.ValidateAndThrowAsync(counter);
             
             if (counterFromDb == null)
             {
                 _logger.LogWarning($"Counter with id = {id} not found", id);
                 throw new CounterNotFoundException($"Counter with id = {id} not found");
             }
    
             var counterEntity = _mapper.Map<CounterEntity>(counter);
    
             await _counterRepository.Update(counterEntity);
    
             return _mapper.Map<CounterModel>(counterFromDb);
         }
         catch (Exception e)
         {
             _logger.LogError(e, "Failed to update counter");
             throw;
         }
    }
}