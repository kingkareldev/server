using KingKarel.Dto;
using KingKarel.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MissionController : ControllerBase
{
    private readonly IStoryRepository _storyRepository;

    public MissionController(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    [HttpGet("{storyUrl}")]
    public async Task<ActionResult<IEnumerable<MissionsListDto>>> GetMissions(string storyUrl)
    {
        int userId;
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out userId);
        if (!success)
        {
            return Unauthorized();
        }

        var missions = await _storyRepository.GetMissions(storyUrl, userId);
        
        foreach (var missionDto in missions)
        {
            Console.WriteLine(missionDto);
        }
        
        return Ok(missions);
    }

    [HttpGet("{storyUrl}/{missionUrl}")]
    public async Task<ActionResult<IEnumerable<MissionsListDto?>>> GetMission(string storyUrl, string missionUrl)
    {
        int userId;
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out userId);
        if (!success)
        {
            return Unauthorized();
        }

        var mission = await _storyRepository.GetMission(missionUrl, userId);

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
        int userId;
        bool success = int.TryParse(User.Claims.First(x => x.Type == "id").Value, out userId);
        if (!success)
        {
            return Unauthorized();
        }

        await _storyRepository.SaveGameProgress(data, userId, missionUrl);
        return Ok();
    }
}