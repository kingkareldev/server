namespace KingKarel.Database.Entities;

public abstract class Mission
{
    public int Id { get; set; }

    public Story Story { get; set; }

    public string Url { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public string Description { get; set; }
}