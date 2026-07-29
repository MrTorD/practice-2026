using OrderManager;

HashSet<string> CONFIRMATION_RESPONSES = [ "yes", "y" ];
const int DELIVERY_DURATION_IN_DAYS = 3;

var order = RequestOrder( "Оформление заказа" );
bool isConfirmed = RequestConfirmation( order );

if ( isConfirmed )
{
    order.Status = Order.OrderStatus.Confirmed;
    Console.WriteLine( $"Благодарим за заказ {order.Name}. Ожидайте достаку к {order.DeliveryDate:dd.MM.yyyy}" );
}

Order RequestOrder( string requestMessage )
{
    Console.WriteLine( requestMessage );

    var name = RequestStringWithRestriction( "Введите название товара", s => string.IsNullOrWhiteSpace( s ) );
    var count = RequestIntWithRestriction( "Введите количество товара", num => num <= 0 );
    var userName = RequestStringWithRestriction( "Введите имя пользователя", s => string.IsNullOrWhiteSpace( s ) );
    var address = RequestStringWithRestriction( "Введите адрес", s => string.IsNullOrWhiteSpace( s ) );

    return new Order
    {
        Name = name,
        Count = count,
        UserName = userName,
        Address = address,
        DeliveryDate = DateTime.Now.AddDays( DELIVERY_DURATION_IN_DAYS )
    };
}

bool RequestConfirmation( Order order )
{
    Console.WriteLine( $"Подтвердите заказ {order.Name}, количеством {order.Count} на адрес {order.Address}" );
    var answer = Console.ReadLine() ?? "";

    return CONFIRMATION_RESPONSES.Contains( answer.ToLower() );
}

string RequestStringWithRestriction( string requestMessage, Func<string, bool> restriction )
{
    Console.WriteLine( requestMessage );

    var input = Console.ReadLine() ?? "";
    while ( restriction( input ) )
    {
        Console.WriteLine( "Ошибка, введите корректное значение" );

        input = Console.ReadLine() ?? "";
    }

    return input;
}

int RequestIntWithRestriction( string requestMessage, Func<int, bool> restriction )
{
    Console.WriteLine( requestMessage );

    var input = Console.ReadLine() ?? "";
    int value;

    while ( !int.TryParse( input, out value ) || restriction( value ) )
    {
        Console.WriteLine( $"Ошибка, введите корректное число" );

        input = Console.ReadLine() ?? "";
    }

    return value;
}