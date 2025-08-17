using FluentValidation;

namespace AirAlmatyFlights.Application.Flights.Queries;

public class GetFlightListQueryValidator : AbstractValidator<GetFlightListQuery>
{
    public GetFlightListQueryValidator()
    {
        RuleFor(q=>q.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty nor null.");
        RuleFor(q => q.Origin).NotEmpty().NotNull().WithMessage("Origin cannot be empty nor null.");
        RuleFor(q => q.Destination).NotEmpty().NotNull().WithMessage("Destination cannot be empty nor null.");
    }
}
