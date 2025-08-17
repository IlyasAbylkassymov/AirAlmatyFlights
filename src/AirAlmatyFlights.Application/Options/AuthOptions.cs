namespace AirAlmatyFlights.Application.Options;

public class AuthOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public int TokenLifeExpirationInHour { get; set; }
}
