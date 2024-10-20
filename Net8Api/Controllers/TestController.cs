using Microsoft.AspNetCore.Mvc;

namespace Net8Api.Controllers;

public class TestController : Controller
{
    [HttpGet("/noauth")]
    public IActionResult TestNoAuth()
    {
        throw new UnauthorizedException("You are not authorized to access this resource.");
    }
    
    [HttpGet("/notfound")]
    public IActionResult TestNotFound()
    {
        throw new NotFoundException("The requested resource was not found.");
    }
    
    [HttpGet("/error")]
    public IActionResult TestError()
    {
        throw new Exception("An unexpected error occurred.");
    }
}