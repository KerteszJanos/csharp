using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// The configuration file class
    /// </summary>
    public class ConfigFile
    {
        #region Public properties
        /// <summary>
        /// Warehouse mapfile getter/setter
        /// </summary>
        public string mapFile { get; set; }
        /// <summary>
        /// Robots file getter/setter
        /// </summary>
        public string agentFile { get; set; }
        /// <summary>
        /// Robots number getter/setter
        /// </summary>
        public int teamSize { get; set; }
        /// <summary>
        /// Tasks file getter/setter
        /// </summary>
        public string taskFile { get; set; }
        /// <summary>
        /// Revealed tasks number getter/setter
        /// </summary>
        public int numTasksReveal { get; set; }
        /// <summary>
        /// Tasks assignment strategy getter/setter
        /// </summary>
        public string taskAssignmentStrategy { get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Confuguration file constructor
        /// </summary>
        public ConfigFile()
        {
            mapFile = String.Empty;
            agentFile = String.Empty;
            teamSize = 0;
            taskFile = String.Empty;
            numTasksReveal = 0;
            taskAssignmentStrategy = String.Empty;
        }

        #endregion
    }
}
