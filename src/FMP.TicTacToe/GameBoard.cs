using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class GameBoard : IGameBoard
    {
        // Display format could be injected by game controller but I think its better to leave it on GameBoard itself
        public const string DisplayFormat = "\n{0}|{1}|{2}\n_ _ _\n{3}|{4}|{5}\n_ _ _\n{6}|{7}|{8}";
        public char[,] GameBoardValues { get; private set; }

        public GameBoard()
        {
            GameBoardValues = new[,]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            }; ;
        }

        // This constructor is only used to prepare game board for testing other functionality.
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

        public bool Play(Player player, int x, int y)
        {
            if (GameBoardValues[x - 1, y - 1] == ' ')
            {
                GameBoardValues[x - 1, y - 1] = player.PlayCharacter;
                return true;
            }

            return false;
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

        public bool IsGameOver(out bool hasAPlayerWon)
        {
            return (hasAPlayerWon = HasAPlayerWon()) || GameBoardIsFull();
        }

        private bool GameBoardIsFull()
        {
            // Can also use move counter
            for (var r = 0; r < 3; r++)
            {
                for (var c = 0; c < 3; c++)
                {
                    if (GameBoardValues[r, c] == ' ')
                        return false;
                }
            }

            return true;
        }

        private bool HasAPlayerWon()
        {
            return AreAllCharactersInAnyRawSame()
                   || AreAllCharactersInAnyColumnSame()
                   || AreAllCharactersInAnyDiagonalSame();
        }

        private bool AreAllCharactersInAnyRawSame()
        {
            return AreAllCharactersInARawSame(0) || AreAllCharactersInARawSame(1) || AreAllCharactersInARawSame(2);
        }

        private bool AreAllCharactersInARawSame(int raw)
        {
            return (GameBoardValues[raw, 0] != ' ' && GameBoardValues[raw, 0] == GameBoardValues[raw, 1] && GameBoardValues[raw, 1] == GameBoardValues[raw, 2]);
        }

        private bool AreAllCharactersInAnyColumnSame()
        {
            return AreAllCharactersInAColumnSame(0) || AreAllCharactersInAColumnSame(1) || AreAllCharactersInAColumnSame(2);
        }

        private bool AreAllCharactersInAColumnSame(int column)
        {
            return (GameBoardValues[0, column] != ' ' && GameBoardValues[0, column] == GameBoardValues[1, column] && GameBoardValues[1, column] == GameBoardValues[2, column]);
        }

        private bool AreAllCharactersInAnyDiagonalSame()
        {
            return (GameBoardValues[0, 0] != ' ' && GameBoardValues[0, 0] == GameBoardValues[1, 1] && GameBoardValues[1, 1] == GameBoardValues[2, 2])
                 || (GameBoardValues[2, 0] != ' ' && GameBoardValues[2, 0] == GameBoardValues[1, 1] && GameBoardValues[1, 1] == GameBoardValues[0, 2]);
        }
    }
}
