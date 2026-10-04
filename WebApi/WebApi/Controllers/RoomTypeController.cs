using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "roomtypes" )]
public class RoomTypeController( RoomTypeService service ) : Controller
{
    [HttpGet( "{id::guid}" )]
    public IActionResult GetRoomType( [FromRoute] Guid id )
    {
        try
        {
            return Ok( service.GetRoomType( id ) );
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpPost]
    public IActionResult CreateRoomType( [FromBody] RoomTypeSaveDto dto )
    {
        service.CreateRoomType( dto );

        return Created();
    }

    [HttpPut( "{id::guid}" )]
    public IActionResult UpdateRoomType( [FromRoute] Guid id, [FromBody] RoomTypeSaveDto dto )
    {
        try
        {
            service.UpdateRoomType( id, dto );

            return Ok();
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpDelete( "{id::guid}" )]
    public IActionResult DeleteRoomType( [FromRoute] Guid id )
    {
        try
        {
            service.DeleteRoomType( id );

            return Ok();
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }
}
