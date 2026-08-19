using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    Property GetProperty( Guid id );

    IReadOnlyList<Property> GetProperties();

    void CreateProperty( Property property );

    void UpdateProperty( Property property );

    void DeleteProperty( Guid id );
}
