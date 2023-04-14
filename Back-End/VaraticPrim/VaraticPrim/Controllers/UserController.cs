using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Framework;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.UserModels;
using VaraticPrim.Repository.Repository;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class UserController : ApiBaseController
{
    private readonly ILogger<UserController> _logger;
    private readonly UserManager _userManager;
    private readonly IMapper _mapper;
    private readonly IAuthenticationAccessor _authenticationAccessor;

    public UserController(
        UserManager userManager,
        ILogger<UserController> logger, 
        IMapper mapper,
        IAuthenticationAccessor authenticationAccessor)
    {
        _authenticationAccessor = authenticationAccessor;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
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
            return BadRequest("user_not_found", "User not found");
        }
    }

    [AllowAnonymous]
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
            return BadRequest("user_already_exists", "User already exists");
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            return Ok(await _userManager.DeleteById(id));
        }
        catch (UserNotFoundException e)
        {
            return BadRequest("user_not_found", "User not found");
        }
    }
    
    [AllowAnonymous]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UserUpdateModel userModel, [FromRoute] int id)
    {
        try
        {
            return Ok(await _userManager.Update(userModel, id));
        }
        catch (UserNotFoundException e)
        {
            return BadRequest("user_not_found", "User not found");
        }
        catch (UserAlreadyExistsException e)
        {
            return BadRequest("email_already_exists", "Email already exists");
        }
    }
}