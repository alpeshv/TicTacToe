using System;

namespace FMP.TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var ticTacToeGameController = new TicTacToeGameController(new TicTacToeGame(new GameBoard(), new ConsoleDisplayHelper(), new TicTacToeRandomNumberGenerator()), new PauseHelper());

                while (ContinuePlaying())
                {
                    ticTacToeGameController.StartGame();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                Console.Write("Press any key to exit...");
                Console.ReadLine();
            }
        }

        private static bool ContinuePlaying()
        {
            Console.Write("Press S to start the game. Press any other key to exit:");
            var keyChar = Console.ReadKey().KeyChar;
            return keyChar == 'S' || keyChar == 's';
        }
    }
}
