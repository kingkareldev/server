using KingKarel.Database;
using KingKarel.Database.Entities;
using KingKarel.Dto;
using KingKarel.Repository.Contract;
using Microsoft.EntityFrameworkCore;

namespace KingKarel.Repository;

public class UserRepository : IUserRepository
{
    private readonly KingKarelDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(KingKarelDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<UserWithHashDto?> GetUser(int id)
    {
        User? user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            return null;
        }

        return GetUserWithHashDto(user);
    }

    public async Task<UserWithHashDto?> GetUserByUsername(string username)
    {
        User? user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user is null)
        {
            return null;
        }

        return GetUserWithHashDto(user);
    }

    public async Task<UserWithHashDto?> CreateUser(RegisterDto userDto, string passwordHash)
    {
        User user = new()
        {
            Name = userDto.Name,
            Username = userDto.Username,
            Email = userDto.Email,
            Description = userDto.Description,
            PasswordHash = passwordHash
        };

        await _dbContext.Users.AddAsync(user);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not save to the database");
            return null;
        }

        return GetUserWithHashDto(user);
    }

    private static UserWithHashDto GetUserWithHashDto(User user) =>
        new(new UserDto(user.Id, user.Name, user.Username, user.Email, user.Description), user.PasswordHash);
}