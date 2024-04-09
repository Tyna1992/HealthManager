using HealthManagerServer.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("getAll"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("getById/{id}"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserById(string id)
    {
        try
        {
            var user = await _userRepository.GetUserById(id);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("getByEmail/{email}"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        try
        {
            var user = await _userRepository.GetByEmail(email);
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("update/{id}"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserResponse userResponse)
    {
        try
        {
            await _userRepository.UpdateUser(id, userResponse);
            return Ok(new { message = "Updated" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete/{id}"), Authorize(Roles = "User, Admin")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        try
        {
            await _userRepository.DeleteUser(id);
            return Ok(new { message = "Deleted" });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}