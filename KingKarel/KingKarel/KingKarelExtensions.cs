using KingKarel.Helpers;
using KingKarel.Repository;
using KingKarel.Repository.Contract;
using KingKarel.Services;
using KingKarel.Services.Contract;

namespace KingKarel;

public static class KingKarelExtensions
{
    public static IServiceCollection AddKingKarelOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection(AppSettings.ConfigName));

        return services;
    }

    public static IServiceCollection AddKingKarelServices(this IServiceCollection services)
    {
        services
            // Repositories
            .AddScoped<IStoryRepository, StoryRepository>()
            .AddScoped<IUserRepository, UserRepository>()

            // Services
            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IStoryService, StoryService>()
            ;

        return services;
    }
}