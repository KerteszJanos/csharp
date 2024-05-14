using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the robot position changes
    /// </summary>
    public class RobotPositionsChangedEventArgs : EventArgs
    {
        private Robot[] _robots;
        private int _actualstep;
        /// <summary>
        /// Robot getter/setter
        /// </summary>
        public Robot[] Robots
        {
            get { return _robots; }
            set { _robots = value; }
        }
        /// <summary>
        /// Actual robot step getter/setter
        /// </summary>
        public int ActualStep
        {
            get { return _actualstep; }
            set { _actualstep = value; }
        }
        /// <summary>
        /// Robot postion change eventargs constructor
        /// </summary>
        public RobotPositionsChangedEventArgs(Robot[] robots, int actualstep)
        {
            _robots = robots;
            _actualstep = actualstep;
        }
    }

}
