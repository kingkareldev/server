using KingKarel.Dto;
using KingKarel.Repository.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStoryRepository _storyRepository;

    public StatsController(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryWithMissionsDto>>> GetStoriesStats()
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }

        return Ok(await _storyRepository.GetStoriesStats(userId));
    }
}