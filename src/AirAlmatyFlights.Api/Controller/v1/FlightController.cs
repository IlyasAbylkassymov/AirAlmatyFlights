using AirAlmatyFlights.Api.Requests.Flights;
using AirAlmatyFlights.Application.Authorization.Queries;
using AirAlmatyFlights.Application.Common.Constants;
using AirAlmatyFlights.Application.Flights.Commands;
using AirAlmatyFlights.Application.Flights.Queries;
using AirAlmatyFlights.Domain.Common.Enums;
using AirAlmatyFlights.Domain.Entities;
using Asp.Versioning;
using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirAlmatyFlights.Api.Controller.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ProducesResponseType(statusCode: 401, type: typeof(ProblemDetails))]
[ProducesResponseType(statusCode: 403, type: typeof(ProblemDetails))]
public class FlightController : BaseController
{

    /// <summary>
    /// Получить список полетов, отфильтрованный по месту вылета или месту назначения
    /// </summary>
    /// <param name="origin">Место вылета</param>
    /// <param name="destination">Место назначения</param>
    /// <returns>Список полетов</returns>
    [HttpGet("list")]
    [Authorize]
    [ProducesResponseType(statusCode: 200, type: typeof(Result<IEnumerable<Flight>>))]
    public async Task<IActionResult> GetFlightList(string? origin, string? destination)
    {
        var usernameResult = await Mediator.Send(new GetTokenDataQuery(HttpContext.Request.Headers[AuthorizationConstants.AuthorizationHeaderKey]));
        if (usernameResult.IsFailed)
            return ErrorResponse(usernameResult.Error);

        var getFlightListResult = await Mediator.Send(new GetFlightListQuery(origin, destination, usernameResult.Value));
        return getFlightListResult.IsFailed ? ErrorResponse(getFlightListResult.Error) : Ok(getFlightListResult);
    }

    /// <summary>
    /// Добавить новый полет в список (доступно только модераторам)
    /// </summary>
    /// <param name="addFlightRequest">Новый полет</param>
    /// <returns></returns>
    [Authorize(Roles = AuthorizationConstants.ModeratorRoleName)]
    [HttpPost("add")]
    [ProducesResponseType(statusCode: 200, type: typeof(Result))]
    public async Task<IActionResult> AddFlight([FromBody] AddFlightRequest addFlightRequest)
    {
        var usernameResult = await Mediator.Send(new GetTokenDataQuery(HttpContext.Request.Headers[AuthorizationConstants.AuthorizationHeaderKey]));
        if (usernameResult.IsFailed)
            return ErrorResponse(usernameResult.Error);

        var addFlightResult = await Mediator.Send(new AddFlightCommand(addFlightRequest.Origin, addFlightRequest.Destination, addFlightRequest.Departure, addFlightRequest.Arrival, addFlightRequest.Status, usernameResult.Value));
        return addFlightResult.IsFailed ? ErrorResponse(addFlightResult.Error) : Ok(addFlightResult);
    }

    /// <summary>
    /// Изменить статус полета (доступно только модераторам)
    /// </summary>
    /// <param name="id">Идентификатор полета</param>
    /// <param name="status">Новый выбранный статус</param>
    /// <returns></returns>
    [Authorize(Roles = AuthorizationConstants.ModeratorRoleName)]
    [HttpPut("update")]
    [ProducesResponseType(statusCode: 200, type: typeof(Result))]
    public async Task<IActionResult> UpdateFlight(int id, Status status)
    {
        var usernameResult = await Mediator.Send(new GetTokenDataQuery(HttpContext.Request.Headers[AuthorizationConstants.AuthorizationHeaderKey]));
        if (usernameResult.IsFailed)
            return ErrorResponse(usernameResult.Error);

        var updateFlightResult = await Mediator.Send(new UpdateFlightCommand(id, status, usernameResult.Value));
        return updateFlightResult.IsFailed ? ErrorResponse(updateFlightResult.Error) : Ok(updateFlightResult);
    }
}
