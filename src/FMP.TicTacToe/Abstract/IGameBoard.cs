using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMP.TicTacToe.Abstract
{
    public interface IGameBoard
    {
        void Reset();
        bool Play(Player player, int x, int y);
        bool IsGameOver(out bool hasAPlayerWon);
        string ToString();
    }
}
