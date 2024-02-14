using HealthManagerServer.Model;
using HealthManagerServer.Service;

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
    
    

    [HttpPost("user/register")]
    public IActionResult Register(string userName, string email, string password,double weight,Gender gender)
    {
        
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            Email = email,
            Password = password,
            Weight = weight,
            Gender = gender
        };
    
        _userRepository.AddUser(user);
        return Ok(user);
    }
        
    
}