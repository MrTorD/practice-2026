using CasinoGame;

var startBalance = GameHandler.RequestIntWithRestriction( "Введите стартовый баланс", ( num ) => num < 0 );
var gameHandler = new GameHandler( startBalance );

gameHandler.PrintMenu();

while ( gameHandler.State == GameHandler.GameState.Running )
{
    string command = Console.ReadLine() ?? "";

    Console.Clear();

    gameHandler.PrintMenu();
    gameHandler.ProcessCommand( command );
}
