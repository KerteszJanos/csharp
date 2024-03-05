using Asteroids.WinForms.Model;
using Asteroids.WinForms.Persistence;
using AsteroidsConsole.Persistence;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;


namespace Asteroids.Model
{
    public class GameModel
    {
        #region fields
        private FileManager _fileManager;

        private bool _gameIsStarted = false;
        public bool gameIsStarted
        {
            get { return _gameIsStarted; }
            set { _gameIsStarted = value; }
        }
        private bool gameOver = false;
        public bool _gameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
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
        }
        public void resetGame()
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].isAsteroid = false;
            }
            asteroids.Clear();
        }
        public void Load(String path)
        {
            (_gameTable, _player, asteroids) = _fileManager.Load(path,player,asteroids);
        }
        #endregion

        #region playerMovement
        public void playerMoveA()
        {
            if (_player.col != 0)
            {
                if (_gameTable[_player.col - 1, _player.row].isAsteroid)
                {
                    _gameOver = true;
                }
                else
                {
                    _player = _gameTable[_player.col - 1, 10];
                }
            }
        }
        public void playerMoveD()
        {
            if (_player.col != 10)
            {
                if (_gameTable[_player.col + 1, _player.row].isAsteroid)
                {
                    _gameOver = true;
                }
                else
                {
                    _player = _gameTable[_player.col + 1, 10];
                }
            }
        }
        #endregion

        #region tableMethods
        public int asteroidGenerating()
        {
            int newAsteroid = _rnd.Next(1,11);
            randoms.Add(newAsteroid);
            asteroids.Add(_gameTable[newAsteroid, 0]);
            _gameTable[newAsteroid, 0].isAsteroid = true;
            return newAsteroid;
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
                        _gameOver = true;
                    }
                    else
                    {
                        asteroid.isAsteroid = false;
                        newAsteroids.Add(_gameTable[asteroid.col, asteroid.row + 1]);
                        _gameTable[asteroid.col, asteroid.row + 1].isAsteroid = true;
                    }
                }
                else
                {
                    asteroid.isAsteroid = false;
                }
            }
            asteroids = newAsteroids;
        }
        #endregion
    }
}
