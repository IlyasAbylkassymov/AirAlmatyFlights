using FluentValidation;

namespace AirAlmatyFlights.Application.Authorization.Queries;

public class GetRoleCodeByCredentialsQueryValidator : AbstractValidator<GetRoleCodeByCredentialsQuery>
{
    public GetRoleCodeByCredentialsQueryValidator()
    {
        RuleFor(q=>q.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty nor null");
        RuleFor(q => q.Password).NotEmpty().NotNull().WithMessage("Password cannot be empty nor null");
    }
}
