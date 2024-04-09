using AsteroidsClassLib.Model;
using CommunityToolkit.Maui.Storage;
using System.Text;

namespace AsteroidsClassLib.Persistence
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
        public async void Save(GameField[,] _gameTable, GameField _player)
        {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < 11; j++)
            {
                for (int i = 0; i < 11; i++)
                {
                    if (_gameTable[i, j].isAsteroid)
                    {
                        sb.Append('1');
                    }
                    else if (i == _player.col && j == _player.row)
                    {
                        sb.Append('2');
                    }
                    else
                    {
                        sb.Append('0');
                    }
                }
                sb.Append('\n');
            }
            using var stream = new MemoryStream(Encoding.Default.GetBytes(sb.ToString()));
            var fileSaveResult = await FileSaver.Default.SaveAsync("test.txt", stream, new CancellationToken());
        }
    }
}
