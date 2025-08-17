using FluentValidation;

namespace AirAlmatyFlights.Application.Flights.Commands;

public class UpdateFlightCommandValidator : AbstractValidator<UpdateFlightCommand>
{
    public UpdateFlightCommandValidator()
    {
        RuleFor(q => q.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty nor null.");
        RuleFor(q => q.Status).NotNull().NotEmpty().WithMessage("Status cannot be empty nor null.");
        RuleFor(q => q.Id).NotEqual(0).WithMessage("Id cannot be zero.");
    }
}
