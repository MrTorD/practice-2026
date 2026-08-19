namespace Infrastructure.Exceptions;

public class InvalidReservationException : Exception
{
    public InvalidReservationException() { }

    public InvalidReservationException( string message ) : base( message ) { }
}
