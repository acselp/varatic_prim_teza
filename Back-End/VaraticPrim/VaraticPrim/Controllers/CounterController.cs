using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Errors;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.CounterModels;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class CounterController : ApiBaseController
{
    private readonly CounterManager _counterManager;

    public CounterController(CounterManager counterManager)
    {
        _counterManager = counterManager;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CounterCreateModel counter)
    {
        try
        {
            return Ok(await _counterManager.Create(counter));
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
            return Ok(await _counterManager.GetById(id));
        }
        catch (CounterNotFoundException)
        {
            return BadRequest(FrontEndErrors.CounterNotFound.ErrorCode, FrontEndErrors.CounterNotFound.ErrorMessage);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _counterManager.DeleteById(id);
            return Ok();
        }
        catch (CounterNotFoundException e)
        {
            return BadRequest(FrontEndErrors.CounterNotFound.ErrorCode, FrontEndErrors.CounterNotFound.ErrorMessage);
        }
    }
    
    // [HttpPut("{id:int}")]
    // public async Task<IActionResult> Update([FromBody] CounterUpdateModel counterModel, [FromRoute] int id)
    // {
    //     try
    //     {
    //         return Ok(await _counterManager.Update(counterModel, id));
    //     }
    //     catch (CounterNotFoundException e)
    //     {
    //         return BadRequest(FrontEndErrors.CounterNotFound.ErrorCode, FrontEndErrors.CounterNotFound.ErrorMessage);
    //     }
    // }
    
    [HttpPut("update-value")]
    public async Task<IActionResult> UpdateByBarCode([FromBody] CounterUpdateModel counterModel)
    {
        try
        {
            return Ok(await _counterManager.UpdateByBarCode(counterModel));
        }
        catch (InvalidCounterValueException e)
        {
            return BadRequest(FrontEndErrors.InvalidCounterValue.ErrorCode, FrontEndErrors.InvalidCounterValue.ErrorMessage);
        }
    }
}