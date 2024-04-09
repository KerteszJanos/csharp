using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asteroids.WinForms.Model;
using AsteroidsConsole.Persistence;

namespace Asteroids.WinForms.Persistence
{
    public class FileManager : IFileManager
    {
        public (GameField[,],GameField,List<GameField>) Load(String path, GameField player, List<GameField> asteroids)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // fájl megnyitása
                {
                    GameField[,] gameTable = new GameField[11, 11];
                    string[] fileData = reader.ReadToEnd().Split('\n');
                    for (int j = 0; j < 11; j++)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            gameTable[i, j] = new GameField(i, j);
                            if (fileData[j][i] == '1')
                            {
                                gameTable[i, j].isAsteroid = true;
                                asteroids.Add(gameTable[i, j]);
                            }
                            else if (fileData[j][i] == '2')
                            {
                                player = gameTable[i, j];
                            }
                        }
                    }
                    return (gameTable, player, asteroids);
                }
            }
            catch
            {
                throw new FileManagerException();
            }
        }
        public void Save(String path, GameField[,] _gameTable, GameField _player)
        {
            using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
            {
                for (int j = 0; j < 11; j++)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        if (_gameTable[i, j].isAsteroid)
                        {
                            writer.Write('1');
                        }
                        else if (i == _player.col && j == _player.row)
                        {
                            writer.Write('2');
                        }
                        else
                        {
                            writer.Write('0');
                        }
                    }
                    writer.Write('\n');
                }
            }
        }
    }
}
