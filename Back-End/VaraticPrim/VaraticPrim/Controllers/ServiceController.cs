using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Errors;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class ServiceController : ApiBaseController
{
    private readonly ServiceManager _serviceManager;

    public ServiceController(ServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var model = await _serviceManager.GetById(id);
            
            return Ok(model);
        }
        catch (ServiceNotFoundException e)
        {
            return BadRequest(FrontEndErrors.ServiceNotFound.ErrorCode, FrontEndErrors.ServiceNotFound.ErrorMessage);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceManager.GetAll());
    }
}