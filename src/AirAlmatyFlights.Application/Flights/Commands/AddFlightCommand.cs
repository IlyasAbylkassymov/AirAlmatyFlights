using AirAlmatyFlights.Domain.Common.Enums;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Flights.Commands;

public record AddFlightCommand(string Origin, string Destination, DateTimeOffset Departure, DateTimeOffset Arrival, Status Status, string Username) : IRequest<Result> { }
