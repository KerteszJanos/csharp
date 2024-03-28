using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    public class Mytask
    {
        #region fields/properties
        private int _id;
        private Destination _startDest;
        private Destination _endDest;
        private int assignedStepNum;
        private int finishedStepNum;
        #endregion

        #region constructors
        public Mytask()
        {
                
        }
        #endregion
    }
}
