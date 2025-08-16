using AirAlmatyFlights.Api.Common.Constants;

namespace AirAlmatyFlights.Api.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILoggerFactory _loggerFactory;

    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logger = _loggerFactory.CreateLogger<LoggingMiddleware>();
        var scope = new Dictionary<string, object>
        {
            { LogKey.RequestedWith, context.Request.Headers[HeaderConstant.RequestedWith].ToString() },
        };
        using (logger.BeginScope(scope))
        {
            await _next(context);
        }
    }
}
