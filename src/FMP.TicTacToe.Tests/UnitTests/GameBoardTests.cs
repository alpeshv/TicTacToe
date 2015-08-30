using System;
using NUnit.Framework;

namespace FMP.TicTacToe.Tests.UnitTests
{
    [TestFixture]
    class GameBoardTests
    {
        private const string DisplayFormat = "{0}|{1}|{2}\n_ _ _\n{3}|{4}|{5}\n_ _ _\n{6}|{7}|{8}";
        private char[,] _boardArray;
        private readonly Player _playerO = new Player('O');
        private readonly Player _playerX = new Player('X');

        [SetUp]
        public void SetUp()
        {
            _boardArray = new[,]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' },
                { ' ', ' ', ' ' }
            };
        }

        [Test]
        public void GameBoardDisplayFormatShouldBePreSet()
        {
            Assert.That(GameBoard.DisplayFormat, Is.EqualTo(DisplayFormat));
        }

        [Test]
        public void CreatingNewGameBoard_WithEmptyValues_ShouldInitializeGameBoardWithEmptyValues()
        {
            var gameBoard = new GameBoard(_boardArray);
            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void CreatingNewGameBoard_WithSomeNonEmptyValues_ShouldInitializeGameBoardWithNonEmptyValuesAtGivenLocations()
        {
            _boardArray[0, 0] = 'X';

            var gameBoard = new GameBoard(_boardArray);

            AssertGameBoardStatus(gameBoard, 'X', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Play_ByPlayerOFor22When22IsEmpty_ShouldPlaceSymbolOOnGameBoadrdAt22()
        {
            var gameBoard = new GameBoard(_boardArray);
            var playLocation = new Tuple<int, int>(2, 2);

            gameBoard.Play(_playerO, playLocation);

            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', 'O', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Play_ByPlayerXFor13When13IsEmpty_ShouldPlaceSymbolXOnGameBoadrdAt13()
        {
            var gameBoard = new GameBoard(_boardArray);
            var playLocation = new Tuple<int, int>(1, 3);

            gameBoard.Play(_playerX, playLocation);

            AssertGameBoardStatus(gameBoard, ' ', ' ', 'X', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Play_ByPlayerOFor22When22IsNotEmpty_ShouldNotPlaceSymbolOOnGameBoadrdAt22()
        {
            _boardArray[1, 1] = 'X';
            var gameBoard = new GameBoard(_boardArray);
            var playLocation = new Tuple<int, int>(2, 2);

            gameBoard.Play(_playerO, playLocation);

            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', 'X', ' ', ' ', ' ', ' ');
        }
        
        [Test]
        public void ToString_WhenGameBoardIsBlank_ShouldCreateAFormattedStringWithSpaces()
        {
            var gameBoard = new GameBoard(_boardArray);

            var expectedGameBoardString = GenerateGameBoardString(' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
            Assert.That(gameBoard.ToString(), Is.EqualTo(expectedGameBoardString));
        }

        [Test]
        public void ToString_WhenGameBoardIsNotBlank_ShouldCreateAFormattedStringWithValues()
        {
            _boardArray[0, 0] = 'O';
            _boardArray[0, 2] = 'X';
            _boardArray[2, 0] = 'X';
            _boardArray[2, 2] = 'O';
            var gameBoard = new GameBoard(_boardArray);

            var expectedGameBoardString = GenerateGameBoardString('O', ' ', 'X', ' ', ' ', ' ', 'X', ' ', 'O');
            Assert.That(gameBoard.ToString(), Is.EqualTo(expectedGameBoardString));
        }
        
        private static string GenerateGameBoardString(char val11, char val12, char val13, char val21, char val22, char val23, char val31, char val32, char val33)
        {
            return string.Format(DisplayFormat, val11, val12, val13, val21, val22, val23, val31, val32, val33);
        }

        private static void AssertGameBoardStatus(GameBoard gameBoard, char val11, char val12, char val13, char val21, char val22, char val23, char val31, char val32, char val33)
        {
            // Could have updated _boardArray and compared it with gameBoardValues rather than comparing 
            // individual values but that would require same array manipulation as production code
            // which I wanted to avoid
            var gameBoardValues = gameBoard.GameBoardValues;
            Assert.That(gameBoardValues[0,0], Is.EqualTo(val11));
            Assert.That(gameBoardValues[0,1], Is.EqualTo(val12));
            Assert.That(gameBoardValues[0,2], Is.EqualTo(val13));

            Assert.That(gameBoardValues[1, 0], Is.EqualTo(val21));
            Assert.That(gameBoardValues[1, 1], Is.EqualTo(val22));
            Assert.That(gameBoardValues[1, 2], Is.EqualTo(val23));

            Assert.That(gameBoardValues[2, 0], Is.EqualTo(val31));
            Assert.That(gameBoardValues[2, 1], Is.EqualTo(val32));
            Assert.That(gameBoardValues[2, 2], Is.EqualTo(val33));
        }
    }
}
