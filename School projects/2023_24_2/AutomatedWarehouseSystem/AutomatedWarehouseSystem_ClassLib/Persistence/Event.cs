using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Warehouse destination events class
    /// </summary>
    public class Event
    {
        #region Private fields

        private int _taskId;
        private int _stepNum;
        private string _status;

        #endregion

        #region Public properties
        /// <summary>
        /// Task id getter/setter
        /// </summary>
        public int TaskId
        {
            get { return _taskId; }
        }
        /// <summary>
        /// Step number getter/setter
        /// </summary>
        public int StepNum
        {
            get { return _stepNum; }
        }
        /// <summary>
        /// Status of the event getter/setter
        /// </summary>
        public string Status
        {
            get { return _status; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Warehouse destination event constructor
        /// </summary>
        public Event(int taskId, int stepNum, string status)
        {
            _taskId = taskId;
            _stepNum = stepNum;
            _status = status;
        }

        #endregion

    }
}