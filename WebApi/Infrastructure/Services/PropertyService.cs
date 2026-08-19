using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services.Dto;

namespace Infrastructure.Services;

public class PropertyService( IPropertyRepository repository )
{
    public PropertyDto GetProperty( Guid id )
    {
        var p = repository.GetProperty( id );

        return new PropertyDto
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            City = p.City,
            Country = p.Country,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
        };
    }

    public IReadOnlyList<PropertyDto> GetProperties()
    {
        var properties = repository.GetProperties();

        return properties.Select( p => new PropertyDto
        {
            Id = p.Id,
            Name = p.Name,
            Address = p.Address,
            City = p.City,
            Country = p.Country,
            Latitude = p.Latitude,
            Longitude = p.Longitude,
        } )
        .ToList();
    }

    public IReadOnlyList<RoomTypeDto> GetPropertyRoomTypes( Guid propertyId )
    {
        var property = repository.GetProperty( propertyId );

        return property.RoomTypes.Select( s => new RoomTypeDto
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

    public void CreateProperty( PropertySaveDto dto )
    {
        var property = new Property
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            City = dto.City,
            Country = dto.Country,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
        };

        repository.CreateProperty( property );
    }

    public void UpdateProperty( Guid id, PropertySaveDto dto )
    {
        var property = repository.GetProperty( id );

        property.Name = dto.Name;
        property.Address = dto.Address;
        property.City = dto.City;
        property.Country = dto.Country;
        property.Latitude = dto.Latitude;
        property.Longitude = dto.Longitude;

        repository.UpdateProperty( property );
    }

    public void DeleteProperty( Guid id )
    {
        repository.DeleteProperty( id );
    }
}
