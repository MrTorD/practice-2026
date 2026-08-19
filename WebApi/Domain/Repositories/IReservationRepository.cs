using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Reservation GetReservation( Guid id );

    IReadOnlyList<Reservation> GetFilteredReservations( Guid? roomTypeId, DateOnly? arrivalDate,
        DateOnly? departureDate, int? guestsCount );

    public IReadOnlyList<RoomType> GetAvailableRoomTypes( DateOnly arrivalDate, DateOnly departureDate,
        string country, string city, int guestsNumber );

    RoomType GetRoomType( Guid id );

    void CreateReservation( Reservation reservation );

    void UpdateReservation( Reservation reservation );

    void DeleteReservation( Guid id );
}
