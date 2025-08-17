using AirAlmatyFlights.Application.Interfaces.Persistence;
using AirAlmatyFlights.Application.Interfaces.Repositories;
using AirAlmatyFlights.Application.Options;
using AirAlmatyFlights.Infrastructure.Persistence;
using AirAlmatyFlights.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirAlmatyFlights.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(configuration.GetSection(AppConfig.SectionName).GetConnectionString(nameof(AppConfig.ConnectionStrings.DbConnection))));
        return services;
    }
    public static IServiceCollection ConfigureInfrastructureRedisCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options => { options.Configuration = configuration.GetSection(AppConfig.SectionName).GetConnectionString(nameof(AppConfig.ConnectionStrings.Redis)); });
        return services;
    }

    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDataContext, DataContext>();

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();

        return services;
    }
}
