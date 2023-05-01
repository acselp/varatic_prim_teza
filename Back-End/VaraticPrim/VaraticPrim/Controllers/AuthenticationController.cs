using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Managers;
using VaraticPrim.Framework.Models.LoginModel;
using VaraticPrim.Framework.Models.TokenModels;

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
    } 
    
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> LoginByRefreshToken([FromBody] LoginByRefreshTokenModel tokenModel)
    {
        try
        {
            var model = await _authenticationManager.LoginByRefreshToken(tokenModel.RefreshToken);
            return Ok(model);
        }
        catch (InvalidAccessTokenOrRefreshTokenException)
        {
            return BadRequest("invalid_token",
                "Invalid token");
        }
        catch (EmailOrPasswordNotFoundException)
        {
            return BadRequest("email_password_not_found",
                "Email or password incorrect");
        }
    } 
}