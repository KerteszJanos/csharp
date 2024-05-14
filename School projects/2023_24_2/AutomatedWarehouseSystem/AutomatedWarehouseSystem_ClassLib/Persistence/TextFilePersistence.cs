using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection.PortableExecutable;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;
using System.Collections;
using AutomatedWarehouseSystem_ClassLib.Model;
using static AutomatedWarehouseSystem_ClassLib.Persistence.DataException;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// File manage persistence
    /// </summary>
    public class TextFilePersistence : IPersistence
    {
        #region Private fields

        private ConfigFile _configfile;
        private Map _map = null!;
        private Robot[] _robots = null!;
        private Destination[] _destinations = null!;

        private LogFile _logFile;

        #endregion

        #region Constructor
        /// <summary>
        /// File manage constructor
        /// </summary>
        public TextFilePersistence()
        {
            _configfile = new ConfigFile();
            _logFile = new LogFile();
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
            if (path == null)
                throw new DataException("Error occurred during reading: The file doesn't exist.");
            try
            {
                string directory = Path.GetDirectoryName(path) ?? String.Empty;

                using (StreamReader reader = new StreamReader(path))
                {
                    string jsontext = (await reader.ReadToEndAsync() ?? String.Empty);
                    _configfile = JsonConvert.DeserializeObject<ConfigFile>(jsontext)!;
                    await LoadMapAsync(directory + "\\" + _configfile.mapFile);
                    await LoadRobotsAsync(directory + "\\" + _configfile.agentFile, _map.Width);
                    await LoadDestinationsAsync(directory + "\\" + _configfile.taskFile, _map.Width);

                    return (_map, _robots, _destinations, _configfile.numTasksReveal, _configfile.taskAssignmentStrategy);
                }
            }
            catch (Exception)
            {
                throw new LoadConfigFileFailed("Configuration file load failed! The path or file format is incorrect.");
            }
        }

        /// <summary>Param: The path of the logfile. It returns:
        /// <list type="bullet">
        /// <item><description>The name of the action model</description>
        /// </item>
        /// <item>
        /// <description>True if all steps were valid (If false, then error array is empty)</description>
        /// </item>
        /// <item>
        /// <description>Team size (robot count)</description>
        /// </item>
        /// <item>
        /// <description>Robots (with id, coords, actual path and planner path)</description>
        /// </item>
        /// <item>
        /// <description>Number of finished tasks</description>
        /// </item>
        /// <item>
        /// <description>Sum of all robots' actions</description>
        /// </item>
        /// <item>
        /// <description>The length of the simulation</description>
        /// </item>
        /// <item>
        /// <description>Planner times</description>
        /// </item>
        /// <item>
        /// <description>Errors (which two robots, stepnum, reason)</description>
        /// </item>
        /// <item>
        /// <description>Events (id of the task, stepnum, status)</description>
        /// </item>
        /// <item>
        /// <description>Tasks (id of the dest, x coord, y coord) (the assigned and finished stepnum is empty)</description>
        /// </item>
        /// </list>
        /// </summary>
        public async Task<(string, bool, int, Robot[], int, int, int, float[], List<Error>, List<List<Event>>, List<Destination>)> LoadLogFileAsync(String filePath)
        {
            if (filePath == null)
                throw new DataException("Error occurred during reading: The file doesn't exist.");

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsontext = (await reader.ReadToEndAsync() ?? String.Empty);
                    _logFile = JsonConvert.DeserializeObject<LogFile>(jsontext)!;

                    bool allValid = (_logFile.AllValid == "Yes");
                    Robot[] robots = new Robot[_logFile.teamSize];
                    for (int i = 0; i < _logFile.teamSize; i++)
                    {
                        string line1 = _logFile.actualPaths[i];
                        List<char> actpath = new List<char>();
                        foreach (char c in line1)
                        {
                            if (c != ',')
                            {
                                actpath.Add(c);
                            }
                        }
                        string line2 = _logFile.plannerPaths[i];
                        List<char> planpath = new List<char>();
                        foreach (char c in line2)
                        {
                            if (c != ',')
                            {
                                planpath.Add(c);
                            }
                        }
                        robots[i] = new Robot(Convert.ToInt32(_logFile.start[i][0]), Convert.ToInt32(_logFile.start[i][1]),
                                            Convert.ToChar(_logFile.start[i][2]), actpath, planpath);
                    }
                    List<Error> errors = new List<Error>();
                    foreach (var e in _logFile.errors)
                    {
                        Error error = new Error(Convert.ToInt32(e[0]), Convert.ToInt32(e[1]),
                                                Convert.ToInt32(e[2]), (Convert.ToString(e[3]) ?? String.Empty));
                        errors.Add(error);
                    }
                    List<List<Event>> events = new List<List<Event>>();
                    foreach (var robotE in _logFile.events)
                    {
                        List<Event> robotsEvents = new List<Event>();
                        foreach (var e in robotE)
                        {
                            Event rEvent = new Event(Convert.ToInt32(e[0]), Convert.ToInt32(e[1]),
                                                    (Convert.ToString(e[2]) ?? String.Empty));
                            robotsEvents.Add(rEvent);
                        }
                        events.Add(robotsEvents);
                    }
                    List<Destination> dests = new List<Destination>();
                    foreach (var t in _logFile.tasks)
                    {
                        Destination dest = new Destination(t[0], t[1], t[2]);
                        dests.Add(dest);
                    }

                    return (_logFile.actionModel, allValid, _logFile.teamSize, robots,
                            _logFile.numTaskFinished, _logFile.sumOfCost, _logFile.makespan, _logFile.plannerTimes,
                            errors, events, dests);
                }
            }
            catch (Exception)
            {
                throw new LoadLogFileFailed("Logfile load failed! The path or file format is incorrect.");
            }
        }

        /// <summary>Params:
        /// <list type="bullet">
        /// <item>
        /// <description>File path to save the logfile</description>
        /// </item>
        /// <item><description>The name of the action model</description>
        /// </item>
        /// <item>
        /// <description>True if all steps were valid (If false, then error array is empty)</description>
        /// </item>
        /// <item>
        /// <description>Team size (robot count)</description>
        /// </item>
        /// <item>
        /// <description>Robots (with id, coords, actual path and planner path)</description>
        /// </item>
        /// <item>
        /// <description>Number of finished tasks</description>
        /// </item>
        /// <item>
        /// <description>Sum of all robots' actions</description>
        /// </item>
        /// <item>
        /// <description>The length of the simulation</description>
        /// </item>
        /// <item>
        /// <description>Planner times</description>
        /// </item>
        /// <item>
        /// <description>Errors (which two robots, stepnum, reason)</description>
        /// </item>
        /// <item>
        /// <description>Events (id of the task, stepnum, status)</description>
        /// </item>
        /// <item>
        /// <description>Tasks (id of the dest, x coord, y coord) (the assigned and finished stepnum is empty)</description>
        /// </item>
        /// </list>
        /// </summary>
        public async Task SaveLogFileAsync(string filePath, string actionModel, bool allValid, int teamSize, Robot[] robots, int numTaskFinished,
            int sumOfCost, int makespan, float[] plannerTimes, List<Error> errors, List<List<Event>> events, List<Destination> dests, Robot[] robotStart)
        {
            try
            {
                List<List<object>> errorsList = new List<List<object>>();
                if (errors.Count != 0)
                {
                    errorsList = errors.Select(error => new List<object> { error.Robot1Id, error.Robot2Id, error.StepNum, error.Reason }).ToList();
                }
                List<List<int>> destList = dests.Select(dest => new List<int> { dest.Id, dest.X, dest.Y }).ToList();
                List<List<object>> startList = robotStart.Select(robot => new List<object> { robot.X, robot.Y, DirectionToChar(robot.Direction) }).ToList();
                List<List<List<object>>> eventsList = events.Select(robotEvents => robotEvents.Select(e => new List<object> { e.TaskId, e.StepNum, e.Status }).ToList()).ToList();

                var actualPaths = robots.Select(robot => string.Join(",", robot.ActualPath.Select(p => PathToChar(p)))).ToArray<string>();
                var plannerPaths = robots.Select(robot => string.Join(",", robot.PlannerPath.Select(p => PathToChar(p)))).ToArray<string>();

                LogFile logFile = new LogFile
                {
                    actionModel = actionModel,
                    AllValid = allValid ? "Yes" : "No",
                    teamSize = teamSize,
                    numTaskFinished = numTaskFinished,
                    sumOfCost = sumOfCost,
                    makespan = makespan,
                    plannerTimes = plannerTimes,
                    errors = errorsList,
                    tasks = destList,
                    actualPaths = actualPaths,
                    plannerPaths = plannerPaths,
                    start = startList,
                    events = eventsList
                };
                string jsonText = JsonConvert.SerializeObject(logFile, Formatting.Indented);
                
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    await writer.WriteAsync(jsonText);
                }
                
            }
            catch (Exception ex)
            {
                throw new DataException($"Error occurred during writing: {ex.Message}");
            }
        }
        /// <summary>
        /// Warehouse mapfile async load function
        /// </summary>
        public async Task<Map> LoadMapAsync(String path)
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
                    return _map;
                }
            }
            catch (Exception)
            {
                throw new LoadMapFailed("Map load failed! The path or file format is incorrect.");
            }
        }

        #endregion

        #region Private methods

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
        private static char DirectionToChar(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return 'N';
                case Direction.East:
                    return 'E';
                case Direction.West:
                    return 'W';
                case Direction.South:
                    return 'S';
                default:
                    return 'N';
            }
        }
        private static string PathToChar(PathEnum path)
        {
            switch (path)
            {
                case PathEnum.Left:
                    return "C";
                case PathEnum.Right:
                    return "R";
                case PathEnum.Forward:
                    return "F";
                case PathEnum.Wait:
                    return "W";
                case PathEnum.Timeout:
                    return "T";
                default:
                    return "W";
            }
        }
        #endregion

        #region Test methods
        //These methods were written only for testing purposes.
        //Should not be used in the program.

        /// <summary>
        /// Configuration file printing methods
        /// </summary>
        public void PrintConfig()
        {
            Console.WriteLine("Map file: {0}", _configfile.mapFile);
            Console.WriteLine("Agent file: {0}", _configfile.agentFile);
            Console.WriteLine("Team size: {0}", _configfile.teamSize);
            Console.WriteLine("Task file: {0}", _configfile.taskFile);
            Console.WriteLine("Num tasks reveal: {0}", _configfile.numTasksReveal);
            Console.WriteLine("Task assignment strategy: {0}", _configfile.taskAssignmentStrategy);
        }
        /// <summary>
        /// Map printing method
        /// </summary>
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
        /// <summary>
        /// Robots printing method
        /// </summary>
        public void PrintRobots()
        {
            for (int i = 0; i < _robots.Length; i++)
            {
                Console.WriteLine("id: {0}, x: {1}, y: {2}", _robots[i].Id, _robots[i].X, _robots[i].Y);
            }
        }
        /// <summary>
        /// Destination printing method
        /// </summary>
        public void PrintDestinations()
        {
            for (int i = 0; i < _destinations.Length; i++)
            {
                Console.WriteLine("id: {0}, x: {1}, y: {2}", _destinations[i].Id, _destinations[i].X, _destinations[i].Y);
            }
        }

        /// <summary>
        ///Loading and printing methods
        /// </summary>
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
        /// <summary>
        ///Loading and printing methods 2
        /// </summary>
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
        /// <summary>
        /// Load map test
        /// </summary>
        public void LoadMapTest()
        {
            LoadMapAsync(@"../Files/maps/random-32-32-20.map").Wait();
            PrintMap();
        }
        /// <summary>
        /// Load robot test
        /// </summary>
        public void LoadRobotsTest()
        {
            LoadRobotsAsync(@"../Files/agents/random_20.agents", 32).Wait();
            PrintRobots();
        }
        /// <summary>
        /// Load destination test
        /// </summary>
        public void LoadDestsTest()
        {
            LoadDestinationsAsync(@"../Files/tasks/random-32-32-20.tasks", 32).Wait();
            PrintDestinations();
        }

        #endregion
    }
}
