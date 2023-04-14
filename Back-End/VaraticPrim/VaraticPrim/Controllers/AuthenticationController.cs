using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.LoginModel;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class AuthenticationController : ApiBaseController
{
    private readonly AuthenticationManager _authenticationManager;
    public AuthenticationController(AuthenticationManager authenticationManager)
    {
        _authenticationManager = authenticationManager;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        try
        {
            var model = await _authenticationManager.Login(loginModel);
            return Ok(model);
        }
        catch (EmailOrPasswordNotFoundException)
        {
            return BadRequest("email_password_not_found",
                "Email or password incorrect");
        }
        catch (Exception e)
        {
            return NotFound("Unknown error occured");
        }
    }
}