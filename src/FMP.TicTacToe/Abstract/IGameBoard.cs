namespace FMP.TicTacToe.Abstract
{
    public interface IGameBoard
    {
        void Reset();
        bool Play(char ch, int x, int y);
        bool IsGameOver(out bool hasAPlayerWon);
        string ToString();
    }
}
