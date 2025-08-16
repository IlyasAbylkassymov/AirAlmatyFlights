using AirAlmatyFlights.Api.Common.Extensions;
using AirAlmatyFlights.Application.Common.Exceptions;
using System.Net;

namespace AirAlmatyFlights.Api.Middleware;

public class ExceptionHandleMiddleware
{
    private readonly ILogger<ExceptionHandleMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandleMiddleware(RequestDelegate next, ILogger<ExceptionHandleMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RequestValidationException ex)
        {
            _logger.LogError(ex, "{message}", "Invalid request parameters");

            var problem = ex.GenerateProblemDetails(context,
                "Invalid request parameters",
                HttpStatusCode.BadRequest);

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", "Unhandled exception");

            var problem = ex.GenerateProblemDetails(context,
                "Unhandled exception",
                HttpStatusCode.InternalServerError);

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
