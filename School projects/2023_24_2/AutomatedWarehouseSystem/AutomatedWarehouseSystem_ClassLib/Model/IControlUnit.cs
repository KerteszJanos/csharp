using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    /// <summary>
    /// Model control unit interface
    /// </summary>
    public interface IControlUnit
    {
        /// <summary>
        /// Calculates the next step for all robot.
        /// Also responsible for move the robots, delete tasks, increase numTaskFinished
        /// </summary>
        /// <param name="map"></param>
        /// <param name="robots"></param>
        /// <param name="dests"></param>
        public void Step(in Map map, ref Robot[] robots, ref List<Mytask> tasks, ref int numTaskFinished, ref int stepNum);
        event EventHandler<TaskFinishedEventArgs>? TaskFinished;
    }
}
