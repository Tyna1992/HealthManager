using HealthManagerServer.Model;
using HealthManagerServer.Service;
using Microsoft.JSInterop;

namespace HealthManagerServer.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly UserRepository _userRepository = new UserRepository();

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }


    [HttpPost("/api/user/register")]
    public IActionResult Register([FromBody] User user)
    {
        Console.WriteLine("Registering user");
        try
        {
            user.Id = Guid.NewGuid();

            _userRepository.AddUser(user);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
    
   
}