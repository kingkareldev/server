using KingKarel.Dto;
using KingKarel.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;

    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpGet]
    public async Task<IEnumerable<StoryDto>> GetStories()
    {
        return await _storyService.GetStories();
    }

    [HttpGet("{storyId}")]
    public async Task<ActionResult<StoryWithMissionsDto?>> GetStory(string storyId)
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }
        var story = await _storyService.GetStory(storyId, userId);

        if (story is null)
        {
            return BadRequest();
        }

        return Ok(story);
    }
}