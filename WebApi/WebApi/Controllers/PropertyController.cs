using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "properties" )]
public class PropertyController( PropertyService service ) : ControllerBase
{
    [HttpGet( "{id::guid}" )]
    public IActionResult GetProperty( Guid id )
    {
        try
        {
            return Ok( service.GetProperty( id ) );
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpGet]
    public IActionResult GetProperties()
    {
        return Ok( service.GetProperties() );
    }

    [HttpGet( "{propertyId::guid}/roomtypes" )]
    public IActionResult GetPropertyRoomTypes( [FromRoute] Guid propertyId )
    {
        try
        {
            return Ok( service.GetPropertyRoomTypes( propertyId ) );
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpPost]
    public IActionResult CreateProperty( [FromBody] PropertySaveDto dto )
    {
        service.CreateProperty( dto );

        return Created();
    }

    [HttpPut( "{id::guid}" )]
    public IActionResult UpdateProperty( [FromRoute] Guid id, [FromBody] PropertySaveDto dto )
    {
        try
        {
            service.UpdateProperty( id, dto );

            return Ok();
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpDelete( "{id::guid}" )]
    public IActionResult DeleteProperty( [FromRoute] Guid id )
    {
        try
        {
            service.DeleteProperty( id );

            return Ok();
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }
}
