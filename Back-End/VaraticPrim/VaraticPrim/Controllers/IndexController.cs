using Microsoft.AspNetCore.Mvc;

namespace VaraticPrim.Controllers;

public class IndexController : Controller
{
    public string Index()
    {
        return "Hi there";
    }
}