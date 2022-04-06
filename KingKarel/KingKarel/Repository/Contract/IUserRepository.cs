using KingKarel.Dto;

namespace KingKarel.Repository.Contract;

public interface IUserRepository
{
    public Task<UserWithHashDto?> GetUser(int id);

    public Task<UserWithHashDto?> GetUserByUsername(string username);

    public Task<UserWithHashDto?> CreateUser(RegisterDto userData, string passwordHash);
}