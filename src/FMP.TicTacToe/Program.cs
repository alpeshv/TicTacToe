using System;

namespace FMP.TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var ticTacToeGameController = new TicTacToeGameController(new TicTacToeGame(new GameBoard(), new DisplayHelper(), new RandomNumberGenerator()), new PauseHelper());

            while (ContinuePlaying())
            {
                ticTacToeGameController.StartGame();   
            }
        }

        private static bool ContinuePlaying()
        {
            Console.Write("Press S to start game. Press any other key to exit:");
            var keyChar = Console.ReadKey().KeyChar;
            return keyChar == 'S' || keyChar == 's';
        }
    }
}
