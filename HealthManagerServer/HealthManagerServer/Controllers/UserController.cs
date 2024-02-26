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
    private readonly IUserRepository _userRepository ;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    


    [HttpPost("/api/user/register")]
    public IActionResult Register([FromBody] User user)
    {
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        if(_userRepository.GetByUserName(user.UserName) != null){
            return BadRequest("Username already taken! Please choose another one!");
        }
        if(_userRepository.GetByEmail(user.Email) != null){
            return BadRequest($"{user.Email} is already registered!");
        }
        _userRepository.AddUser(user);
        return Ok("User registered successfully!");
        
    }
    

}