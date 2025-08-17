using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirAlmatyFlights.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [Column(name:"Id")]
    public int Id { get; set; }
}
