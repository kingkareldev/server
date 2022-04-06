using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KingKarel.Dto;
using KingKarel.Helpers;
using KingKarel.Services.Contract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KingKarel.Services;

public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;
    private readonly ILogger<JwtService> _logger;

    public JwtService(IOptions<AppSettings> appSettings, ILogger<JwtService> logger)
    {
        _appSettings = appSettings.Value;
        _logger = logger;
    }

    public string GenerateJwtToken(UserDto user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_appSettings.JwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public JwtSecurityToken? ValidateJwtToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.JwtSecret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
        catch (Exception e)
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
            _logger.LogInformation(e, "Invalid Jwt token {Token}", token);
        }

        return null;
    }
}