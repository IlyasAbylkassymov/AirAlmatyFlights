using AirAlmatyFlights.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Flights.Commands;

public class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, Result>
{
    private readonly IFlightRepository _flightRepository;

    public UpdateFlightCommandHandler(IFlightRepository flightRepository) => _flightRepository = flightRepository;

    public async Task<Result> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        => await _flightRepository.UpdateFlight(request.Id, request.Status, request.Username);
}
