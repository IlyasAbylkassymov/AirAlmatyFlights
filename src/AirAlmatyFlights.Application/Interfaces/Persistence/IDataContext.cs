using AirAlmatyFlights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirAlmatyFlights.Application.Interfaces.Persistence;

public interface IDataContext
{
    DbSet<Flight> Flights { get; set; }

    DbSet<User> Users { get; set; }

    DbSet<Role> Roles { get; set; }
}
