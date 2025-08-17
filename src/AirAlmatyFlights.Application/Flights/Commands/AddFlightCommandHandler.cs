using AirAlmatyFlights.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Flights.Commands;

public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, Result>
{
    private readonly IFlightRepository _flightRepository;

    public AddFlightCommandHandler(IFlightRepository flightRepository) => _flightRepository = flightRepository;
    public async Task<Result> Handle(AddFlightCommand request, CancellationToken cancellationToken)
        => await _flightRepository.AddFlight(new DTO.FlightDto(request.Origin, request.Destination, request.Departure, request.Arrival, request.Status, request.Username));
}
