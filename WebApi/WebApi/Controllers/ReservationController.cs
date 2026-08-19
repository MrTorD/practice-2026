using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route( "reservations" )]
public class ReservationController( ReservationService service ) : ControllerBase
{
    [HttpGet( "{id::guid}" )]
    public IActionResult GetReservation( Guid id )
    {
        try
        {
            return Ok( service.GetReservation( id ) );
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpGet]
    public IActionResult GetFilteredReservations(
        [FromQuery] Guid? roomTypeId,
        [FromQuery] DateOnly? arrivalDate,
        [FromQuery] DateOnly? departureDate,
        [FromQuery] int? guestsCount )
    {
        return Ok( service.GetFilteredReservations( roomTypeId, arrivalDate, departureDate, guestsCount ) );
    }

    [HttpPost]
    public IActionResult CreateReservation( [FromBody] ReservationSaveDto dto )
    {
        try
        {
            service.CreateReservation( dto );

            return Created();
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
        catch ( InvalidReservationException ex )
        {
            return BadRequest( ex.Message );
        }
    }

    [HttpDelete]
    public IActionResult DeleteReservation( Guid id )
    {
        try
        {
            service.DeleteReservation( id );

            return Ok();
        }
        catch ( NotFoundException ex )
        {
            return NotFound( ex.Message );
        }
    }

    [HttpGet( "search" )]
    public IActionResult GetAvailableRoomTypes(
        [FromQuery] DateOnly arrivalDate,
        [FromQuery] DateOnly departureDate,
        [FromQuery] string country,
        [FromQuery] string city,
        [FromQuery] int guestCount )
    {
        return Ok(
            service.GetAvailableRoomTypes(
                arrivalDate,
                departureDate,
                country,
                city,
                guestCount )
            );
    }
}
