using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Data expceptions class
    /// </summary>
    public class DataException : Exception
    {
        /// <summary>
        /// General data exception
        /// </summary>
        public DataException(String message) : base(message) { }
        /// <summary>
        /// Failed Configuration file load exception
        /// </summary>
        public class LoadConfigFileFailed : Exception { public LoadConfigFileFailed(string exception) : base(exception) { } }
        /// <summary>
        /// Failed Log file load exception
        /// </summary>
        public class LoadLogFileFailed : Exception { public LoadLogFileFailed(string exception) : base(exception) { } }
        /// <summary>
        /// Failed warehouse map load exception
        /// </summary>
        public class LoadMapFailed : Exception { public LoadMapFailed(string exception) : base(exception) { } }
    }
}
