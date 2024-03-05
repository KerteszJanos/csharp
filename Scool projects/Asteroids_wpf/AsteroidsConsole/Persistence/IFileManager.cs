using AsteroidsConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsConsole.Persistence
{
    public interface IFileManager
    {
        public (GameField[,], GameField, List<GameField>) Load(String path, GameField player, List<GameField> asteroids);
        public void Save(String path, GameField[,] _gameTable, GameField _player);
    }
}
