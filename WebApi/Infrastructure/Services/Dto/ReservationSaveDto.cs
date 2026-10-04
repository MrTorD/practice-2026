using Domain.Entities;

namespace Infrastructure.Services.Dto;

public class ReservationSaveDto
{
    public required Guid RoomTypeId { get; init; }

    public required DateOnly ArrivalDate { get; init; }

    public required DateOnly DepartureDate { get; init; }

    public required TimeOnly ArrivalTime { get; init; }

    public required TimeOnly DepartureTime { get; init; }

    public required string GuestName { get; init; }

    public required int GuestsCount { get; init; }

    public required string GuestPhoneNumber { get; init; }

    public Currency Currency { get; init; }
}
