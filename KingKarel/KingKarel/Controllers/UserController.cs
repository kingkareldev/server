using KingKarel.Dto;
using KingKarel.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<UserDto?> GetUser(int id)
    {
        return await _userService.GetUser(id);
    }

    [HttpGet]
    [Route("ByUsername")]
    public async Task<ActionResult<UserDto?>> GetUserByUsername(string username)
    {
        var user = await _userService.GetUserByUsername(username);

        if (user is null)
        {
            return NotFound();
        }

        return user;
    }

    [HttpGet]
    [Route("Current")]
    public async Task<ActionResult<UserDto?>> GetCurrentUser()
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }

        return await _userService.GetUser(userId);
    }
}