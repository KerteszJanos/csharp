using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the simulation start process
    /// </summary>
    public class SimulationStartEventArgs : EventArgs
    {
        private string _simulationPeriod;
        private string _simulationStepnum;
        /// <summary>
        /// Simulation period in sec getter/setter
        /// </summary>
        public string SimulationPeriod
        {
            get { return _simulationPeriod; }
            set { _simulationPeriod = value; }
        }
        /// <summary>
        /// Simulation steps number getter/setter
        /// </summary>
        public string SimulationStepNum
        {
            get { return _simulationStepnum; }
            set { _simulationStepnum = value; }
        }
        /// <summary>
        /// Simulation start eventargs constructor
        /// </summary>
        public SimulationStartEventArgs(string simulationPeriod, string simulationStepnum)
        {
            _simulationPeriod = simulationPeriod;
            _simulationStepnum = simulationStepnum;
        }
    }

}
