using System.Text;
using KingKarel.Database;
using KingKarel.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace KingKarel;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();

        services.AddCors(
            options => options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(appSettings.CorsOrigins.ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    ;
            }));
        services.AddControllers();

        var key = Encoding.UTF8.GetBytes(appSettings.JwtSecret);
        services
            .AddAuthentication("Basic")
            .AddScheme<KingKarelAuthOptions, KingKarelAuthHandler>("Basic", null);

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes("Basic")
                    .RequireAuthenticatedUser()
                    .Build()
                ;
        });

        var connectionString =
            PsqlConnectionStringParser.GetEfConnectionString(appSettings.DatabaseUrl);
        services
            // DB
            .AddDbContext<KingKarelDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("KingKarel"))
            )

            // Swagger
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KingKarel",
                    Version = "v1",
                    Description = "KingKarel Web API"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
                //
            })

            // King Karel
            .AddKingKarelOptions(Configuration)
            .AddKingKarelServices()
            ;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(delegate(SwaggerUIOptions c)
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "KingKarel v1");
                //c.RoutePrefix = string.Empty;
            });
        }
        else
        {
            UpdateDatabase(app);
            //app.UseExceptionHandler("/error");
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseKingKarelMiddlewares();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    private static void UpdateDatabase(IApplicationBuilder app)
    {
        using IServiceScope serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using KingKarelDbContext? context = serviceScope.ServiceProvider.GetService<KingKarelDbContext>();
        context?.Database.Migrate();
    }
}