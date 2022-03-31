using KingKarel.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingKarel.Database.Mappers;

internal sealed class GameMapper : IEntityMapper<Game>
{
    public void Map(EntityTypeBuilder<Game> builder)
    {
        // builder.HasKey(v => v.Id);
    }
}
