using System.Collections.Immutable;
using KingKarel.Database;
using KingKarel.Database.Entities;
using KingKarel.Dto;
using KingKarel.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace KingKarel.Repository;

public class StoryRepository : IStoryRepository
{
    private readonly KingKarelDbContext _dbContext;
    private readonly ILogger<StoryRepository> _logger;

    public StoryRepository(KingKarelDbContext dbContext, ILogger<StoryRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<StoryDto>> GetStories()
    {
        return (await _dbContext.Stories.Include(s => s.Missions).ToListAsync().ConfigureAwait(false))
            .Select(GetStoryDto).ToImmutableList();
    }

    public async Task<IEnumerable<StoryWithMissionsDto>> GetStoriesStats(int userId)
    {
        var storiesWithMissions = new List<StoryWithMissionsDto>();
        var stories = await _dbContext.Stories.ToListAsync().ConfigureAwait(false);

        foreach (var story in stories)
        {
            var missions = (await GetMissions(story.Url, userId)).ToList();
            var storyWithMissions = GetStoryWithMissionsDto(story, missions);
            storiesWithMissions.Add(storyWithMissions);
        }

        return storiesWithMissions;
    }

    public async Task<StoryWithMissionsDto?> GetStory(string storyUrl, int userId)
    {
        Story? storyRecord =
            await _dbContext.Stories.FirstOrDefaultAsync(s => s.Url == storyUrl);

        var missions = (await GetMissions(storyUrl, userId)).ToList();

        return storyRecord is null ? null : GetStoryWithMissionsDto(storyRecord, missions);
    }

    public Task<IEnumerable<MissionsListDto>> GetMissions(string storyUrl, int userId)
    {
        var missions = _dbContext.Stories
            .Include(s => s.Missions)
            .FirstOrDefault(s => s.Url == storyUrl)
            ?.Missions;

        if (missions is null)
        {
            return Task.FromResult<IEnumerable<MissionsListDto>>(new List<MissionsListDto>());
        }

        var queryData = missions.OrderBy(m => m.Order)
            .GroupJoin(
                _dbContext.GameProgresses.Where(progress => progress.User.Id == userId),
                m => m.Id,
                gp => gp.Game.Id,
                (mission, progresses) => GetMissionDtoFromGroupJoin(mission, progresses.SingleOrDefault())
            ).ToList();

        return Task.FromResult<IEnumerable<MissionsListDto>>(queryData);
    }

    public async Task<MissionsListDto?> GetMission(string missionUrl, int userId)
    {
        Mission? missionRecord =
            await _dbContext.Missions.FirstOrDefaultAsync(m => m.Url == missionUrl);

        if (missionRecord is null)
        {
            return null;
        }

        GameProgress? gameProgressRecord = null;

        if (missionRecord is Game)
        {
            gameProgressRecord =
                await _dbContext.GameProgresses.FirstOrDefaultAsync(p =>
                    p.Game.Id == missionRecord.Id && p.User.Id == userId);
        }

        return GetMissionDtoFromGroupJoin(missionRecord switch
        {
            Game game => game,
            Learning learning => learning,
            _ => throw new ArgumentOutOfRangeException(nameof(missionRecord))
        }, gameProgressRecord);
    }

    public async Task SaveGameProgress(GameProgressDto data, int userId, string gameUrl)
    {
        GameProgress? gameProgressRecord =
            await _dbContext.GameProgresses.FirstOrDefaultAsync(gp => gp.User.Id == userId && gp.Game.Url == gameUrl);

        // create new one
        if (gameProgressRecord is null)
        {
            GameProgress gameProgress = new()
            {
                Commands = data.Commands,
                Speed = data.Speed,
                Size = data.Size,
                Completed = data.Completed
            };

            await _dbContext.GameProgresses.AddAsync(gameProgress);
            Console.WriteLine("add");
        }
        // update old one
        else
        {
            gameProgressRecord.Commands = data.Commands;
            gameProgressRecord.Speed = data.Speed;
            gameProgressRecord.Size = data.Size;
            gameProgressRecord.Completed = data.Completed;
            _dbContext.GameProgresses.Update(gameProgressRecord);
            Console.WriteLine("update");
        }

        try
        {
            int i = await _dbContext.SaveChangesAsync();
            Console.WriteLine($"save {i}");
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not save to the database");
        }
    }

    private static StoryDto GetStoryDto(Story story) =>
        new(story.Id, story.Url, story.Name, story.Description, story.Missions.Count);

    private static StoryWithMissionsDto GetStoryWithMissionsDto(Story story, List<MissionsListDto> missions) =>
        new(story.Id, story.Url, story.Name, story.Description, missions);

    private static MissionsListDto GetMissionDtoFromGroupJoin(Mission mission, GameProgress? gameProgress)
    {
        return new MissionsListDto(
            mission is Game game ? GetGameMissionDto(game, gameProgress) : null,
            mission is Learning learning ? GetLearningMissionDto(learning) : null
        );
    }

    private static GameDto GetGameMissionDto(Game game, GameProgress? gameProgress) =>
        new
        (
            game.Url, game.Name, game.Description,
            game.TaskDescription,
            game.CommandsInitial, gameProgress?.Commands ?? "",
            game.BoardInitial, game.BoardResult,
            game.SpeedLimit, gameProgress?.Speed ?? 0,
            game.SizeLimit, gameProgress?.Size ?? 0,
            game.RobotInitial, game.RobotResult,
            gameProgress?.Completed ?? false
        );

    private static LearningDto GetLearningMissionDto(Learning learning) =>
        new(
            learning.Url,
            learning.Name,
            learning.Description,
            learning.Data,
            learning.IsStory
        );
}