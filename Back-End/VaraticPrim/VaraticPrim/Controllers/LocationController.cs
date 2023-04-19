using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.LocationModels;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class LocationController : ApiBaseController
{
    private readonly LocationManager _locationManager;

    public LocationController(LocationManager locationManager)
    {
        _locationManager = locationManager;
    }
    
    [AllowAnonymous]
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
}