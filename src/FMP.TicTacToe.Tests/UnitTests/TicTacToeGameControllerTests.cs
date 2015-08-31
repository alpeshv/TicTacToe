using System.Threading;
using FMP.TicTacToe.Abstract;
using Moq;
using NUnit.Framework;

namespace FMP.TicTacToe.Tests.UnitTests
{
    [TestFixture]
    public class TicTacToeGameControllerTests
    {
        private Mock<ITicTacToeGame> _gameMock;
        private Mock<IPauseHelper> _pauseProviderMock;
        private TicTacToeGameController _gameController;

        [SetUp]
        public void SetUp()
        {
            _gameMock = new Mock<ITicTacToeGame>();
            _pauseProviderMock = new Mock<IPauseHelper>();
            _gameController = new TicTacToeGameController(_gameMock.Object, _pauseProviderMock.Object);
        }

        [Test]
        public void StartGame_ControlsGameFlow()
        {
            var callOrder = 0;
            _gameMock.Setup(x => x.Reset()).Callback(() => Assert.That(callOrder++, Is.EqualTo(0)));
            _gameMock.Setup(x => x.DisplayGameBoard()).Callback(() => Assert.That(callOrder++, Is.EqualTo(1).Or.EqualTo(5)));
            _pauseProviderMock.Setup(x => x.PauseFor(1000)).Callback(() => Assert.That(callOrder++, Is.EqualTo(2)));
            _gameMock.Setup(x => x.SetCurrentPlayer()).Callback(() => Assert.That(callOrder++, Is.EqualTo(3)));
            _gameMock.Setup(x => x.CurrentPlayerPlay()).Callback(() => Assert.That(callOrder++, Is.EqualTo(4)));
            _gameMock.Setup(x => x.IsGameOver()).Returns(true).Callback(() => Assert.That(callOrder++, Is.EqualTo(6)));
            _gameMock.Setup(x => x.DisplayResult()).Callback(() => Assert.That(callOrder++, Is.EqualTo(7)));

            _gameController.StartGame();

            _gameMock.Verify(m => m.Reset(), Times.Once);
            _gameMock.Verify(m => m.DisplayGameBoard(), Times.Exactly(2));
            _gameMock.Verify(m => m.SetCurrentPlayer(), Times.Once);
            _gameMock.Verify(m => m.CurrentPlayerPlay(), Times.Once);
            _pauseProviderMock.Verify(m => m.PauseFor(1000), Times.Once);
            _gameMock.Verify(m => m.IsGameOver(), Times.Once);
            _gameMock.Verify(m => m.DisplayResult(), Times.Once);
        }
        
        [Test]
        public void StartGame_ContinuesGamePlayUntilGameIsOver()
        {
            _gameMock.SetupSequence(x => x.IsGameOver())
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(false)
            .Returns(true);

            _gameController.StartGame();

            _gameMock.Verify(m => m.Reset(), Times.Once);
            _gameMock.Verify(m => m.DisplayGameBoard(), Times.Exactly(6));
            _gameMock.Verify(m => m.SetCurrentPlayer(), Times.Exactly(5));
            _gameMock.Verify(m => m.CurrentPlayerPlay(), Times.Exactly(5));
            _pauseProviderMock.Verify(m => m.PauseFor(1000), Times.Exactly(5));
            _gameMock.Verify(m => m.IsGameOver(), Times.Exactly(5));
            _gameMock.Verify(m => m.DisplayResult(), Times.Once);
        }
    }




}
