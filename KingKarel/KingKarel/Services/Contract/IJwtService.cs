using System.IdentityModel.Tokens.Jwt;
using KingKarel.Dto;

namespace KingKarel.Services.Contract;

public interface IJwtService
{
    public string GenerateJwtToken(UserDto user);
    public JwtSecurityToken? ValidateJwtToken(string token);
}