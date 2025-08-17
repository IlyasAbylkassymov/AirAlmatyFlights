using AirAlmatyFlights.Application.Common.Constants;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AirAlmatyFlights.Api.Controller;

[ApiController]
[ProducesResponseType(statusCode: (int)HttpStatusCode.BadRequest, type: typeof(ProblemDetails))]
[ProducesResponseType(statusCode: (int)HttpStatusCode.NotFound, type: typeof(ProblemDetails))]
[ProducesResponseType(statusCode: (int)HttpStatusCode.ServiceUnavailable, type: typeof(ProblemDetails))]
[ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(ProblemDetails))]
public class BaseController : ControllerBase
{
    protected ISender Mediator => HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult ErrorResponse(Error error)
    {
        return error.Code switch
        {
            ErrorCode.DatabaseError => Problem(title: "Database error",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.ServiceUnavailable),
            ErrorCode.NotFound => Problem(title: "Couldn't find data",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.NotFound),
            _ => Problem(title: "Unhandled exception",
                detail: error.Message,
                statusCode: (int)HttpStatusCode.InternalServerError),
        };
    }
}
