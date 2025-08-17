using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AirAlmatyFlights.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApplicationAssemblies(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
