namespace CasinoGame;

internal class GameHandler( int startBalance )
{
    public enum GameState
    {
        Running,
        Ended
    }

    public GameState State { get; set; } = GameState.Running;

    public void ProcessCommand( string command )
    {
        switch ( command )
        {
            case "1":
                Console.WriteLine( $"Ваш баланс: {_casino.Balance}" );
                break;
            case "2":
                int bet = RequestIntWithRestriction( "Введите ставку", ( num ) => !IsValidBet( num ) );
                var result = _casino.PlaceBet( bet );
                PrintResultMessage( result );
                break;
            case "3":
                State = GameState.Ended;
                break;
            default:
                Console.WriteLine( "Введена некорректная команда. Повторите ввод" );
                break;
        }
    }

    public static int RequestIntWithRestriction( string requestMessage, Func<int, bool> restriction )
    {
        Console.WriteLine( requestMessage );

        var input = Console.ReadLine() ?? "";

        int value;

        while ( !int.TryParse( input, out value ) || restriction.Invoke( value ) )
        {
            Console.WriteLine( "Некорректное значение. Повторите ввод" );

            input = Console.ReadLine() ?? "";
        }

        return value;
    }

    public void PrintMenu()
    {
        Console.WriteLine( "Казино. Выбирите опцию" );
        Console.WriteLine( "1. Посмотреть баланс" );
        Console.WriteLine( "2. Сделать ставку" );
        Console.WriteLine( "3. Выйти" );
    }

    private void PrintResultMessage( Casino.GameResult result )
    {
        var msg = result == Casino.GameResult.Win
            ? "Победа. "
            : "Поражение. ";

        Console.WriteLine( msg + $"Ваш новый баланс: {_casino.Balance}" );
    }

    private bool IsValidBet( int bet )
    {
        return bet > 0 && bet <= _casino.Balance;
    }

    private readonly Casino _casino = new() { Balance = startBalance };
}
