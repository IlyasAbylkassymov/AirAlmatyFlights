using FluentValidation;

namespace AirAlmatyFlights.Application.Authorization.Commands;

public class CreateUserTokenCommandValidator : AbstractValidator<CreateUserTokenCommand>
{
    public CreateUserTokenCommandValidator()
    {
        RuleFor(q => q.Username).NotEmpty().NotNull().WithMessage("Username cannot be empty nor null.");
        RuleFor(q => q.RoleCode).NotEmpty().NotNull().WithMessage("RoleCode cannot be empty nor null. GetRoleByCredentials logic possibly compromised.");
        RuleFor(q => q.SecretKey).NotEmpty().NotNull().WithMessage("SecretKey cannot be empty nor null. Application settings possibly corrupted.");
        RuleFor(q => q.TokenLifeExpirationInHours).NotEqual(0).WithMessage("TokenLifeExpirationInHours cannot be 0. Application settings possibly corrupted.");
    }
}
