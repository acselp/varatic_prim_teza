using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaraticPrim.Service.Interfaces;
using VaraticPrim.Service.Models.LoginModel;

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
        var user = _authenticationService.Authenticate(loginModel);

        if (user != null)
        {
            var token = _tokenGenerator.Generate(user);

            return Ok(token);
        }

        return NotFound("User not found");
    }
}