using AsteroidsClassLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsClassLib.Persistence
{
    public interface IFileManager
    {
        public (GameField[,], GameField, List<GameField>) Load(String path, GameField player, List<GameField> asteroids);
        public void Save(GameField[,] _gameTable, GameField _player) { }
    }
}
