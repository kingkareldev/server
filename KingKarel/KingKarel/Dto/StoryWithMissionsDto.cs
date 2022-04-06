namespace KingKarel.Dto;

public record StoryWithMissionsDto(
    int Id, string Url, string Name, string Description,
    List<MissionsListDto> Missions
);