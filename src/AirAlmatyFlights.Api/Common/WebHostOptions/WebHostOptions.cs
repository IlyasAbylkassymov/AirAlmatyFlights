namespace AirAlmatyFlights.Api.Common.WebHostOptions;

public class WebHostOptions
{
    public WebHostOptions(string instanceName, string webAddress)
    {
        InstanceName = instanceName;
        WebAddress = webAddress;
    }

    public const string InstanceSectionName = "AppConfig:WebHostOptions:InstanceName";
    public const string WebAddressSectionName = "AppConfig:WebHostOptions:WebAddress";
    public string InstanceName { get; init; }
    public string WebAddress { get; init; }
}
