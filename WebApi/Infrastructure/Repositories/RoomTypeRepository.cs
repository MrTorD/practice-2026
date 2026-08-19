using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoomTypeRepository( AppDbContext dbContext ) : IRoomTypeRepository
{
    public RoomType GetRoomType( Guid id )
    {
        return dbContext.RoomTypes
            .AsNoTracking()
            .Where( r => r.Id == id )
            .FirstOrDefault()
            ?? throw new NotFoundException( "Категория номера не найдена" );
    }

    public void CreateRoomType( RoomType roomType )
    {
        dbContext.RoomTypes.Add( roomType );
        dbContext.SaveChanges();
    }

    public void UpdateRoomType( RoomType roomType )
    {
        dbContext.RoomTypes.Update( roomType );
        dbContext.SaveChanges();
    }

    public void DeleteRoomType( Guid id )
    {
        var roomType = GetRoomType( id );

        dbContext.RoomTypes.Remove( roomType );
        dbContext.SaveChanges();
    }
}
