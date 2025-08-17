using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Authorization.Commands;

public record CreateUserTokenCommand(string Username, string SecretKey, string RoleCode, int TokenLifeExpirationInHours) : IRequest<Result<string>> { }

