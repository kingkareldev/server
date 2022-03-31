using KingKarel.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingKarel.Database.Mappers;

internal sealed class MissionMapper : IEntityMapper<Mission>
{
    public void Map(EntityTypeBuilder<Mission> builder)
    {
        builder.HasKey(m => m.Id);
        builder.HasIndex(m => m.Url).IsUnique();
    }
}
