using AirAlmatyFlights.Application.DTO;
using AirAlmatyFlights.Domain.Common.Enums;
using AirAlmatyFlights.Domain.Entities;
using KDS.Primitives.FluentResult;

namespace AirAlmatyFlights.Application.Interfaces.Repositories;

public interface IFlightRepository
{
    Task<Result<IEnumerable<Flight>>> GetFlightList(string? origin, string? destination, string userName);
    Task<Result> AddFlight(FlightDto request);
    Task<Result> UpdateFlight(int id, Status status, string userName);
}
