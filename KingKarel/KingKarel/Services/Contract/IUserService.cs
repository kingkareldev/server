using KingKarel.Dto;

namespace KingKarel.Services;

public interface IUserService
{
    public Task<UserDto?> LoginUser(LoginDto loginData);

    public Task<UserDto?> RegisterUser(RegisterDto registerData);
    
    public Task<UserDto?> GetUser(int id);

    public Task<UserDto?> GetUserByUsername(string username);
}