namespace OrderManager;

internal class Order
{
    public required string Name
    {
        get; set => field = string.IsNullOrEmpty( value )
            ? throw new ArgumentException( "Name не может быть пустым" )
            : value;
    }

    public required int Count
    {
        get; set => field = value < 1
            ? throw new ArgumentException( "Количество должно быть положительным числом" )
            : value;
    }

    public required string UserName
    {
        get; set => field = string.IsNullOrEmpty( value )
            ? throw new ArgumentException( "UserName не может быть пустым" )
            : value;
    }

    public required string Address
    {
        get; set => field = string.IsNullOrEmpty( value )
            ? throw new ArgumentException( "Address не может быть пустым" )
            : value;
    }

    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public required DateTime DeliveryDate { get; set; }

    public enum OrderStatus
    {
        Completed,
        Confirmed,
        Draft
    }
}
