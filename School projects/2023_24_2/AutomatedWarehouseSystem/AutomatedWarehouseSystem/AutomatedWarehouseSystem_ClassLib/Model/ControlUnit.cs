using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    public class ControlUnit : IControlUnit
    {
        #region fields/properties
        private List<Robot> _robots;
        private Map _map;
        private int _stepNum;
        private float _timeLimit;
        private int _numTasksReveal;
        #endregion

        #region constructors
        public ControlUnit()
        {
                
        }
        #endregion

        #region public methods
        public void Step()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
