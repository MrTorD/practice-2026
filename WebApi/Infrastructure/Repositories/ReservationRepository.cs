using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationRepository( AppDbContext dbContext ) : IReservationRepository
{
    public Reservation GetReservation( Guid id )
    {
        return dbContext.Reservations
            .AsNoTracking()
            .Where( x => x.Id == id )
            .FirstOrDefault()
            ?? throw new NotFoundException( $"Бронирование с ID: {id} не найдено" );
    }

    public IReadOnlyList<Reservation> GetFilteredReservations( Guid? roomTypeId, DateOnly? arrivalDate,
        DateOnly? departureDate, int? guestsCount )
    {
        return dbContext.Reservations
            .AsNoTracking()
            .Where( r =>
                roomTypeId != null && r.RoomTypeId == roomTypeId ||
                arrivalDate != null && r.ArrivalDate == arrivalDate ||
                departureDate != null && r.DepartureDate == departureDate ||
                guestsCount != null && r.GuestsCount == guestsCount )
            .ToList();
    }

    public RoomType GetRoomType( Guid id )
    {
        return dbContext.RoomTypes
            .AsNoTracking()
            .Where( x => x.Id == id )
            .Include( x => x.Reservations )
            .FirstOrDefault()
            ?? throw new NotFoundException( $"Категория номера c ID: {id} не найдена" );
    }

    public IReadOnlyList<RoomType> GetAvailableRoomTypes( DateOnly arrivalDate, DateOnly departureDate,
        string country, string city, int guestsNumber )
    {
        return dbContext.RoomTypes
            .AsNoTracking()
            .Where( r =>
                r.Reservations.All( r =>
                    r.ArrivalDate > departureDate ||
                    r.DepartureDate < arrivalDate ) &&
                r.Property.Country == country &&
                r.Property.City == city &&
                r.MinPersonCount <= guestsNumber &&
                r.MaxPersonCount >= guestsNumber
            )
            .ToList();
    }

    public void CreateReservation( Reservation reservation )
    {
        dbContext.Reservations.Add( reservation );
        dbContext.SaveChanges();
    }

    public void UpdateReservation( Reservation reservation )
    {
        dbContext.Update( reservation );
        dbContext.SaveChanges();
    }

    public void DeleteReservation( Guid id )
    {
        var reservation = GetReservation( id );

        dbContext.Remove( reservation );
        dbContext.SaveChanges();
    }
}
