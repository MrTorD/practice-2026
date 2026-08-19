using Domain.Entities;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext( DbContextOptions options )
        : base( options )
    {
    }

    public DbSet<Property> Properties => Set<Property>();

    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        base.OnConfiguring( optionsBuilder );

        optionsBuilder.UseSqlServer( "Server=localhost\\SQLEXPRESS;Database=hotels;Trusted_Connection=True;TrustServerCertificate=True;" );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
    }
}
