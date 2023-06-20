using FleetGraphAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetGraphAPI.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Vehicle>()
            .HasIndex(a => a.VIN)
            .IsUnique();
    }

    public DbSet<Vehicle> Vehicles { get; set; } = default!;
    public DbSet<Trip> Trips { get; set; } = default!;
    public DbSet<Alert> Alerts { get; set; } = default!;
}