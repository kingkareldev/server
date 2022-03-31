namespace KingKarel.Dto;

public record GameDto(
    string Url, string Name, string Description,
    string CommandsInitial, string Commands,
    string BoardInitial, string BoardResult,
    int SpeedLimit, int Speed,
    int SizeLimit, int Size,
    string RobotInitial, string RobotResult,
    bool Completed
) : MissionDto(Url, Name, Description);