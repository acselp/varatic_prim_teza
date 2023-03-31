using Microsoft.AspNetCore.Mvc;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class IndexController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Salut");
    }
}