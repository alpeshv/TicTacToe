using System;
using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class TicTacToeGame : ITicTacToeGame
    {
        private readonly IGameBoard _gameBoard;
        private readonly IDisplayHelper _displayHelper;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public Player PlayerO { get; private set; }
        public Player PlayerX { get; private set; }
        public Player CurrentPlayer { get; set; }
        public Player Winner { get; set; }

        public TicTacToeGame(IGameBoard gameBoard, IDisplayHelper displayHelper, IRandomNumberGenerator randomNumberGenerator)
        {
            _gameBoard = gameBoard;
            _displayHelper = displayHelper;
            _randomNumberGenerator = randomNumberGenerator;

            // Started with player factory but I think it was over engineered so replaced it with direct player creations
            PlayerO = new Player('O');
            PlayerX = new Player('X');
        }

        public void Reset()
        {
            _gameBoard.Reset();
            Winner = null;
        }

        public void DisplayGameBoard()
        {
            _displayHelper.Display(_gameBoard.ToString());
        }

        public void SetCurrentPlayer()
        {
            if (CurrentPlayer == null || CurrentPlayer.PlayCharacter == 'X')
                CurrentPlayer = PlayerO;
            else
                CurrentPlayer = PlayerX;
        }

        public void CurrentPlayerPlay()
        {
            while(! _gameBoard.Play(CurrentPlayer.PlayCharacter, _randomNumberGenerator.Generate(), _randomNumberGenerator.Generate()));
        }

        // Violating SRP by Checking for game over and setting up winner in the same method
        // but it makes sense to keep them together
        public bool IsGameOver()
        {
            bool hasAPlayerWon;
            var result = _gameBoard.IsGameOver(out hasAPlayerWon);

            if (hasAPlayerWon)
                Winner = CurrentPlayer;

            return result;
        }

        public void DisplayResult()
        {
            if (Winner == null)
                _displayHelper.Display("Game Over. No Winner.");
            else
                _displayHelper.Display(String.Format("Game Over. Winner is Player {0}.", Winner.PlayCharacter));
        }
    }
}
