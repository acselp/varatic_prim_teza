using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Errors;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class LocationController : ApiBaseController
{
    private readonly LocationManager _locationManager;

    public LocationController(LocationManager locationManager)
    {
        _locationManager = locationManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LocationCreateModel location)
    {
        try
        {
            return Ok(await _locationManager.Create(location));
        }
        catch (ValidationException e)
        {
            return ValidationError(e);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            return Ok(await _locationManager.GetById(id));
        }
        catch (LocationNotFoundException)
        {
            return BadRequest(FrontEndErrors.LocationNotFound.ErrorCode, FrontEndErrors.LocationNotFound.ErrorMessage);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _locationManager.DeleteById(id);
            return Ok();
        }
        catch (LocationNotFoundException e)
        {
            return BadRequest(FrontEndErrors.LocationNotFound.ErrorCode, FrontEndErrors.LocationNotFound.ErrorMessage);
        }
    }

    [AllowAnonymous]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] LocationUpdateModel locationModel, [FromRoute] int id)
    {
        try
        {
            return Ok(await _locationManager.Update(locationModel, id));
        }
        catch (LocationNotFoundException e)
        {
            return BadRequest(FrontEndErrors.LocationNotFound.ErrorCode, FrontEndErrors.LocationNotFound.ErrorMessage);
        }
    }
}