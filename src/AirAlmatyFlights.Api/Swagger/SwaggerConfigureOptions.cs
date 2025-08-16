using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace AirAlmatyFlights.Api.Swagger;

public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider) => _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            try
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo
        {
            Title = "API для получению данных о статусе рейсов",
            Version = GetAssemblyVersion(),
            Description = GetSwaggerDescription(),
        };

        if (description.IsDeprecated)
        {
            info.Description += " Версия API не поддерживается.";
        }

        return info;
    }

    private static string GetAssemblyVersion()
    {
        return Assembly.GetEntryAssembly()
                      ?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                      ?.InformationalVersion ?? "undefined";
    }

    private static string GetSwaggerDescription()
    {
        var path = Path.Combine(AppContext.BaseDirectory, "SwaggerDescription.xml");

        try
        {
            return File.ReadAllText(path);
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}
