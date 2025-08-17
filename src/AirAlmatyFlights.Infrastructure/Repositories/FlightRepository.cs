using AirAlmatyFlights.Application.Common.Errors;
using AirAlmatyFlights.Application.DTO;
using AirAlmatyFlights.Application.Interfaces.Repositories;
using AirAlmatyFlights.Application.Options;
using AirAlmatyFlights.Domain.Common.Enums;
using AirAlmatyFlights.Domain.Entities;
using AirAlmatyFlights.Infrastructure.Common.Exceptions;
using AirAlmatyFlights.Infrastructure.Persistence;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace AirAlmatyFlights.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly DataContext _dataContext;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<FlightRepository> _logger;
    private readonly AppConfig _appConfig;

    public FlightRepository(DataContext dataContext, IDistributedCache distributedCache, ILogger<FlightRepository> logger, IOptions<AppConfig> appConfigOptions)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _appConfig = appConfigOptions.Value;
    }

    public async Task<Result<IEnumerable<Flight>>> GetFlightList(string origin, string destination, string userName)
    {
        IEnumerable<Flight> result = Enumerable.Empty<Flight>();
        try
        {
            var cacheKey = new StringBuilder(_appConfig.RedisOptions.GetFlightList).Append(userName).ToString();
            var cacheFlightList = await _distributedCache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheFlightList))
                return Result.Success(JsonConvert.DeserializeObject<IEnumerable<Flight>>(cacheFlightList)
                                      ?? throw new Exception(nameof(cacheFlightList)));

            result = await _dataContext.Flights
                .Where(f => f.Origin == origin && f.Destination == destination)
                .OrderBy(f => f.Arrival)
                .ToListAsync();
            if (!result.Any())
            {
                _logger.LogError("{Message} {Action} {Date}",
                    "Couldn't get data by request", nameof(GetFlightList), DateTime.Now);
                return Result.Failure<IEnumerable<Flight>>(DomainError.NotFound);
            }

            await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(result),
                new DistributedCacheEntryOptions { AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(_appConfig.RedisOptions.ExpirationInSeconds) });
        }
        catch (DatabaseException ex)
        {
            _logger.LogError("{Message} {Action} {Date} {Exception}",
                "Database error", nameof(GetFlightList), DateTime.Now, ex.Message);
            return Result.Failure<IEnumerable<Flight>>(DomainError.DatabaseFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message} {Action} {Date} {Exception}",
                "Database request error", nameof(GetFlightList), DateTime.Now, ex.Message);
            return Result.Failure<IEnumerable<Flight>>(DomainError.DatabaseFailed);
        }

        _logger.LogInformation("{Message} {Action}", "Data fetched successfully", nameof(GetFlightList));

        return Result.Success(result);
    }

    public async Task<Result> AddFlight(FlightDto request)
    {
        try
        {
            var model = new Flight
            {
                Origin = request.Origin,
                Destination = request.Destination,
                Arrival = request.Arrival,
                Departure = request.Departure,
                Status = request.Status
            };

            await _dataContext.AddAsync(model);
            if(await _dataContext.SaveChangesAsync() == 0)
                return Result.Failure(DomainError.DatabaseFailed);
        }
        catch (DatabaseException ex)
        {
            _logger.LogError("{Message} {Action} {UserName} {Date} {Exception}",
                "Database error", nameof(AddFlight), request.UserName, DateTime.Now, ex.Message);
            return Result.Failure(DomainError.DatabaseFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message} {Action} {UserName} {Date} {Exception}",
                "Database request error", nameof(AddFlight), request.UserName, DateTime.Now, ex.Message);
            return Result.Failure(DomainError.DatabaseFailed);
        }

        _logger.LogInformation("{Message} {Action} {UserName} {Date}",
            "Data added successfully", nameof(AddFlight), request.UserName, DateTime.Now);

        return Result.Success();
    }

    public async Task<Result> UpdateFlight(int id, Status status, string userName)
    {
        try
        {
            var updateData = await _dataContext.Flights
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
            if (updateData is null)
            {
                _logger.LogError("{Message} {Action} {UserName} {Date}",
                    "Couldn't get data by request", nameof(UpdateFlight), userName, DateTime.Now);
                return Result.Failure(DomainError.NotFound);
            }

            updateData.Status = status;
            if (await _dataContext.SaveChangesAsync() == 0)
                return Result.Failure(DomainError.DatabaseFailed);
        }
        catch (DatabaseException ex)
        {
            _logger.LogError("{Message} {Action} {UserName} {Date} {Exception}",
                "Database error", nameof(UpdateFlight), userName, DateTime.Now, ex.Message);
            return Result.Failure(DomainError.DatabaseFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError("{Message} {Action} {UserName} {Date} {Exception}",
                "Database request error", nameof(UpdateFlight), userName, DateTime.Now, ex.Message);
            return Result.Failure(DomainError.DatabaseFailed);
        }

        _logger.LogInformation("{Message} {Action} {UserName} {Date}",
            "Data updated successfully", nameof(UpdateFlight), userName, DateTime.Now);

        return Result.Success();
    }
}
