namespace Domain.Entities;

public class RoomType
{
    public required Guid Id { get; set; }

    public required Guid PropertyId { get; set; }

    public Property Property { get; set; } = null!;

    public required string Name { get; set; }

    public required decimal DailyPrice { get; set; }

    public required Currency Currency { get; set; }

    public required int MinPersonCount { get; set; }

    public required int MaxPersonCount { get; set; }

    public required List<Service> Services { get; set; }

    public required List<Amenity> Amenities { get; set; }

    public IReadOnlyList<Reservation> Reservations { get; set; } = null!;
}
