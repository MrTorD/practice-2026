using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Infrastructure.Services.Dto;

namespace Infrastructure.Services;

public class ReservationService( IReservationRepository repository )
{
    public ReservationDto GetReservation( Guid id )
    {
        var reservation = repository.GetReservation( id );

        return new ReservationDto
        {
            Id = reservation.Id,
            RoomTypeId = reservation.RoomTypeId,
            ArrivalDate = reservation.ArrivalDate,
            DepartureDate = reservation.DepartureDate,
            ArrivalTime = reservation.ArrivalTime,
            DepartureTime = reservation.ArrivalTime,
            GuestName = reservation.GuestName,
            GuestPhoneNumber = reservation.GuestPhoneNumber,
            Currency = reservation.Currency,
        };
    }

    public IReadOnlyList<ReservationDto> GetFilteredReservations( Guid? roomTypeId, DateOnly? arrivalDate,
        DateOnly? departureDate, int? guestsCount )
    {
        var reservations = repository.GetFilteredReservations( roomTypeId, arrivalDate, departureDate, guestsCount );

        return reservations.Select( s => new ReservationDto
        {
            Id = s.Id,
            RoomTypeId = s.RoomTypeId,
            ArrivalDate = s.ArrivalDate,
            DepartureDate = s.DepartureDate,
            ArrivalTime = s.ArrivalTime,
            DepartureTime = s.ArrivalTime,
            GuestName = s.GuestName,
            GuestPhoneNumber = s.GuestPhoneNumber,
            Currency = s.Currency,
        } )
        .ToList();
    }

    public IReadOnlyList<RoomTypeDto> GetAvailableRoomTypes( DateOnly arrivalDate, DateOnly departureDate,
        string country, string city, int guestsNumber )
    {
        var roomTypes = repository.GetAvailableRoomTypes( arrivalDate, departureDate, country, city, guestsNumber );

        return roomTypes.Select( s => new RoomTypeDto
        {
            Id = s.Id,
            PropertyId = s.PropertyId,
            Name = s.Name,
            DailyPrice = s.DailyPrice,
            Currency = s.Currency,
            MinPersonCount = s.MinPersonCount,
            MaxPersonCount = s.MaxPersonCount,
            Amenities = s.Amenities,
            Services = s.Services,

        } )
        .ToList();
    }

    public void CreateReservation( ReservationSaveDto dto )
    {

        ValidateReservationParams( dto.RoomTypeId, dto.ArrivalDate, dto.DepartureDate, dto.GuestsCount );

        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            RoomTypeId = dto.RoomTypeId,
            ArrivalDate = dto.ArrivalDate,
            DepartureDate = dto.DepartureDate,
            ArrivalTime = dto.ArrivalTime,
            DepartureTime = dto.ArrivalTime,
            GuestName = dto.GuestName,
            GuestsCount = dto.GuestsCount,
            GuestPhoneNumber = dto.GuestPhoneNumber,
            Currency = dto.Currency,
        };

        repository.CreateReservation( reservation );
    }

    public void UpdateReservation( Guid id, ReservationSaveDto dto )
    {
        ValidateReservationParams( dto.RoomTypeId, dto.ArrivalDate, dto.DepartureDate, dto.GuestsCount );

        var reservation = repository.GetReservation( id );

        reservation.RoomTypeId = dto.RoomTypeId;
        reservation.ArrivalDate = dto.ArrivalDate;
        reservation.DepartureDate = dto.DepartureDate;
        reservation.ArrivalTime = dto.ArrivalTime;
        reservation.DepartureTime = dto.ArrivalTime;
        reservation.GuestName = dto.GuestName;
        reservation.GuestsCount = dto.GuestsCount;
        reservation.GuestPhoneNumber = dto.GuestPhoneNumber;
        reservation.Currency = dto.Currency;

        repository.UpdateReservation( reservation );
    }

    public void DeleteReservation( Guid id )
    {
        repository.DeleteReservation( id );
    }
    
    private void ValidateReservationParams( Guid roomTypeId, DateOnly arrivalDate, 
        DateOnly departureDate, int guestsCount )
    {
        var roomType = repository.GetRoomType( roomTypeId );

        if ( roomType.MinPersonCount > guestsCount || roomType.MaxPersonCount < guestsCount )
        {
            throw new InvalidReservationException( "Количество гостей не подходит под параметры номера" );
        }

        if ( roomType.Reservations.Any( r =>
            r.ArrivalDate <= departureDate &&
            r.DepartureDate >= arrivalDate ) )
        {
            throw new InvalidReservationException( "Номер уже зарезервирован на выбранную дату" );
        }
    }
}
