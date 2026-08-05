namespace CasinoGame;

internal class Casino
{
    public enum GameResult
    {
        Win,
        Loss
    }

    public required int Balance
    {
        get => _balance;
        init => _balance = value >= 0
                ? value
                : throw new ArgumentOutOfRangeException( $"{nameof( Balance )}", "Баланс должен быть неотрицательным" );
    }

    public GameResult PlaceBet( int bet )
    {
        ValidateBet( bet );

        int diff = CalcBalanceDiff( bet );

        _balance += diff;

        return diff > 0
            ? GameResult.Win
            : GameResult.Loss;
    }

    private void ValidateBet( int bet )
    {
        if ( bet <= 0 )
        {
            throw new ArgumentOutOfRangeException( $"{nameof( bet )}", "Ставка должна быть положительным числом" );
        }

        if ( bet > _balance )
        {
            throw new ArgumentOutOfRangeException( $"{nameof( bet )}", "Ставка не может превышать значение баланса" );
        }
    }

    private int CalcBalanceDiff( int bet )
    {
        int randomNum = Random.Shared.Next( MinRandomValue, MaxRandomValue );

        bool isWon = randomNum >= MinWinValue;

        return isWon
            ? bet * ( 1 + ( Multiplicator * ( randomNum % (MinWinValue - 1) ) ) )
            : -bet;
    }

    private const int Multiplicator = 2;
    private const int MinRandomValue = 0;
    private const int MaxRandomValue = 21;
    private const int MinWinValue = 18;

    private int _balance;
}
