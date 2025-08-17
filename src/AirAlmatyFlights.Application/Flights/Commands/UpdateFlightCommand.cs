using AirAlmatyFlights.Domain.Common.Enums;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Flights.Commands;

public record UpdateFlightCommand(int Id, Status Status, string Username) : IRequest<Result> { }
