using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    RoomType GetRoomType( Guid id );

    void CreateRoomType( RoomType roomType );

    void UpdateRoomType( RoomType roomType );

    void DeleteRoomType( Guid id );
}
