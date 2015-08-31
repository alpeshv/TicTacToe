using System;
using NUnit.Framework;

namespace FMP.TicTacToe.Tests.UnitTests
{
    [TestFixture]
    class GameBoardTests
    {
        private const string DisplayFormat = "\n{0}|{1}|{2}\n_ _ _\n{3}|{4}|{5}\n_ _ _\n{6}|{7}|{8}";
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
        public void CreatingNewGameBoard_WithoutPassingInitialValues_InitializesGameBoardWithEmptyValues()
        {
            var gameBoard = new GameBoard();
            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void CreatingNewGameBoard_WithEmptyValues_InitializesGameBoardWithEmptyValues()
        {
            var gameBoard = new GameBoard(_boardArray);
            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void CreatingNewGameBoard_WithSomeNonEmptyValues_InitializesGameBoardWithNonEmptyValuesAtGivenLocations()
        {
            _boardArray[0, 0] = 'X';

            var gameBoard = new GameBoard(_boardArray);

            AssertGameBoardStatus(gameBoard, 'X', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Reset_ResetsAllGameBoardValues()
        {
            _boardArray[0, 0] = 'O';
            _boardArray[0, 1] = 'O';
            _boardArray[0, 2] = 'X';

            _boardArray[1, 0] = 'X';
            _boardArray[1, 1] = 'O';
            _boardArray[1, 2] = 'O';

            _boardArray[2, 0] = 'O';
            _boardArray[2, 1] = 'X';
            _boardArray[2, 2] = 'X';

            var gameBoard = new GameBoard(_boardArray);
            gameBoard.Reset();

            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Play_ByPlayerOFor22When22IsEmpty_PlacesSymbolOOnGameBoadrdAt22()
        {
            var gameBoard = new GameBoard(_boardArray);

            gameBoard.Play(_playerO, 2, 2);

            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', 'O', ' ', ' ', ' ', ' ');
        }
        
        [Test]
        public void Play_ByPlayerXFor13When13IsEmpty_PlacesSymbolXOnGameBoadrdAt13()
        {
            var gameBoard = new GameBoard(_boardArray);

            gameBoard.Play(_playerX, 1, 3);

            AssertGameBoardStatus(gameBoard, ' ', ' ', 'X', ' ', ' ', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Play_ByPlayerOFor22When22IsEmpty_ReturnsTrue()
        {
            var gameBoard = new GameBoard(_boardArray);

            var result = gameBoard.Play(_playerO, 2, 2);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Play_ByPlayerOFor22When22IsNotEmpty_DoesNotPlaceSymbolOOnGameBoadrdAt22()
        {
            _boardArray[1, 1] = 'X';
            var gameBoard = new GameBoard(_boardArray);

            gameBoard.Play(_playerO, 2, 2);

            AssertGameBoardStatus(gameBoard, ' ', ' ', ' ', ' ', 'X', ' ', ' ', ' ', ' ');
        }

        [Test]
        public void Play_ByPlayerOFor22When22IsNotEmpty_ReturnsFalse()
        {
            _boardArray[1, 1] = 'X';
            var gameBoard = new GameBoard(_boardArray);

            var result = gameBoard.Play(_playerO, 2, 2);

            Assert.That(result, Is.False);
        }

        [Test]
        public void ToString_WhenGameBoardIsBlank_ReturnsAFormattedStringWithSpaces()
        {
            var gameBoard = new GameBoard(_boardArray);

            var expectedGameBoardString = GenerateGameBoardString(' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ');
            Assert.That(gameBoard.ToString(), Is.EqualTo(expectedGameBoardString));
        }

        [Test]
        public void ToString_WhenGameBoardIsNotBlank_ReturnsAFormattedStringWithValues()
        {
            _boardArray[0, 0] = 'O';
            _boardArray[0, 2] = 'X';
            _boardArray[2, 0] = 'X';
            _boardArray[2, 2] = 'O';
            var gameBoard = new GameBoard(_boardArray);

            var expectedGameBoardString = GenerateGameBoardString('O', ' ', 'X', ' ', ' ', ' ', 'X', ' ', 'O');
            Assert.That(gameBoard.ToString(), Is.EqualTo(expectedGameBoardString));
        }

        // Testing two things together rather than repeating all scenarios for both the asserts separately
        [Test]
        public void IsGameOver_WhenGameBoardIsBlank_ReturnsFalseAndDoesNotSetWinnerFlag()
        {
            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.False);
            Assert.That(hasAPlayerWon, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void IsGameOver_When3SameCharactersAreInTheSameRow_ReturnsTrueAndSetsWinnerFlag(int row)
        {
            _boardArray[row, 0] = 'X';
            _boardArray[row, 1] = 'X';
            _boardArray[row, 2] = 'X';
            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.True);
            Assert.That(hasAPlayerWon, Is.True);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void IsGameOver_When3SameCharactersAreInTheSameColumn_ReturnsTrueAndSetsWinnerFlag(int column)
        {
            _boardArray[0, column] = 'O';
            _boardArray[1, column] = 'O';
            _boardArray[2, column] = 'O';
            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.True);
            Assert.That(hasAPlayerWon, Is.True);
        }

        [Test]
        public void IsGameOver_When3SameCharactersAreInTheForwardDiagonal_ReturnsTrueAndSetsWinnerFlag()
        {
            _boardArray[0, 0] = 'X';
            _boardArray[1, 1] = 'X';
            _boardArray[2, 2] = 'X';
            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.True);
            Assert.That(hasAPlayerWon, Is.True);
        }

        [Test]
        public void IsGameOver_When3SameCharactersAreInTheBackwardDiagonal_ReturnsTrueAndSetsWinnerFlag()
        {
            _boardArray[2, 0] = 'O';
            _boardArray[1, 1] = 'O';
            _boardArray[0, 2] = 'O';
            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.True);
            Assert.That(hasAPlayerWon, Is.True);
        }

        [Test]
        public void IsGameOver_When3SameCharactersAreNotInLineAndGameBoardIsNotFull_ReturnsFalseAndDoesNotSetWinnerFlag()
        {
            _boardArray[0, 0] = 'O';
            _boardArray[0, 2] = 'X';

            _boardArray[1, 0] = 'X';
            _boardArray[1, 1] = 'O';
            
            _boardArray[2, 0] = 'O';
            _boardArray[2, 1] = 'X';
            
            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.False);
            Assert.That(hasAPlayerWon, Is.False);
        }

        [Test]
        public void IsGameOver_When3SameCharactersAreNotInLineAndGameBoardIsFull_ReturnsTrueAndDoesNotSetWinnerFlag()
        {
            _boardArray[0, 0] = 'O';
            _boardArray[0, 1] = 'O';
            _boardArray[0, 2] = 'X';

            _boardArray[1, 0] = 'X';
            _boardArray[1, 1] = 'O';
            _boardArray[1, 2] = 'O';

            _boardArray[2, 0] = 'O';
            _boardArray[2, 1] = 'X';
            _boardArray[2, 2] = 'X';

            var gameBoard = new GameBoard(_boardArray);

            bool hasAPlayerWon;
            var result = gameBoard.IsGameOver(out hasAPlayerWon);

            Assert.That(result, Is.True);
            Assert.That(hasAPlayerWon, Is.False);
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
            Assert.That(gameBoardValues[0, 0], Is.EqualTo(val11));
            Assert.That(gameBoardValues[0, 1], Is.EqualTo(val12));
            Assert.That(gameBoardValues[0, 2], Is.EqualTo(val13));

            Assert.That(gameBoardValues[1, 0], Is.EqualTo(val21));
            Assert.That(gameBoardValues[1, 1], Is.EqualTo(val22));
            Assert.That(gameBoardValues[1, 2], Is.EqualTo(val23));

            Assert.That(gameBoardValues[2, 0], Is.EqualTo(val31));
            Assert.That(gameBoardValues[2, 1], Is.EqualTo(val32));
            Assert.That(gameBoardValues[2, 2], Is.EqualTo(val33));
        }
    }
}
