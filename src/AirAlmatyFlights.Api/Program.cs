using AirAlmatyFlights.Api.Common.Extensions;
using AirAlmatyFlights.Api.Common.WebHostOptions;
using AirAlmatyFlights.Api.Middleware;
using AirAlmatyFlights.Api.Swagger;
using AirAlmatyFlights.Api.Versioning;
using AirAlmatyFlights.Application;
using AirAlmatyFlights.Application.Options;
using AirAlmatyFlights.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

try
{
    var webHostOptions = new WebHostOptions
        (builder.Configuration.GetValue<string>(WebHostOptions.InstanceSectionName) ?? throw new Exception("can't get instance name section"),
        builder.Configuration.GetValue<string>(WebHostOptions.WebAddressSectionName) ?? throw new Exception("can't get web address name section"));

    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);
    
    builder.ConfigureHostVersioning();
    builder.ConfigureWebHost();
    builder.Services.ConfigureApplicationAssemblies();

    builder.Services
        .ConfigureInfrastructurePersistence(builder.Configuration)
        .ConfigureInfrastructureRedisCache(builder.Configuration)
        .ConfigureInfrastructureServices();

    builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("f2a1ed52710d4533bde25be6da03b6e3"))
            };
        });
    builder.Services.Configure<AppConfig>(builder.Configuration.GetSection(AppConfig.SectionName));
    builder.Services.AddAuthorization();

    var app = builder.Build();

    Log.Information("{Msg} ({ApplicationName})...",
        "Builing project",
        webHostOptions.InstanceName);

    app.UseConfiguredSwagger();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<LoggingMiddleware>();
    app.UseMiddleware<ExceptionHandleMiddleware>();
    app.MapHealthChecks("/healthcheck");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed with message: {msg}!",
        ex.Message);
}
finally
{
    Log.Information("{Msg}!", "Log close and flush initiated");
    await Log.CloseAndFlushAsync();
}


