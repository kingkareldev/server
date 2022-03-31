using KingKarel.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingKarel.Database.Mappers;

internal sealed class GameProgressMapper : IEntityMapper<GameProgress>
{
    public void Map(EntityTypeBuilder<GameProgress> builder)
    {
        builder.HasKey(v => v.Id);
    }
}
