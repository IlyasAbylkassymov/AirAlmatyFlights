using AirAlmatyFlights.Domain.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirAlmatyFlights.Domain.Entities;

[Table(name: "Flight", Schema = "dbo")]
public class Flight : BaseEntity
{
    [Column(name: "origin")]
    public string Origin { get; set; } = string.Empty;

    [Column(name: "destination")]
    public string Destination { get; set; } = string.Empty;

    [Column(name: "departure")]
    public DateTimeOffset Departure { get; set; }

    [Column(name: "arrival")]
    public DateTimeOffset Arrival { get; set; }

    [Column(name: "status")]
    public Status Status { get; set; }
}
