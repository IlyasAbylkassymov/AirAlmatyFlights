using AirAlmatyFlights.Api.Requests.Authorization;
using AirAlmatyFlights.Application.Authorization.Commands;
using AirAlmatyFlights.Application.Authorization.Queries;
using AirAlmatyFlights.Application.Options;
using Asp.Versioning;
using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AirAlmatyFlights.Api.Controller.v1;

[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
public class AuthorizationController : BaseController
{
    private readonly AppConfig _appConfig;

    public AuthorizationController(IOptions<AppConfig> appConfigOptions) => _appConfig = appConfigOptions.Value;

    [HttpPost("token")]
    [ProducesResponseType(typeof(Result<string>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var roleCodeResult = await Mediator.Send(new GetRoleCodeByCredentialsQuery(loginRequest.Username, loginRequest.Password));
        if (roleCodeResult.IsFailed)
            return ErrorResponse(roleCodeResult.Error);

        var tokenResult = await Mediator.Send(new CreateUserTokenCommand(loginRequest.Username, _appConfig.AuthOptions.SecretKey, roleCodeResult.Value, _appConfig.AuthOptions.TokenLifeExpirationInHour));
        if (tokenResult.IsFailed)
            return ErrorResponse(tokenResult.Error);

        return Ok(tokenResult);
    }
}
