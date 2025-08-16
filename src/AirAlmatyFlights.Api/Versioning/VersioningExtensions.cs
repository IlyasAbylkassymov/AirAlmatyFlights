using Asp.Versioning;
using Serilog;

namespace AirAlmatyFlights.Api.Versioning;

public static class VersioningExtensions
{
    public static WebApplicationBuilder ConfigureHostVersioning(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
                options.ApiVersionParameterSource = new UrlSegmentApiVersionReader());

        Log.Debug("Configiration of {Application}...", builder.Environment.ApplicationName);

        return builder;
    }
}
