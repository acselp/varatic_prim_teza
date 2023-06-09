using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Errors;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.UserModels;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class UserController : ApiBaseController
{
    private readonly UserManager _userManager;

    public UserController(
        UserManager userManager)
    {
        _userManager = userManager;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var model = await _userManager.GetById(id);
            
            return Ok(model);
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(FrontEndErrors.UserNotFound.ErrorCode, FrontEndErrors.UserNotFound.ErrorMessage);
        }
    }

    [AllowAnonymous] //For dev
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateModel userModel)
    {
        try
        {
            return Ok(await _userManager.Create(userModel));
        }
        catch (ValidationException e)
        {
            return ValidationError(e);
        }
        catch (UserAlreadyExistsException e)
        {
            return BadRequest(FrontEndErrors.UserAlreadyExists.ErrorCode, FrontEndErrors.UserAlreadyExists.ErrorMessage);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await _userManager.DeleteById(id);
            return Ok();
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(FrontEndErrors.UserNotFound.ErrorCode, FrontEndErrors.UserNotFound.ErrorMessage);
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UserUpdateModel userModel, [FromRoute] int id)
    {
        try
        {
            return Ok(await _userManager.Update(userModel, id));
        }
        catch (UserNotFoundException e)
        {
            return BadRequest(FrontEndErrors.UserNotFound.ErrorCode, FrontEndErrors.UserNotFound.ErrorMessage);
        }
        catch (UserAlreadyExistsException e)
        {
            return BadRequest(FrontEndErrors.EmailAlreadyExists.ErrorCode, FrontEndErrors.EmailAlreadyExists.ErrorMessage);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] UserFilterModel filterModel)
    {
        return Ok(await _userManager.GetAll(filterModel));
    }
}