using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Authorization.Queries;

public record GetRoleCodeByCredentialsQuery(string Username, string Password) : IRequest<Result<string>> { }
