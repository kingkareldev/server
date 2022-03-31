using KingKarel.Dto;
using KingKarel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KingKarel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IStoryRepository _storyRepository;

    public StoryController(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<StoryDto>> GetStories()
    {
        return await _storyRepository.GetStories();
    }

    [HttpGet("{storyId}")]
    public async Task<ActionResult<StoryDto?>> GetStory(string storyId)
    {
        var story = await _storyRepository.GetStory(storyId);

        if (story is null)
        {
            return BadRequest();
        }

        return Ok(story);
    }

    // [HttpPost]
    // public async Task<ActionResult<Story>> PostStory([FromBody] Story story)
    // {
    //     var newStory = await _storyRepository.Create(story);
    //     return CreatedAtAction(nameof(GetBooks), new { id = newStory.Id }, newStory);
    // }

    // [HttpPut]
    // public async Task<ActionResult> PostStory(int id, [FromBody] Story story)
    // {
    //     if (id != story.Id)
    //     {
    //         return BadRequest();
    //     }
    //     
    //     var newStory = await _storyRepository.Update(story);
    //
    //     return NoContent();
    // }

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(int id)
    // {
    //     var storyToDelete = _storyRepository.Get(id);
    //     if (storyToDelete == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     await _storyRepository.Delete(storyToDelete.Id);
    //     return NoContent();
    // }

    // TODO: entity to DTO
    // private static Reminder GetDto(Database.Reminder reminder) =>
    //     new(reminder.Id, reminder.OwnerId, reminder.MessageId, reminder.ChannelId, reminder.DateTime, reminder.Content);
}