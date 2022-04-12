using System.Drawing.Printing;
using KingKarel.Dto;
using KingKarel.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionController : ControllerBase
{
    private readonly IStoryService _storyService;

    public MissionController(IStoryService storyService)
    {
        _storyService = storyService;
    }

    [HttpGet("{storyUrl}")]
    public async Task<ActionResult<IEnumerable<MissionsListDto>>> GetMissions(string storyUrl)
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }

        var missions = await _storyService.GetMissions(storyUrl, userId);
        
        return Ok(missions);
    }

    [HttpGet("{storyUrl}/{missionUrl}")]
    public async Task<ActionResult<IEnumerable<MissionsListDto?>>> GetMission(string storyUrl, string missionUrl)
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }

        var mission = await _storyService.GetMission(missionUrl, userId);

        if (mission is null)
        {
            return NotFound();
        }
        
        return Ok(mission);
    }

    [HttpPut("{storyUrl}/{missionUrl}")]
    public async Task<ActionResult> UpdateGameProgress(
        [FromBody] GameProgressDto data,
        string storyUrl,
        string missionUrl
    )
    {
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out var userId);
        if (!success)
        {
            return Unauthorized();
        }

        Console.WriteLine($"put {data}");
        
        await _storyService.SaveGameProgress(data, userId, missionUrl);
        return Ok();
    }
}