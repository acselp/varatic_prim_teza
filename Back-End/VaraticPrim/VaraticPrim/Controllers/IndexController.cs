using Microsoft.AspNetCore.Mvc;

namespace VaraticPrim.Controllers;

[Route("[controller]")]
public class IndexController : ControllerBase
{
    private ILogger<IndexController> _logger;

    public IndexController(ILogger<IndexController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Hi there, this is the first log using Serilog!!!");
        
        return Ok("Salut");
    }
}