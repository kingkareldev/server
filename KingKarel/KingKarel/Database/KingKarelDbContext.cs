using KingKarel.Database.Entities;
using KingKarel.Database.Mappers;
using Microsoft.EntityFrameworkCore;

#nullable disable
namespace KingKarel.Database;

public class KingKarelDbContext : DbContext
{
    public KingKarelDbContext(DbContextOptions<KingKarelDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new GameMapper().Map(builder.Entity<Game>());
        new GameProgressMapper().Map(builder.Entity<GameProgress>());
        new LearningMapper().Map(builder.Entity<Learning>());
        new MissionMapper().Map(builder.Entity<Mission>());
        new StoryMapper().Map(builder.Entity<Story>());
        new UserMapper().Map(builder.Entity<User>());
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<GameProgress> GameProgresses { get; set; }
    public DbSet<Learning> Learnings { get; set; }
    public DbSet<Mission> Missions { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<User> Users { get; set; }
}