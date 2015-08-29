using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMP.TicTacToe
{
    public class Player
    {
        public char PlayCharacter { get; private set; }
        
        public Player(char c)
        {
            PlayCharacter = c;
        }
    }
}
