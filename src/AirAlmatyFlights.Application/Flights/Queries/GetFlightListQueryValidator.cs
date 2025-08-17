using FluentValidation;

namespace AirAlmatyFlights.Application.Flights.Queries;

public class GetFlightListQueryValidator : AbstractValidator<GetFlightListQuery>
{
    public GetFlightListQueryValidator()
    {
        RuleFor(q => q.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty nor null.");
        RuleFor(q => q).Must(q => 
        (string.IsNullOrEmpty(q.Origin) && !string.IsNullOrEmpty(q.Destination)) 
        || (!string.IsNullOrEmpty(q.Origin) && string.IsNullOrEmpty(q.Destination))
        || (!string.IsNullOrEmpty(q.Origin) && !string.IsNullOrEmpty(q.Destination))).WithMessage("At least one of those properties must be not null nor empty: Origin and Destination.");
    }
}
