using KingKarel.Dto;
using KingKarel.Repository.Contract;
using KingKarel.Services.Contract;

namespace KingKarel.Services;

public class StoryService : IStoryService
{
    private readonly IStoryRepository _storyRepository;

    public StoryService(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public Task<IEnumerable<StoryDto>> GetStories()
    {
        return _storyRepository.GetStories();
    }

    public Task<IEnumerable<StoryWithMissionsDto>> GetStoriesStats(int userId)
    {
        return _storyRepository.GetStoriesStats(userId);
    }

    public Task<StoryWithMissionsDto?> GetStory(string storyUrl, int userId)
    {
        return _storyRepository.GetStory(storyUrl, userId);
    }

    public Task<IEnumerable<MissionsListDto>> GetMissions(string storyUrl, int userId)
    {
        return _storyRepository.GetMissions(storyUrl, userId);
    }

    public Task<MissionsListDto?> GetMission(string missionUrl, int userId)
    {
        return _storyRepository.GetMission(missionUrl, userId);
    }

    public Task SaveGameProgress(GameProgressDto data, int userId, string gameUrl)
    {
        return _storyRepository.SaveGameProgress(data, userId, gameUrl);
    }
}