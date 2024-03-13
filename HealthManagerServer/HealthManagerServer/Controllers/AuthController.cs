using HealthManagerServer.Contracts;
using HealthManagerServer.Service.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthManagerServer.Controllers;

[ApiController]
[Route("api/[controller]")]


public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, request.Weight, request.Gender, "User");

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.UserName, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        Response.Cookies.Append("Authorization", result.Token, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(new AuthResponse(result.Email, result.UserName));
    }

    [HttpGet("WhoAmI"), Authorize(Roles = "User,Admin")]
    public ActionResult<AuthResponse> WhoAmI()
    {
        var cookie = Request.Cookies["Authorization"];
        
        var token = _authenticationService.Verify(cookie);
        
        if (token != null)
        {
            var claims = token.Claims;
            var email = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            var username = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return Ok(new AuthResponse(email, username));
        }
        return BadRequest("No token found");
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
}