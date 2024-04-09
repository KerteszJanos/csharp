using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    public class ConfigFile
    {
        #region Public properties

        public string mapFile { get; set; }
        public string agentFile { get; set; }
        public int teamSize { get; set; }
        public string taskFile { get; set; }
        public int numTasksReveal { get; set; }
        public string taskAssignmentStrategy { get; set; }

        #endregion

        #region Constructor

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
