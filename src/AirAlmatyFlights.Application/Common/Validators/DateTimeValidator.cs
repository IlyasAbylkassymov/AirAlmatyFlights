using FluentValidation;

namespace AirAlmatyFlights.Application.Common.Validators;

public class DateTimeValidator : AbstractValidator<string>
{
    public DateTimeValidator()
    {
        RuleFor(x => x).NotEmpty().NotNull().WithMessage("Date cannot be empty nor null.")
            .Matches(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$")
            .WithMessage("Invalid DateTime format");
    }
}
