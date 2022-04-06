namespace KingKarel.Helpers;

public class AppSettings
{
    public const string ConfigName = "AppSettings";

    public string DatabaseUrl { get; set; }
    public string JwtSecret { get; set; }
    public List<string> CorsOrigins { get; set; }
}