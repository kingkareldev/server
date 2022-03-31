using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using KingKarel.Dto;
using KingKarel.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace KingKarel.Helpers;

public class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
}

public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;

    public CustomAuthenticationHandler(
        IOptionsMonitor<BasicAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IJwtService jwtService,
        IUserService userService
    ) : base(options, logger, encoder, clock)
    {
        _jwtService = jwtService;
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        string authorizationHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.NoResult();
        }

        if (!authorizationHeader.StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        string token = authorizationHeader.Substring("Bearer".Length).Trim();
        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        try
        {
            return await ValidateToken(token);
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }

    private async Task<AuthenticateResult> ValidateToken(string token)
    {
        JwtSecurityToken? jwtToken = _jwtService.ValidateJwtToken(token);
        if (jwtToken is null)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        UserDto? currentUser = await _userService.GetUser(userId);
        if (currentUser is null)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var claims = new List<Claim>
        {
            new("id", userId.ToString())
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new System.Security.Principal.GenericPrincipal(identity, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
}