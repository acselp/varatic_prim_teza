using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.CounterModels;
using VaraticPrim.Framework.Models.LocationModels;
using VaraticPrim.Framework.Models.UserModels;

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
            return BadRequest("counter_not_found", "Counter not found");
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
            return BadRequest("counter_not_found", "Counter not found");
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] CounterUpdateModel counterModel, [FromRoute] int id)
    {
        try
        {
            return Ok(await _counterManager.Update(counterModel, id));
        }
        catch (LocationNotFoundException e)
        {
            return BadRequest("counter_not_found", "Counter not found");
        }
    }
}