using AirAlmatyFlights.Application.Interfaces.Persistence;
using AirAlmatyFlights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirAlmatyFlights.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Flight> Flights { get; set; } = null!;

}
