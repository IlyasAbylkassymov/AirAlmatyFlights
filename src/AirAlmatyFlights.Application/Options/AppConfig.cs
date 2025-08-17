namespace AirAlmatyFlights.Application.Options;

public class AppConfig
{
    public const string SectionName = nameof(AppConfig);
    public ConnectionStrings ConnectionStrings { get; set; } = new ConnectionStrings();
    public RedisOptions RedisOptions { get; set; } = new RedisOptions();
    public AuthOptions AuthOptions { get; set; } = new AuthOptions();   
}
