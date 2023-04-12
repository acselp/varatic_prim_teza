using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Framework.Exceptions;
using VaraticPrim.Framework.Models.LoginModel;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Controllers;

[Route("[controller]/[action]/{id?}")]
public class LoginController : ApiBaseController
{
    private readonly ITokenGeneratorService _tokenGenerator;
    private readonly IAuthenticationService _authenticationService;
    
    public LoginController(
        ITokenGeneratorService tokenGenerator,
        IAuthenticationService authenticationSevice)
    {
        _tokenGenerator = tokenGenerator;
        _authenticationService = authenticationSevice;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        try
        {
            var user = await _authenticationService.Authenticate(loginModel);
            var token = _tokenGenerator.Generate(user);

            return Ok(token);
        }
        catch (EmailOrPasswordNotFoundException)
        {
            return BadRequest("email_password_not_found",
                "Email or password incorrect");
        }
    }
}