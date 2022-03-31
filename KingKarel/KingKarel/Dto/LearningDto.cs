namespace KingKarel.Dto;

public record LearningDto(
    string Url, string Name, string Description,
    string Data,
    bool IsStory
) : MissionDto(Url, Name, Description);