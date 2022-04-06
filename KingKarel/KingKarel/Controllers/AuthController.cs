using KingKarel.Dto;
using KingKarel.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<AuthResponseDto?>> Login([FromBody] LoginDto loginData)
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        
        UserDto? user = await _userService.LoginUser(loginData);
        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(new AuthResponseDto(_jwtService.GenerateJwtToken(user), user));
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<AuthResponseDto?>> Register([FromBody] RegisterDto registerData)
    {
        // Check if user exists.
        var user = await _userService.GetUserByUsername(registerData.Username);
        if (user != null)
        {
            return Unauthorized();
        }

        // Try to register the user.
        var registeredUser = await _userService.RegisterUser(registerData);
        if (registeredUser is null)
        {
            return Problem();
        }

        return Ok(new AuthResponseDto(_jwtService.GenerateJwtToken(registeredUser), registeredUser));
    }
}