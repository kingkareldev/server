namespace KingKarel.Database.Entities;

public class Story
{
    public int Id { get; set; }
    
    public ICollection<Mission> Missions { get; set; }

    public string Url { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
