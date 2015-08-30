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
        public char[,] GameBoardValues { get; private set; }

        public GameBoard(char[,] initialValues)
        {
            GameBoardValues = initialValues;
        }

        public override string ToString()
        {
            return string.Format(DisplayFormat,
                GameBoardValues[0, 0],
                GameBoardValues[0, 1],
                GameBoardValues[0, 2],
                GameBoardValues[1, 0],
                GameBoardValues[1, 1],
                GameBoardValues[1, 2],
                GameBoardValues[2, 0],
                GameBoardValues[2, 1],
                GameBoardValues[2, 2]);
        }

        public void Play(Player player, Tuple<int, int> playLocation)
        {
            if (GameBoardValues[playLocation.Item1 - 1, playLocation.Item2 - 1] == ' ')
                GameBoardValues[playLocation.Item1 - 1, playLocation.Item2 - 1] = player.PlayCharacter;
        }

        public void Reset()
        {
            GameBoardValues = new[,]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            };
        }
    }
}
