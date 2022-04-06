using BCrypt.Net;
using KingKarel.Dto;
using KingKarel.Repository;
using KingKarel.Repository.Contract;
using KingKarel.Services.Contract;

namespace KingKarel.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
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
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
        catch (SaltParseException e)
        {
            _logger.LogError(e, "Salt couldn't been parsed");
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Password verification failed");
        }

        return false;
    }
}