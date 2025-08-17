using AirAlmatyFlights.Application.Interfaces.Persistence;
using AirAlmatyFlights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirAlmatyFlights.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Flight> Flights { get; set; }

}
