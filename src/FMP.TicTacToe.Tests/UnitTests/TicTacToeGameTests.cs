using System;
using FMP.TicTacToe.Abstract;
using Moq;
using NUnit.Framework;

namespace FMP.TicTacToe.Tests.UnitTests
{
    [TestFixture]
    public class TicTacToeGameTests
    {
        private Mock<IGameBoard> _gameBoardMock;
        private Mock<IDisplayHelper> _displayHelperMock;
        private Mock<IRandomNumberGenerator> _randomNumberGenerator;
        private TicTacToeGame _game;

        [SetUp]
        public void SetUp()
        {
            _gameBoardMock = new Mock<IGameBoard>();
            _displayHelperMock = new Mock<IDisplayHelper>();
            _randomNumberGenerator = new Mock<IRandomNumberGenerator>();

            _game = new TicTacToeGame(_gameBoardMock.Object, _displayHelperMock.Object, _randomNumberGenerator.Object);
        }

        [Test]
        public void Reset_ResetsGameBoard()
        {
            _game.Reset();

            _gameBoardMock.Verify(m => m.Reset(), Times.Once);
        }

        [Test]
        public void Reset_ResetsWinner()
        {
            _game.Winner = new Player('X');

            _game.Reset();

            Assert.That(_game.Winner, Is.Null);
        }

        [Test]
        public void DisplayGameBoard_PassesFormattedGameBoardStringToDisplayHelper()
        {
            var randomString = DateTime.Now.ToString();
            _gameBoardMock.Setup(m => m.ToString()).Returns(randomString);
            _game.DisplayGameBoard();

            _displayHelperMock.Verify(m => m.Display(randomString), Times.Once);
        }

        [Test]
        public void CreatingNewGame_GetsPlayerOFromPlayerFactory()
        {
            Assert.That(_game.PlayerO, Is.Not.Null);
            Assert.That(_game.PlayerO.PlayCharacter, Is.EqualTo('O'));
        }

        [Test]
        public void CreatingNewGame_GetsPlayerXFromPlayerFactory()
        {
            Assert.That(_game.PlayerX, Is.Not.Null);
            Assert.That(_game.PlayerX.PlayCharacter, Is.EqualTo('X'));
        }

        [Test]
        public void SetCurrentPlayer_WhenCurrentPlayerIsNotSet_SetsPlayerOAsCurrentUser()
        {
            _game.SetCurrentPlayer();

            Assert.That(_game.CurrentPlayer, Is.Not.Null);
            Assert.That(_game.CurrentPlayer.PlayCharacter, Is.EqualTo('O'));
        }

        [Test]
        public void SetCurrentPlayer_WhenCurrentPlayerIsPlayerO_SetsPlayerXAsCurrentUser()
        {
            _game.CurrentPlayer = new Player('O');

            _game.SetCurrentPlayer();

            Assert.That(_game.CurrentPlayer, Is.Not.Null);
            Assert.That(_game.CurrentPlayer.PlayCharacter, Is.EqualTo('X'));
        }

        [Test]
        public void SetCurrentPlayer_WhenCurrentPlayerIsPlayerX_SetsPlayerOAsCurrentUser()
        {
            _game.CurrentPlayer = new Player('X');

            _game.SetCurrentPlayer();

            Assert.That(_game.CurrentPlayer, Is.Not.Null);
            Assert.That(_game.CurrentPlayer.PlayCharacter, Is.EqualTo('O'));
        }

        [TestCase(1, 3)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void CurrentPlayerPlay_MakesARandomMoveByCurrentPlayer(int x, int y)
        {
            _randomNumberGenerator.SetupSequence(m => m.Generate())
                .Returns(x)
                .Returns(y);

            _gameBoardMock.Setup(m => m.Play(It.IsAny<char>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            
            _game.CurrentPlayerPlay();

            _gameBoardMock.Verify(m => m.Play(_game.CurrentPlayer.PlayCharacter, x, y), Times.Once);
        }

        [Test]
        public void CurrentPlayerPlay_KeepsTryingUntilPlayerMakesASuccessfulMove()
        {
            _gameBoardMock.SetupSequence(m => m.Play(It.IsAny<char>(), It.IsAny<int>(), It.IsAny<int>()))
               .Returns(false)
               .Returns(false)
               .Returns(false)
               .Returns(false)
               .Returns(true);

            _game.CurrentPlayerPlay();

            _gameBoardMock.Verify(m => m.Play(_game.CurrentPlayer.PlayCharacter, It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(5));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsGameOver_ChecksForGameOverOnGameBoardAndReturnsResult(bool expectedResult)
        {
            bool hasAPlayerWon;
            _gameBoardMock.Setup(m => m.IsGameOver(out hasAPlayerWon)).Returns(expectedResult);
            var result = _game.IsGameOver();

            _gameBoardMock.Verify(m => m.IsGameOver(out hasAPlayerWon), Times.Once);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase('O')]
        [TestCase('X')]
        public void IsGameOver_WhenAPlayerHasWon_SetsCurrentPlayerAsWinner(char playerCharacter)
        {
            _game.CurrentPlayer = new Player(playerCharacter);
            var hasAPlayerWon = true;
            _gameBoardMock.Setup(m => m.IsGameOver(out hasAPlayerWon)).Returns(true);

            _game.IsGameOver();

            Assert.That(_game.Winner, Is.Not.Null);
            Assert.That(_game.Winner.PlayCharacter, Is.EqualTo(playerCharacter));
        }

        [Test]
        public void DisplayResult_WhenNoWinner_DisplaysNoWinnerMessage()
        {
            _game.Winner = null;

            _game.DisplayResult();

            _displayHelperMock.Verify(m => m.Display("Game Over. No Winner."), Times.Once);
        }

        [Test]
        public void DisplayResult_WhenWinnerIsPlayerO_DisplaysPlayerOAsWinner()
        {
            _game.Winner = new Player('O');

            _game.DisplayResult();

            _displayHelperMock.Verify(m => m.Display("Game Over. Winner is Player O."), Times.Once);
        }

        [Test]
        public void DisplayResult_WhenWinnerIsPlayerX_DisplaysPlayerXAsWinner()
        {
            _game.Winner = new Player('X');

            _game.DisplayResult();

            _displayHelperMock.Verify(m => m.Display("Game Over. Winner is Player X."), Times.Once);
        }
    }
}
