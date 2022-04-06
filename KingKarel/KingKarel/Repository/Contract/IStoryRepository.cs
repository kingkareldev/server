﻿using KingKarel.Dto;

namespace KingKarel.Repository.Contract;

public interface IStoryRepository
{
    public Task<IEnumerable<StoryDto>> GetStories();
    public Task<IEnumerable<StoryWithMissionsDto>> GetStoriesStats(int userId);
    public Task<StoryWithMissionsDto?> GetStory(string storyUrl, int userId);

    public Task<IEnumerable<MissionsListDto>> GetMissions(string storyUrl, int userId);
    public Task<MissionsListDto?> GetMission(string missionUrl, int userId);

    public Task SaveGameProgress(GameProgressDto data, int userId, string gameUrl);
}