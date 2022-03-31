using KingKarel.Dto;
using KingKarel.Repository;

namespace KingKarel.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> LoginUser(LoginDto loginData)
    {
        var user = await _userRepository.GetUserByUsername(loginData.Username);
        if (user is null || !VerifyPassword(loginData.Password, user.PasswordHash))
        {
            return null;
        }

        return user.user;
    }

    public async Task<UserDto?> RegisterUser(RegisterDto registerData)
    {
        var createdUser = await _userRepository.CreateUser(registerData, HashPassword(registerData.Password));
        return createdUser?.user;
    }

    public async Task<UserDto?> GetUser(int id)
    {
        var user = await _userRepository.GetUser(id);
        return user?.user;
    }

    public async Task<UserDto?> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsername(username);
        return user?.user;
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}