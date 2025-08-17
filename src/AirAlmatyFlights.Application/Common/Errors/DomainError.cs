using AirAlmatyFlights.Application.Common.Constants;
using KDS.Primitives.FluentResult;

namespace AirAlmatyFlights.Application.Common.Errors;

public static class DomainError
{
    public static Error NotFound => new(ErrorCode.NotFound, "Entity not found in database");
    public static Error DatabaseFailed => new(ErrorCode.DatabaseError, "Database error occured");
}
