using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    /// <summary>
    /// Mytasks class
    /// </summary>
    public class Mytask
    {
        #region Private fields

        private int _id;
        private static int _idCtr = 0;

        private int _startDestId;
        private int _endDestId;

        private int _robotInd;
        private Robot _taskRobot;
        private Destination _taskDestination;

        private int _assignedStepNum;
        private int _finishedStepNum;

        private bool _isFinished;
        #endregion

        #region Public properties
        /// <summary>
        /// Task Id getter/setter
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// Start destination Id getter/setter
        /// </summary>
        public int StartDestId
        {
            get { return _startDestId; }
            set { _startDestId = value; }
        }
        /// <summary>
        /// End destination Id getter/setter
        /// </summary>
        public int EndDestId
        {
            get { return _endDestId; }
            set { _endDestId = value; }
        }
        /// <summary>
        /// Robot index getter/setter
        /// </summary>
        public int RobotInd
        {
            get { return _robotInd; }
            set { _robotInd = value; }
        }
        /// <summary>
        /// Robot task getter/setter
        /// </summary>
        public Robot TaskRobot
        {
            get { return _taskRobot; }
            set { _taskRobot = value; }
        }
        /// <summary>
        /// Destination task getter/setter
        /// </summary>
        public Destination TaskDestination
        {
            get { return _taskDestination; }
            set { _taskDestination = value; }
        }
        /// <summary>
        /// Assigned stepnumber task getter/setter
        /// </summary>
        public int AssignedStepNum
        {
            get { return _assignedStepNum; }
            set { _assignedStepNum = value; }
        }
        /// <summary>
        /// Finished stepnumber task getter/setter
        /// </summary>
        public int FinishedStepNum
        {
            get { return _finishedStepNum; }
            set { _finishedStepNum = value; }
        }
        /// <summary>
        /// Is finished task getter/setter
        /// </summary>
        public bool IsFinished
        {
            get { return _isFinished; }
            set { _isFinished = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for inicialize MyTasks while simulation
        /// </summary>
        /// <param name="robotInd"></param>
        /// <param name="destinationInd"></param>
        public Mytask(int robotInd, Robot taskRobot, Destination taskDestination)
        {
            _id = _idCtr;
            _idCtr++;

            _robotInd = robotInd;
            _taskRobot = taskRobot;
            _taskDestination = taskDestination;
            _isFinished = false;

            _assignedStepNum = -1;
            _finishedStepNum = -1;
        }

        /// <summary>
        /// Mytasks constructor with parameters
        /// </summary>
        public Mytask(int destId, int robotId, int assignedStepNum)
        {
            _id = _idCtr;
            _idCtr++;

            _startDestId = -1;
            _endDestId = destId;
            _assignedStepNum = assignedStepNum;
            _finishedStepNum = -1;

            _robotInd = robotId;
            _taskRobot = null!;
            _taskDestination = null!;
        }
        /// <summary>
        /// Restart the Id conter
        /// </summary>
        public static void RestartIdCount()
        {
            _idCtr = 0;
        }
        #endregion
    }
}
