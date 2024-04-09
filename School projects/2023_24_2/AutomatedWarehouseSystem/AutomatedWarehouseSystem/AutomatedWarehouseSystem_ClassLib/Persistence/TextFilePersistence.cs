using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Data;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    public class TextFilePersistence : IPersistence
    {
        #region Private fields

        private ConfigFile _configfile;
        private Map _map = null!;
        private Robot[] _robots = null!;
        private Destination[] _destinations = null!;

        #endregion

        #region Constructor

        public TextFilePersistence()
        {
            _configfile = new ConfigFile();
        }

        #endregion

        #region Public methods

        /// <summary>The path should be the name of the config file. It returns:
        /// <list type="bullet">
        /// <item><description>A map</description>
        /// </item>
        /// <item>
        /// <description>An array of robots</description>
        /// </item>
        /// <item>
        /// <description>An array of destinations</description>
        /// </item>
        /// <item>
        /// <description>The number of destinations that the Contol Unit gets at once</description>
        /// </item>
        /// <item>
        /// <description>The name of the task assignment startegy</description>
        /// </item>
        /// </list>
        /// </summary>
        public async Task<(Map, Robot[], Destination[], int, string)> LoadConfigFileAsync(String path)
        {
            //path = @"../Files/" + path;
            if (path == null)
                throw new DataException("Error occurred during reading: The file doesn't exist.");
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string jsontext = (await reader.ReadToEndAsync() ?? String.Empty);
                    _configfile = JsonSerializer.Deserialize<ConfigFile>(jsontext)!;
                    await LoadMapAsync(@"../Files/" + _configfile.mapFile);
                    await LoadRobotsAsync(@"../Files/" + _configfile.agentFile, _map.Width);
                    await LoadDestinationsAsync(@"../Files/" + _configfile.taskFile, _map.Width);

                    return (_map, _robots, _destinations, _configfile.numTasksReveal, _configfile.taskAssignmentStrategy);
                }
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
            }
        }

        #endregion

        #region Private methods

        private async Task LoadMapAsync(String path)
        {
            if (path == null)
                throw new DataException("Error occurred during reading: The file doesn't exist.");

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = await reader.ReadLineAsync() ?? String.Empty;
                    string type = line.Split(" ")[1];

                    line = await reader.ReadLineAsync() ?? String.Empty;
                    int height = Convert.ToInt32(line.Split(" ")[1]);

                    line = await reader.ReadLineAsync() ?? String.Empty;
                    int width = Convert.ToInt32(line.Split(" ")[1]);

                    line = await reader.ReadLineAsync() ?? String.Empty;

                    bool[,] table = new bool[height, width];
                    for (int i = 0; i < height; i++)
                    {
                        line = await reader.ReadLineAsync() ?? String.Empty;
                        for (int j = 0; j < width; j++)
                        {
                            table[i, j] = (line[j] == '.');
                        }
                    }

                    _map = new Map(type, height, width, table);
                }
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
            }
        }
        private async Task LoadRobotsAsync(String path, int mapWidth)
        {
            if (path == null)
                throw new DataException("Error occurred during reading: The file doesn't exist.");

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = await reader.ReadLineAsync() ?? String.Empty;
                    int robotCount = Convert.ToInt32(line);

                    _robots = new Robot[robotCount];
                    for (int i = 0; i < robotCount; i++)
                    {
                        int integerCoord = Convert.ToInt32(await reader.ReadLineAsync() ?? String.Empty);
                        int x = integerCoord / mapWidth;
                        int y = integerCoord % mapWidth;
                        Robot robot = new Robot(x, y);
                        _robots[i] = robot;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
            }
        }
        private async Task LoadDestinationsAsync(String path, int mapWidth)
        {
            if (path == null)
                throw new DataException("Error occurred during reading: The file doesn't exist.");

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = await reader.ReadLineAsync() ?? String.Empty;
                    int destCount = Convert.ToInt32(line);

                    _destinations = new Destination[destCount];
                    for (int i = 0; i < destCount; i++)
                    {
                        int integerCoord = Convert.ToInt32(await reader.ReadLineAsync() ?? String.Empty);
                        int x = integerCoord / mapWidth;
                        int y = integerCoord % mapWidth;
                        Destination destination = new Destination(x, y);
                        _destinations[i] = destination;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException(ex.Message);
            }
        }

        #endregion

        #region Test methods
        //These methods were written only for testing purposes.
        //Should not be used in the program.

        //Printing methods
        public void PrintConfig()
        {
            Console.WriteLine("Map file: {0}", _configfile.mapFile);
            Console.WriteLine("Agent file: {0}", _configfile.agentFile);
            Console.WriteLine("Team size: {0}", _configfile.teamSize);
            Console.WriteLine("Task file: {0}", _configfile.taskFile);
            Console.WriteLine("Num tasks reveal: {0}", _configfile.numTasksReveal);
            Console.WriteLine("Task assignment strategy: {0}", _configfile.taskAssignmentStrategy);
        }
        public void PrintMap()
        {
            for (int i = 0; i < _map.Height; i++)
            {
                for (int j = 0; j < _map.Width; j++)
                {
                    if (_map[i, j])
                        Console.Write('.');
                    else
                        Console.Write('@');
                }
                Console.WriteLine();
            }
        }
        public void PrintRobots()
        {
            for (int i = 0; i < _robots.Length; i++)
            {
                Console.WriteLine("id: {0}, x: {1}, y: {2}", _robots[i].Id, _robots[i].X, _robots[i].Y);
            }
        }
        public void PrintDestinations()
        {
            for (int i = 0; i < _destinations.Length; i++)
            {
                Console.WriteLine("id: {0}, x: {1}, y: {2}", _destinations[i].Id, _destinations[i].X, _destinations[i].Y);
            }
        }

        //Loading and printing methods
        public void LoadConfigTestAsync()
        {
            LoadConfigFileAsync("random_20_config.json").Wait();

            Console.WriteLine("Config file:\n");
            PrintConfig();
            Console.WriteLine("Map file:\n");
            PrintMap();
            Console.WriteLine("Robots file:\n");
            PrintRobots();
            Console.WriteLine("Tasks file:\n");
            PrintDestinations();
        }
        public async Task LoadConfigTestAsync2()
        {
            (Map m, Robot[] r, Destination[] d, int ntr, string tas) = await LoadConfigFileAsync("random_20_config.json");

            _map = m;
            _robots = r;
            _destinations = d;
            _configfile.numTasksReveal = ntr;
            _configfile.taskAssignmentStrategy = tas;

            Console.WriteLine("Config file:\n");
            PrintConfig();
            Console.WriteLine("Map file:\n");
            PrintMap();
            Console.WriteLine("Robots file:\n");
            PrintRobots();
            Console.WriteLine("Tasks file:\n");
            PrintDestinations();
        }
        public void LoadMapTest()
        {
            LoadMapAsync(@"../Files/maps/random-32-32-20.map").Wait();
            PrintMap();
        }
        public void LoadRobotsTest()
        {
            LoadRobotsAsync(@"../Files/agents/random_20.agents", 32).Wait();
            PrintRobots();
        }
        public void LoadDestsTest()
        {
            LoadDestinationsAsync(@"../Files/tasks/random-32-32-20.tasks", 32).Wait();
            PrintDestinations();
        }

        #endregion
    }
}
