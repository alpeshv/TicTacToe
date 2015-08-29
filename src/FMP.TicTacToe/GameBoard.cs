using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMP.TicTacToe
{
    public class GameBoard
    {
        // Display format could be injected by game controller but I think its better to leave it on GameBoard itself
        public const string DisplayFormat = "{0}|{1}|{2}\n_ _ _\n{3}|{4}|{5}\n_ _ _\n{6}|{7}|{8}";
        private char[,] _gameBoardValues;

        public GameBoard(char[,] initialValues)
        {
            _gameBoardValues = initialValues;
        }

        public override string ToString()
        {
            return string.Format(DisplayFormat,
                _gameBoardValues[0, 0],
                _gameBoardValues[0, 1],
                _gameBoardValues[0, 2],
                _gameBoardValues[1, 0],
                _gameBoardValues[1, 1],
                _gameBoardValues[1, 2],
                _gameBoardValues[2, 0],
                _gameBoardValues[2, 1],
                _gameBoardValues[2, 2]);
        }

        public void Play(Player player, Tuple<int, int> playLocation)
        {
            if (_gameBoardValues[playLocation.Item1 - 1, playLocation.Item2 - 1] == ' ')
                _gameBoardValues[playLocation.Item1 - 1, playLocation.Item2 - 1] = player.PlayCharacter;
        }

        public void Reset()
        {
            _gameBoardValues = new[,]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            };
        }
    }
}
