using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Log file class
    /// </summary>
    public class LogFile
    {
        #region Public properties
        /// <summary>
        /// Action modell method getter/setter
        /// </summary>
        public string actionModel { get; set; }
        /// <summary>
        /// Validation getter/setter
        /// </summary>
        public string AllValid { get; set; }
        /// <summary>
        /// Robots number getter/setter
        /// </summary>
        public int teamSize { get; set; }
        public List<List<object>> start { get; set; }
        /// <summary>
        /// Finished tasks number getter/setter
        /// </summary>
        public int numTaskFinished { get; set; }
        public int sumOfCost { get; set; }
        public int makespan { get; set; }
        /// <summary>
        /// List of actual paths getter/setter
        /// </summary>
        public string[] actualPaths { get; set; }
        /// <summary>
        /// List of planner paths getter/setter
        /// </summary>
        public string[] plannerPaths { get; set; }
        /// <summary>
        /// List of planner times getter/setter
        /// </summary>
        public float[] plannerTimes { get; set; }
        /// <summary>
        /// Logfile errors getter/setter
        /// </summary>
        public List<List<object>> errors { get; set; }
        /// <summary>
        /// Logfile events getter/setter
        /// </summary>
        public List<List<List<object>>> events { get; set; }
        /// <summary>
        /// Logfile tasks getter/setter
        /// </summary>
        public List<List<int>> tasks { get; set; }


        #endregion

        #region Constructors
        /// <summary>
        /// Logfile constructor with parameters
        /// </summary>
        public LogFile(string actionModel, string AllValid, int teamSize, List<List<object>> start, int numTaskFinished, int sumOfCost,
                   int makespan, string[] actualPaths, string[] plannerPaths, float[] plannerTimes,
                   List<List<object>> errors, List<List<List<object>>> events, List<List<int>> tasks)
        {
            this.actionModel = actionModel;
            this.AllValid = AllValid;
            this.teamSize = teamSize;
            this.start = start;
            this.numTaskFinished = numTaskFinished;
            this.sumOfCost = sumOfCost;
            this.makespan = makespan;
            this.actualPaths = actualPaths;
            this.plannerPaths = plannerPaths;
            this.plannerTimes = plannerTimes;
            this.errors = errors;
            this.events = events;
            this.tasks = tasks;
        }
        /// <summary>
        /// Logfile constructor
        /// </summary>
        public LogFile()
        {
            this.actionModel = String.Empty;
            this.AllValid = String.Empty;
            this.teamSize = 0;
            this.start = new List<List<object>>();
            this.numTaskFinished = 0;
            this.sumOfCost = 0;
            this.makespan = 0;
            this.actualPaths = [];
            this.plannerPaths = [];
            this.plannerTimes = [];
            this.errors = new List<List<object>>();
            this.events = new List<List<List<object>>>();
            this.tasks = new List<List<int>>();
        }

        #endregion
    }
}
