using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;

namespace AirAlmatyFlights.Application.Common.Exceptions;

public class RequestValidationException : ValidationException
{
    public RequestValidationException() : base("Parameter validation exception: ")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public RequestValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
