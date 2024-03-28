using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    public class Error
    {
        #region fields/properties
        private Robot _robot1;
        private Robot _robot2;
        private int _stepNum;
        private string _reason;
        #endregion

        #region constructors
        public Error()
        {

        }
        #endregion
    }
}
