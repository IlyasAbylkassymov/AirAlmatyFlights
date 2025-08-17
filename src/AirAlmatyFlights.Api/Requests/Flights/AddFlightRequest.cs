using AirAlmatyFlights.Domain.Common.Enums;

namespace AirAlmatyFlights.Api.Requests.Flights;

public record AddFlightRequest(string Origin, string Destination, DateTimeOffset Departure, DateTimeOffset Arrival, Status Status) {}
