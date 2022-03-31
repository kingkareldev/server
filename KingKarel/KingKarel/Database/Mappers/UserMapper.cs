using KingKarel.Database.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingKarel.Database.Mappers;

internal sealed class UserMapper : IEntityMapper<User>
{
    public void Map(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Username).IsUnique();
    }
}
