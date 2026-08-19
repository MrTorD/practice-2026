using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.Name )
            .HasMaxLength( 50 )
            .IsRequired();

        builder.Property( r => r.DailyPrice )
            .IsRequired();

        builder.Property( r => r.Currency )
            .IsRequired();

        builder.Property( r => r.MinPersonCount )
            .IsRequired();

        builder.Property( r => r.MaxPersonCount )
            .IsRequired();

        builder.Property( r => r.Services )
            .IsRequired();

        builder.Property( r => r.Amenities )
            .IsRequired();

        builder.HasOne( r => r.Property )
            .WithMany( p => p.RoomTypes )
            .HasForeignKey( r => r.PropertyId )
            .HasPrincipalKey( r => r.Id );

        builder.HasMany( r => r.Reservations )
            .WithOne( r => r.RoomType )
            .HasForeignKey( r => r.RoomTypeId )
            .HasPrincipalKey( r => r.Id );
    }
}
