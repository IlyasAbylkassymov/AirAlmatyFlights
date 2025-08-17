using AirAlmatyFlights.Application.Interfaces.Repositories;
using AirAlmatyFlights.Domain.Entities;
using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Flights.Queries;

public class GetFlightListQueryHandler : IRequestHandler<GetFlightListQuery, Result<IEnumerable<Flight>>>
{
    private readonly IFlightRepository _flightRepository;

    public GetFlightListQueryHandler(IFlightRepository flightRepository) => _flightRepository = flightRepository;

    public async Task<Result<IEnumerable<Flight>>> Handle(GetFlightListQuery request, CancellationToken cancellationToken)
        => await _flightRepository.GetFlightList(request.Origin, request.Destination, request.Username);
}
