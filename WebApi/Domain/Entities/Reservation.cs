using Infrastructure.Exceptions;

namespace Domain.Entities;

public class Reservation
{
    public required Guid Id { get; set; }

    public required Guid RoomTypeId { get; set; }

    public RoomType RoomType { get; set; } = null!;

    public required DateOnly ArrivalDate
    {
        get;
        set => field = value >= DepartureDate
            ? throw new InvalidReservationException( "Дата заезда не может быть позже или равна дате выезда" )
            : value;
    } = DateOnly.MinValue;

    public required DateOnly DepartureDate
    {
        get;
        set => field = value <= ArrivalDate
            ? throw new InvalidReservationException( "Дата выезда не может быть раньше или равна дате заезда" )
            : value;
    } = DateOnly.MaxValue;

    public required TimeOnly ArrivalTime { get; set; }

    public required TimeOnly DepartureTime { get; set; }

    public required string GuestName { get; set; }

    public required int GuestsCount { get; set; }

    public required string GuestPhoneNumber { get; set; }

    public decimal Total => RoomType.DailyPrice * ( DepartureDate.DayNumber - ArrivalDate.DayNumber );

    public Currency Currency { get; set; }
}
