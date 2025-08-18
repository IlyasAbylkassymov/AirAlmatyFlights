using AirAlmatyFlights.Application.DTO;
using AirAlmatyFlights.Application.Flights.Commands;
using AirAlmatyFlights.Application.Flights.Queries;
using AirAlmatyFlights.Application.Interfaces.Repositories;
using AirAlmatyFlights.Domain.Common.Enums;
using AirAlmatyFlights.Domain.Entities;
using KDS.Primitives.FluentResult;

namespace AirAlmatyFlights.Tests;

public class MockFlightRepository : IFlightRepository
{
    public async Task<Result> AddFlight(FlightDto request)
    {
        await Task.Yield();
        return Result.Success();
    }

    public async Task<Result<IEnumerable<Flight>>> GetFlightList(string? origin, string? destination, string userName)
    {
        await Task.Yield();
        IEnumerable<Flight> flights = new Flight[1];
        return Result.Success(flights);
    }

    public async Task<Result> UpdateFlight(int id, Status status, string userName)
    {
        await Task.Yield();
        return Result.Success();
    }
}

public class FlightTests
{
    [Fact]
    public async Task AddFlightCommandTest()
    {
        var mfr = new MockFlightRepository();
        var afch = new AddFlightCommandHandler(mfr);
        var result = await afch.Handle(new AddFlightCommand("test", "test", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddHours(5), Status.InTime, "test"), CancellationToken.None);
        Assert.True(result.IsSuccess);
    }


    [Fact]
    public async Task UpdateFlightCommandTest()
    {
        var mfr = new MockFlightRepository();
        var ufch = new UpdateFlightCommandHandler(mfr);
        var result = await ufch.Handle(new UpdateFlightCommand(1, Status.InTime, "test"), CancellationToken.None);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetFlightListQueryHandlerTest()
    {
        var mfr = new MockFlightRepository();
        var gflqh = new GetFlightListQueryHandler(mfr);
        var result = await gflqh.Handle(new GetFlightListQuery("test", "test", "test"), CancellationToken.None);
        Assert.True(result.IsSuccess);
    }
}
