using AsteroidsConsole.Persistence;


namespace AsteroidsConsole.Model
{
    public class GameModel
    {
        #region fields
        private FileManager _fileManager;
        public FileManager FileManager 
        {
            get { return _fileManager; }
        }

        private bool _gameIsStarted = false;

        public bool gameIsStarted
        {
            get { return _gameIsStarted; }
            set { _gameIsStarted = value; }
        }

        private List<int> _randoms = new List<int>();
        public List<int> randoms
        {
            get { return _randoms; }
            set { _randoms = value; }
        }
        //startNewGame
        //[col, row]
        private GameField[,] gameTable = new GameField[11, 11]; //mezőértékek
        public GameField[,] _gameTable
        {
            get { return gameTable; }
            set { gameTable = value; }
        }
        private GameField player = new GameField(5, 10);
        public GameField _player
        {
            get { return player; }
            set { player = value; }
        }

        //asteroidGenerating
        private Random _rnd = new Random();
        private List<GameField> _asteroids = new List<GameField>();
        public List<GameField> asteroids
        {
            get { return _asteroids; }
            set { _asteroids = value; }
        }
        #endregion

        public GameModel(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        #region menuMethods
        public void startNewGame()
        {
            _gameTable = new GameField[11, 11];
            for (int i = 0; i < 11; i++)
            {
                for (int k = 0; k < 11; k++)
                {
                    _gameTable[i, k] = new GameField(i, k);
                }
            }
            _player = _gameTable[5, 10];
            OnFieldChanged(_player.row, player.col, FieldChangedEventArgs.FieldStatus.Player);
        }
        public void resetGame()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].isAsteroid = false;
                OnFieldChanged(asteroids[i].row, asteroids[i].col, FieldChangedEventArgs.FieldStatus.Nothing);
            }
            asteroids.Clear();
            OnFieldChanged(player.row, player.col, FieldChangedEventArgs.FieldStatus.Nothing);
        }
        public void Load(String path)
        {
            (_gameTable, _player, asteroids) = _fileManager.Load(path, player, asteroids);
            OnFieldChanged(_player.row, player.col, FieldChangedEventArgs.FieldStatus.Player);
        }
        #endregion

        #region playerMovement
        public void playerMoveA()
        {
            if (_player.col != 0)
            {
                if (_gameTable[_player.col - 1, _player.row].isAsteroid)
                {
                    OnGameOver();
                }
                else
                {
                    OnFieldChanged(_player.row, player.col, FieldChangedEventArgs.FieldStatus.Nothing);
                    _player = _gameTable[_player.col - 1, 10];
                    OnFieldChanged(_player.row, player.col, FieldChangedEventArgs.FieldStatus.Player);
                }
            }
        }
        public void playerMoveD()
        {
            if (_player.col != 10)
            {
                if (_gameTable[_player.col + 1, _player.row].isAsteroid)
                {
                    OnGameOver();
                }
                else
                {
                    OnFieldChanged(_player.row, player.col, FieldChangedEventArgs.FieldStatus.Nothing);
                    _player = _gameTable[_player.col + 1, 10];
                    OnFieldChanged(_player.row, player.col, FieldChangedEventArgs.FieldStatus.Player);
                }
            }
        }
        #endregion

        #region tableMethods
        public void asteroidGenerating()
        {
            int newAsteroid = _rnd.Next(1, 11);
            randoms.Add(newAsteroid);
            asteroids.Add(_gameTable[newAsteroid, 0]);
            _gameTable[newAsteroid, 0].isAsteroid = true;
            OnFieldChanged(0, newAsteroid, FieldChangedEventArgs.FieldStatus.Asteroid);
            //OnAsteroidGenerated(newAsteroid);
        }
        public void refreshTable()
        {

            List<GameField> newAsteroids = new List<GameField>();
            foreach (var asteroid in asteroids)
            {
                if (asteroid.row != 10) //row++
                {
                    if (_player.col == asteroid.col && _player.row == asteroid.row + 1)
                    {
                        OnGameOver();
                        return;
                    }
                    else
                    {
                        asteroid.isAsteroid = false;
                        OnFieldChanged(asteroid.row, asteroid.col, FieldChangedEventArgs.FieldStatus.Nothing);

                        newAsteroids.Add(_gameTable[asteroid.col, asteroid.row + 1]);
                        _gameTable[asteroid.col, asteroid.row + 1].isAsteroid = true;
                        OnFieldChanged(asteroid.row + 1, asteroid.col, FieldChangedEventArgs.FieldStatus.Asteroid);
                    }
                }
                else
                {
                    asteroid.isAsteroid = false;
                    OnFieldChanged(asteroid.row, asteroid.col, FieldChangedEventArgs.FieldStatus.Nothing);
                }
            }
            asteroids = newAsteroids;
        }
        #endregion

        public event EventHandler? gameOver;

        private void OnGameOver()
        {
            gameOver?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<FieldChangedEventArgs>? FieldChanged;

        private void OnFieldChanged(int x, int y, FieldChangedEventArgs.FieldStatus fs) //jol parameterezve field utan hivas
        {
            FieldChanged?.Invoke(this, new FieldChangedEventArgs(x, y, fs));
        }
    }
}
