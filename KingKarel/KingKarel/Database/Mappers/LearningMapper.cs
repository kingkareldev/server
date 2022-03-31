using KingKarel.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingKarel.Database.Mappers;

internal sealed class LearningMapper : IEntityMapper<Learning>
{
    public void Map(EntityTypeBuilder<Learning> builder)
    {
        // builder.HasKey(v => v.Id);
    }
}
