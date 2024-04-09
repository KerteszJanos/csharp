using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    public class RobotPositionsChangedEvenetArgs : EventArgs
    {
        public Robot[] robots;
        public RobotPositionsChangedEvenetArgs(Robot[] robots)
        {
            this.robots = robots;
        }
    }
}
