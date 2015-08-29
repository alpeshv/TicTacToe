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

        // Next 2 test sets are simlar but with different purposes. We need to rely on toString method to inspect private gameboard value array.
        // Could have made gameboard value array public to test constructor and toString method independently
        // Not using AssertGameBoardStatus method in next 2 tests to keep toString method visible in the tests
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
        
        private static string GenerateGameBoardString(char val11, char val12, char val13, char val21, char val22, char val23, char val31, char val32, char val33)
        {
            return string.Format(DisplayFormat, val11, val12, val13, val21, val22, val23, val31, val32, val33);
        }

        private static void AssertGameBoardStatus(GameBoard gameBoard, char val11, char val12, char val13, char val21, char val22, char val23, char val31, char val32, char val33)
        {
            var expectedGameBoardString = GenerateGameBoardString(val11, val12, val13, val21, val22, val23, val31, val32, val33);
            Assert.That(gameBoard.ToString(), Is.EqualTo(expectedGameBoardString));
        }
    }
}
