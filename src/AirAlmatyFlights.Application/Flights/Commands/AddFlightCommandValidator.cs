using AirAlmatyFlights.Application.Common.Validators;
using FluentValidation;

namespace AirAlmatyFlights.Application.Flights.Commands;

public class AddFlightCommandValidator : AbstractValidator<AddFlightCommand>
{
    public AddFlightCommandValidator()
    {
        RuleFor(q => q.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty nor null.");
        RuleFor(q => q.Origin).NotEmpty().NotNull().WithMessage("Origin cannot be empty nor null.");
        RuleFor(q => q.Destination).NotEmpty().NotNull().WithMessage("Destination cannot be empty nor null.");
        RuleFor(q => q.Status).NotNull().NotEmpty().WithMessage("Status cannot be empty nor null.");
        RuleFor(q => q.Departure.ToString()).SetValidator(new DateTimeValidator());
        RuleFor(q => q.Arrival.ToString()).SetValidator(new DateTimeValidator());
        RuleFor(q => q.Arrival).GreaterThan(q => q.Departure).WithMessage("Arrival time must be later than Departure time");
    }
}
