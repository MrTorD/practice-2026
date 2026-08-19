using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.ArrivalDate )
            .IsRequired();

        builder.Property( r => r.DepartureDate )
            .IsRequired();

        builder.Property( r => r.ArrivalTime )
            .IsRequired();

        builder.Property( r => r.DepartureTime )
            .IsRequired();

        builder.Property( r => r.GuestName )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( r => r.GuestPhoneNumber )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( r => r.Currency )
            .IsRequired();

        builder.Ignore( r => r.Total );

        builder.HasOne( r => r.RoomType )
        .WithMany( r => r.Reservations )
        .HasForeignKey( r => r.RoomTypeId )
        .HasPrincipalKey( r => r.Id );
    }
}
