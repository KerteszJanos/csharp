using AutomatedWarehouseSystem_ClassLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Interface of the file manage persistence
    /// </summary>
    public interface IPersistence
    {
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
        Task<(Map, Robot[], Destination[], int, string)> LoadConfigFileAsync(String path);
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
        Task<(string, bool, int, Robot[], int, int, int, float[], List<Error>, List<List<Event>>, List<Destination>)> LoadLogFileAsync(String filePath);
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
        /// <description>Tasks (id of the task, start destination id, end destination id) (the assigned and finished stepnum is empty)</description>
        /// </item>
        /// </list>
        /// </summary>
        Task SaveLogFileAsync(string filePath, string actionModel, bool allValid, int teamSize, Robot[] robots, int numTaskFinished,
            int sumOfCost, int makespan, float[] plannerTimes, List<Error> errors, List<List<Event>> events, List<Destination> tasks, Robot[] robotStart);
        /// <summary>
        /// Param: The map's path. Returns a map
        /// </summary>
        Task<Map> LoadMapAsync(String path);
    }
}
