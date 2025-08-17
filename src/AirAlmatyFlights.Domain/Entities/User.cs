using System.ComponentModel.DataAnnotations.Schema;

namespace AirAlmatyFlights.Domain.Entities;

[Table(name: "User", Schema = "dbo")]
public class User : BaseEntity
{
    [Column(name: "Username")]
    public string Username { get; set; } = string.Empty;

    [Column(name: "Password")]
    public string Password { get; set; } = string.Empty;

    [Column(name: "RoleId")]
    public int RoleId { get; set; }
}
