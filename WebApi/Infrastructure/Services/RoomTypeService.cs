using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services.Dto;

namespace Infrastructure.Services;

public class RoomTypeService( IRoomTypeRepository repository )
{
    public RoomTypeDto GetRoomType( Guid id )
    {
        var rt = repository.GetRoomType( id );

        return new RoomTypeDto
        {
            Id = rt.Id,
            PropertyId = rt.PropertyId,
            Name = rt.Name,
            DailyPrice = rt.DailyPrice,
            Currency = rt.Currency,
            MinPersonCount = rt.MinPersonCount,
            MaxPersonCount = rt.MaxPersonCount,
            Amenities = rt.Amenities,
            Services = rt.Services,
        };
    }

    public void CreateRoomType( RoomTypeSaveDto dto )
    {
        var roomType = new RoomType
        {
            Id = Guid.NewGuid(),
            PropertyId = dto.PropertyId,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            Currency = dto.Currency,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            Amenities = dto.Amenities,
            Services = dto.Services,
        };

        repository.CreateRoomType( roomType );
    }

    public void DeleteRoomType( Guid id )
    {
        repository.DeleteRoomType( id );
    }

    public void UpdateRoomType( Guid id, RoomTypeSaveDto dto )
    {
        var roomType = repository.GetRoomType( id );

        roomType.PropertyId = dto.PropertyId;
        roomType.Name = dto.Name;
        roomType.DailyPrice = dto.DailyPrice;
        roomType.Currency = dto.Currency;
        roomType.MinPersonCount = dto.MinPersonCount;
        roomType.MaxPersonCount = dto.MaxPersonCount;
        roomType.Amenities = dto.Amenities;
        roomType.Services = dto.Services;

        repository.UpdateRoomType( roomType );
    }
}
