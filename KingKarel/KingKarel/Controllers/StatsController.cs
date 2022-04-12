using KingKarel.Dto;
using KingKarel.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StatsController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryWithMissionsDto>>> GetStoriesStats()
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }

        return Ok(await _storyService.GetStoriesStats(userId));
    }
}