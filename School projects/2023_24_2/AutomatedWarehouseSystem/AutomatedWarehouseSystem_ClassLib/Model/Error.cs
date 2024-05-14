using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    /// <summary>
    /// Model errors class
    /// </summary>
    public class Error
    {
        #region fields/properties

        private int _robot1id;
        private int _robot2id;
        private int _stepNum;
        private string _reason;
        /// <summary>
        /// Robot1 id getter/setter
        /// </summary>
        public int Robot1Id
        {
            get { return _robot1id; }
            set { _robot1id = value; }
        }
        /// <summary>
        /// Robot2 id getter/setter
        /// </summary>
        public int Robot2Id
        {
            get { return _robot2id; }
            set { _robot2id = value; }
        }
        /// <summary>
        /// Step number getter/setter
        /// </summary>
        public int StepNum
        {
            get { return _stepNum; }
            set { _stepNum = value; }
        }
        /// <summary>
        /// Error reason getter/setter
        /// </summary>
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        #endregion

        #region constructors
        /// <summary>
        /// Model error constructor
        /// </summary>
        public Error(int robot1id, int robot2id, int stepNum, string reason)
        {
            _robot1id = robot1id;
            _robot2id = robot2id;
            _stepNum = stepNum;
            _reason = reason;
        }
        #endregion
    }
}
