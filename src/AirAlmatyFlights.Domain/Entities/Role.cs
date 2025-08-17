using System.ComponentModel.DataAnnotations.Schema;

namespace AirAlmatyFlights.Domain.Entities;

[Table(name: "Role", Schema = "dbo")]
public class Role : BaseEntity
{
    [Column(name: "code")]
    public string Code { get; set; } = string.Empty;
}
