using AsteroidsClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsClassLib.Model
{
    public class GameModel
    {
        #region Fields
        private GameField[,] _gameTable = null!;

        private int _tableSize = 10;
        #endregion

        #region Getters/Setters
        public int tableSize
        {
            get
            {
                return _tableSize;
            }
            set
            {
                _tableSize = value;
            }
        }
        #endregion

        public GameModel()
        {

        }

        #region menu Methods
        public void modelNewGame()
        {
            createGametable();
            //gets prev tableSize from the prev game
            onNewGame(_tableSize);
        }
        public void modelSetTable10x10()
        {
            tableSize = 10;
            createGametable();
            onNewGame(_tableSize);
        }
        public void modelSetTable20x20()
        {
            tableSize = 20;
            createGametable();
            onNewGame(_tableSize);
        }
        #endregion

        #region public Methods
        public void modelButtonClicked(int row, int col)
        {
            if (_gameTable[row, col].isBlack)
            {
                _gameTable[row, col].isBlack = false;
            }
            else
            {
                _gameTable[row, col].isBlack = true;
            }
            onGameAdvance(_gameTable);
            if (checkGameOver())
            {
                onGameOver(true);
            }
        }
        #endregion

        #region private Methods         
        //main game logic
        private void createGametable()
        {
            _gameTable = new GameField[_tableSize, _tableSize];
            for (int i = 0; i < _tableSize; i++)
            {
                for (int j = 0; j < _tableSize; j++)
                {
                    _gameTable[i, j] = new GameField(i, j);
                }
            }
        }
        private bool checkGameOver()
        {
            for (int i = 0; i < _tableSize; i++)
            {
                for (int j = 0; j < _tableSize; j++)
                {
                    if (!_gameTable[i, j].isBlack)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region events/event methods
        public event EventHandler<NewGameEventArgs>? NewGame;
        public event EventHandler<GameAdvanceEventArgs>? GameAdvance;
        public event EventHandler<GameOverEventArgs>? GameOver;

        private void onNewGame(int size)
        {
            NewGame?.Invoke(this, new NewGameEventArgs(size));
        }
        private void onGameAdvance(GameField[,] gameTable)
        {
            GameAdvance?.Invoke(this, new GameAdvanceEventArgs(gameTable));
        }
        private void onGameOver(bool isWon)
        {
            GameOver?.Invoke(this, new GameOverEventArgs(isWon));
        }
        #endregion
    }
}
