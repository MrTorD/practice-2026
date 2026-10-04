namespace Infrastructure.Services.Dto;

public record PropertyDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Country { get; init; }

    public required string City { get; init; }

    public required string Address { get; init; }

    public required decimal Latitude { get; init; }

    public required decimal Longitude { get; init; }
}
