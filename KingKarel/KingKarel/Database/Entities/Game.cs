using System.ComponentModel.DataAnnotations.Schema;

namespace KingKarel.Database.Entities;

[Table("Game")]
public class Game : Mission
{
    public string CommandsInitial { get; set; }
    public string TaskDescription { get; set; }

    public string BoardInitial { get; set; }
    public string BoardResult { get; set; }

    public int SpeedLimit { get; set; }
    public int SizeLimit { get; set; }

    public string RobotInitial { get; set; }
    public string RobotResult { get; set; }
}