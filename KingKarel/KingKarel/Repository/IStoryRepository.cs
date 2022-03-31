using KingKarel.Dto;

namespace KingKarel.Repository;

public interface IStoryRepository
{
    public Task<IEnumerable<StoryDto>> GetStories();
    public Task<StoryDto?> GetStory(string storyUrl);

    public Task<IEnumerable<MissionsListDto>> GetMissions(string storyUrl, int userId);
    public Task<MissionsListDto?> GetMission(string missionUrl, int userId);

    public Task SaveGameProgress(GameProgressDto data, int userId, string gameUrl);
}