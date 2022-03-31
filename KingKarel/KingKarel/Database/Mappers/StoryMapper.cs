using KingKarel.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingKarel.Database.Mappers;

internal sealed class StoryMapper : IEntityMapper<Story>
{
    public void Map(EntityTypeBuilder<Story> builder)
    {
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.Url).IsUnique();
    }
}