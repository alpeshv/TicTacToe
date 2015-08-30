using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMP.TicTacToe.Abstract
{
    public interface ITicTacToeGame
    {
        void Reset();
        void DisplayGameBoard();
        void SetCurrentPlayer();
        bool CurrentPlayerPlay();
        bool IsGameOver();
        void DisplayResult();
    }
}
