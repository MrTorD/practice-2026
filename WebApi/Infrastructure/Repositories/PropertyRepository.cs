using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PropertyRepository( AppDbContext dbContext ) : IPropertyRepository
{
    public Property GetProperty( Guid id )
    {
        return dbContext.Properties
            .AsNoTracking()
            .Where( x => x.Id == id )
            .Include( x => x.RoomTypes )
            .FirstOrDefault()
            ?? throw new NotFoundException( $"Средство размещения c ID: {id} не найдено" );
    }

    public IReadOnlyList<Property> GetProperties()
    {
        return dbContext.Properties
            .AsNoTracking()
            .Include( x => x.RoomTypes )
            .ToList();
    }

    public void CreateProperty( Property property )
    {
        dbContext.Properties.Add( property );
        dbContext.SaveChanges();
    }

    public void DeleteProperty( Guid id )
    {
        var property = GetProperty( id );

        dbContext.Properties.Remove( property );
        dbContext.SaveChanges();
    }

    public void UpdateProperty( Property property )
    {
        dbContext.Update( property );
        dbContext.SaveChanges();
    }
}
