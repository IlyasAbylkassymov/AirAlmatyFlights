using KDS.Primitives.FluentResult;
using MediatR;

namespace AirAlmatyFlights.Application.Authorization.Queries;

public record GetTokenDataQuery(string? Token) : IRequest<Result<string>> { }
