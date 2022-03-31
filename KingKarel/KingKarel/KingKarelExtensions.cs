using KingKarel.Helpers;
using KingKarel.Repository;
using KingKarel.Services;

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
            ;

        return services;
    }

    public static IApplicationBuilder UseKingKarelMiddlewares(this IApplicationBuilder builder)
    {
        return builder;
    }
}