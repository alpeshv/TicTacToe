using System.Threading;
using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class PauseHelper: IPauseHelper
    {
        public void PauseFor(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
    }
}
