using AirAlmatyFlights.Domain.Entities;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Flights.Queries;

public record GetFlightListQuery(string? Origin, string? Destination, string Username) : IRequest<Result<IEnumerable<Flight>>> { }
