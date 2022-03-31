namespace KingKarel.Dto;

public record UserWithHashDto(UserDto user, string PasswordHash)
{
    UserDto WithoutPasswordHash()
    {
        return user;
    }
}