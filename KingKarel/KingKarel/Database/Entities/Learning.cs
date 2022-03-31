using System.ComponentModel.DataAnnotations.Schema;

namespace KingKarel.Database.Entities;

[Table("Learning")]
public class Learning : Mission
{
    public string Data { get; set; }
    public bool IsStory { get; set; }
}