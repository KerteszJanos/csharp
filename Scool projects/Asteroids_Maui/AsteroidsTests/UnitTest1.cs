using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using AsteroidsClassLib.Persistence;
using AsteroidsClassLib.Model;

namespace AsteroidsTests
{
    [TestClass]
    public class GameLogicsTests
    {
        private GameModel _model = null!;
        private GameField[,] _mockedTable = null!; // mockolt játéktábla
        private Mock<IFileManager> _mock = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockedTable = new GameField[11, 11];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    _mockedTable[i, j] = new GameField(i, j);
                }
            }
            _mock = new Mock<IFileManager>();
            _mock.Setup(mock => mock.Load(It.IsAny<String>(), new GameField(6, 10), new List<GameField>())).Returns(() => (_mockedTable, new GameField(6, 10), new List<GameField>()));
            _model = new GameModel(new FileManager());
            _model._player = _mockedTable[6, 10];
        }

        [TestMethod]
        public void startNewGameTest()
        {
            _model.startNewGame();

            Assert.AreEqual(121, _model._gameTable.Length);
            Assert.AreEqual(5, _model._player.col);
            Assert.AreEqual(10, _model._player.row);
        }
        [TestMethod]
        public void asteroidGenerating_resetGameTest()
        {
            _model.startNewGame();
            _model.asteroidGenerating();
            _model.asteroidGenerating();

            Assert.AreEqual(true, _model._gameTable[_model.randoms[0], 0].isAsteroid);
            Assert.AreEqual(true, _model._gameTable[_model.randoms[1], 0].isAsteroid);
            Assert.AreEqual(false, _model._gameTable[4, 0].isAsteroid);
            Assert.AreEqual(2, _model.asteroids.Count);

            _model.resetGame();
            Assert.AreEqual(5, _model._player.col);
            Assert.AreEqual(10, _model._player.row);
            Assert.AreEqual(false, _model._gameTable[_model.randoms[0], 0].isAsteroid);
            Assert.AreEqual(false, _model._gameTable[_model.randoms[1], 0].isAsteroid);
            Assert.AreEqual(false, _model._gameTable[4, 0].isAsteroid);
            Assert.AreEqual(0, _model.asteroids.Count);
        }
        [TestMethod]
        public void playerMoveA_playerMoveDTest()
        {
            _model.startNewGame();
            _model.playerMoveA();
            _model.playerMoveA();
            Assert.AreEqual(3, _model._player.col);
            _model.playerMoveD();
            Assert.AreEqual(4, _model._player.col);
        }
        [TestMethod]
        public void refreshTableTest()
        {
            _model.startNewGame();
            _model.asteroidGenerating();
            _model.asteroidGenerating();
            _model.refreshTable();
            Assert.IsTrue(_model._gameTable[_model.randoms[0], 1].isAsteroid);
            Assert.IsTrue(!_model._gameTable[_model.randoms[0], 0].isAsteroid);
            Assert.IsTrue(_model._gameTable[_model.randoms[1], 1].isAsteroid);
            Assert.IsTrue(!_model._gameTable[_model.randoms[1], 0].isAsteroid);
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            Assert.IsTrue(_model.asteroids.Count == 0);
        }

        public class GameIsOver
        {
            public bool gameOver = false;
            public void gameIsover(object? sender, EventArgs e)
            {
                gameOver = true;
            }
        }
        [TestMethod]
        public void GameOverFromAboveTest()
        {
            GameIsOver gameIsOver = new GameIsOver();
            _model.gameOver += gameIsOver.gameIsover;
            _model.startNewGame();
            _model._gameTable[5, 0].isAsteroid = true;
            _model.asteroids.Add(_model._gameTable[5, 0]);
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            Assert.IsTrue(gameIsOver.gameOver);
        }
        [TestMethod]
        public void GameOverFromNextToTest()
        {
            GameIsOver gameIsOver = new GameIsOver();
            _model.gameOver += gameIsOver.gameIsover;
            _model.startNewGame();
            _model._gameTable[4, 0].isAsteroid = true;
            _model.asteroids.Add(_model._gameTable[4, 0]);
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.refreshTable();
            _model.playerMoveA();
            Assert.IsTrue(gameIsOver.gameOver);
        }
    }
}