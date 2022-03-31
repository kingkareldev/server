namespace KingKarel.Database.Entities;

public class GameProgress
{
    public int Id { get; set; }

    public User User { get; set; }
    public Game Game { get; set; }

    public string Commands { get; set; }
    public int Speed { get; set; }
    public int Size { get; set; }
    public bool Completed { get; set; }
}