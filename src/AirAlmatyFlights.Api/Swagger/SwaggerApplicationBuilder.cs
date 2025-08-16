using Asp.Versioning.ApiExplorer;

namespace AirAlmatyFlights.Api.Swagger;

public static class SwaggerApplicationBuilder
{
    public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider = app.ApplicationServices
            .GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseDeveloperExceptionPage();

        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());

                options.RoutePrefix = "swagger";
            }
        });

        return app;
    }
}
