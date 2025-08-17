namespace AirAlmatyFlights.Application.Options;

public class RedisOptions
{
    public string GetFlightList { get; set; } = string.Empty;
    public int ExpirationInSeconds { get; set; }
}
