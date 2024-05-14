using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the finished task
    /// </summary>
    public class TaskFinishedEventArgs : EventArgs
    {
        private int _robotId;
        private int _destId;
        /// <summary>
        /// Robot Id getter/setter
        /// </summary>
        public int RobotId
        {
            get { return _robotId; }
            set { _robotId = value; }
        }
        /// <summary>
        /// Destination Id getter/setter
        /// </summary>
        public int DestId
        {
            get { return _destId; }
            set { _destId = value; }
        }
        /// <summary>
        /// Finished task eventargs constructor
        /// </summary>
        public TaskFinishedEventArgs(int robotId, int destId)
        {
            _robotId = robotId;
            _destId = destId;
        }
    }

}
