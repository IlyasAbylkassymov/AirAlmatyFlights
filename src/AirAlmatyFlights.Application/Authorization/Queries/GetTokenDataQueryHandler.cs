using AirAlmatyFlights.Application.Common.Constants;
using KDS.Primitives.FluentResult;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace AirAlmatyFlights.Application.Authorization.Queries;

public class GetTokenDataQueryHandler : IRequestHandler<GetTokenDataQuery, Result<string>>
{
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public GetTokenDataQueryHandler() => _tokenHandler = new JwtSecurityTokenHandler();
    public async Task<Result<string>> Handle(GetTokenDataQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Token) || !request.Token.StartsWith(AuthorizationConstants.AuthorizationHeaderStart))
            return (Result<string>)Result.Failure(new Error("401", "Authorization token not found"));

        var username = _tokenHandler.ReadJwtToken(request.Token[AuthorizationConstants.AuthorizationHeaderStart.Length..].Trim()).Claims.FirstOrDefault(c => c.Type == ClaimConstants.Name)?.Value;
        await Task.Yield();
        return username is null
            ? (Result<string>)Result.Failure(new Error("403", "Couldn't get username"))
            : Result.Success(username);
    }
}
