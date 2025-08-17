using System.ComponentModel.DataAnnotations.Schema;

namespace AirAlmatyFlights.Domain.Entities;

[Table(name: "User", Schema = "public")]
public class User : BaseEntity
{
    [Column(name: "username")]
    public string UserName { get; set; } = string.Empty;

    [Column(name: "password")]
    public string Password { get; set; } = string.Empty;

    [Column(name: "roleid")]
    public int RoleId { get; set; }
}
