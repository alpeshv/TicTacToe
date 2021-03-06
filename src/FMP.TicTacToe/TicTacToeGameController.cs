﻿using FMP.TicTacToe.Abstract;

namespace FMP.TicTacToe
{
    public class TicTacToeGameController
    {
        private readonly ITicTacToeGame _ticTacToeGame;
        private readonly IPauseHelper _pauseHelper;

        public TicTacToeGameController(ITicTacToeGame ticTacToeGame, IPauseHelper pauseHelper)
        {
            _ticTacToeGame = ticTacToeGame;
            _pauseHelper = pauseHelper;
        }

        public void StartGame()
        {
            _ticTacToeGame.Reset();
            _ticTacToeGame.DisplayGameBoard();

            do
            {
                _pauseHelper.PauseFor(1000);
                _ticTacToeGame.SetCurrentPlayer();
                
                _ticTacToeGame.CurrentPlayerPlay();
                _ticTacToeGame.DisplayGameBoard();
                
            } while (!_ticTacToeGame.IsGameOver());

            _ticTacToeGame.DisplayResult();
        }
    }
}
