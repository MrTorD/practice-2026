using Domain.Entities;

namespace Infrastructure.Services.Dto;

public class RoomTypeDto
{
    public required Guid Id { get; init; }

    public required Guid PropertyId { get; init; }

    public required string Name { get; init; }

    public required decimal DailyPrice { get; init; }

    public required Currency Currency { get; init; }

    public required int MinPersonCount { get; init; }

    public required int MaxPersonCount { get; init; }

    public required List<Service> Services { get; init; }

    public required List<Amenity> Amenities { get; init; }
}