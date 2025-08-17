using AirAlmatyFlights.Domain.Common.Enums;

namespace AirAlmatyFlights.Application.DTO;
public record FlightDto(string Origin, string Destination, DateTimeOffset Departure, DateTimeOffset Arrival, Status Status, string UserName);