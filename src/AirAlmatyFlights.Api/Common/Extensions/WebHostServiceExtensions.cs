using AirAlmatyFlights.Api.Common.Constants;
using AirAlmatyFlights.Api.Common.WebHostOptions;
using AirAlmatyFlights.Api.Swagger;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AirAlmatyFlights.Api.Common.Extensions;

public static class WebHostServiceExtensions
{
    public static WebApplicationBuilder ConfigureWebHost(this WebApplicationBuilder builder)
    {
        if (builder.Environment.EnvironmentName.Equals(EnvironmentConstants.Test))
            builder.WebHost.UseUrls(builder.Configuration[WebHostOptions.WebHostOptions.WebAddressSectionName]);

        builder.Services.AddControllers()
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.Converters.Add(new StringEnumConverter());
                x.SerializerSettings.FloatParseHandling = FloatParseHandling.Decimal;
                x.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
                x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        builder.Services.AddHealthChecks();
        builder.Services.AddConfiguredSwagger();

        return builder;
    }
}
