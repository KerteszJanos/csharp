using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    /// <summary>
    /// Warehouse system class
    /// </summary>
    public class WarehouseSystem
    {
        #region fields
        private IPersistence _persistence;
        private Map _map;
        private Robot[] _robots;
        private Destination[] _dests;
        private List<Mytask> _tasks;
        private IControlUnit _controlUnit;
        private bool controlUnitFinishedCalculating;
        private int _stepNum;
        private int _timeLimit;
        private int _totalStepNum;
        private string _actionModel;
        private int _teamSize;
        private bool _allValid;
        private int _numTaskFinished;
        private int _sumOfCost;
        private int _makeSpan;
        private List<float> _plannerTimes;
        private List<Error> _errors;
        private List<List<Event>> _events;
        private List<Event>[] _eventsArray;

        private int _numTasksReveal;
        private string _taskAssignmentStrategy;
        private Robot[] _robotStartPositions;
        private Destination[] _destStart;


        private List<Robot[]> _robotPositions;
        private List<Destination[]> _destPositions;
        private Stopwatch _stopwatch;

        private (int, int) _userDest;
        #endregion
        #region getters/setters
        //mainly for unitTesting
        /// <summary>
        /// Events array getter/setter
        /// </summary>
        public List<Event>[] EventsArray
        {
            get { return _eventsArray; }
            set { _eventsArray = value; }
        }
        /// <summary>
        /// Persistence getter/setter
        /// </summary>
        public IPersistence Persistence => _persistence;
        /// <summary>
        /// Warehouse map getter/setter
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }
        /// <summary>
        /// Warehouse robots getter/setter
        /// </summary>
        public Robot[] Robots
        {
            get { return _robots; }
            set { _robots = value; }
        }
        /// <summary>
        /// Warehouse destination getter/setter
        /// </summary>
        public Destination[] Dests
        {
            get { return _dests; }
            set { _dests = value; }
        }
        /// <summary>
        /// Warehouse tasks getter/setter
        /// </summary>
        public List<Mytask> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }
        /// <summary>
        /// ControlUnit getter/setter
        /// </summary>
        public IControlUnit ControlUnit => _controlUnit;
        /// <summary>
        /// Finished control unit calculation getter/setter
        /// </summary>
        public bool ControlUnitFinishedCalculating => controlUnitFinishedCalculating;
        /// <summary>
        /// Current steps number getter/setter
        /// </summary>
        public int StepNum
        {
            get { return _stepNum; }
            set { _stepNum = value; }
        }
        /// <summary>
        /// Time limit getter/setter
        /// </summary>
        public int TimeLimit
        {
            get { return _timeLimit; }
            set { _timeLimit = value; }
        }
        /// <summary>
        /// Total steps number getter/setter
        /// </summary>
        public int TotalStepNum
        {
            get { return _totalStepNum; }
            set { _totalStepNum = value; }
        }
        /// <summary>
        /// Action modell getter/setter
        /// </summary>
        public string ActionModel => _actionModel;
        /// <summary>
        /// Robot size getter/setter
        /// </summary>
        public int TeamSize => _teamSize;
        /// <summary>
        /// Validation getter/setter
        /// </summary>
        public bool AllValid => _allValid;
        /// <summary>
        /// Finished tasks number getter/setter
        /// </summary>
        public int NumTaskFinished => _numTaskFinished;
        /// <summary>
        /// Summary cost getter/setter
        /// </summary>
        public int SumOfCost => _sumOfCost;
        /// <summary>
        /// Make span getter/setter
        /// </summary>
        public int MakeSpan => _makeSpan;
        /// <summary>
        /// Time planner getter/setter
        /// </summary>
        public List<float> PlannerTimes => _plannerTimes;
        /// <summary>
        /// Errors getter/setter
        /// </summary>
        public List<Error> Errors => _errors;
        /// <summary>
        /// Warehouse events getter/setter
        /// </summary>
        public List<List<Event>> Events
        {
            get { return _events; }
            set { _events = value; }
        }
        /// <summary>
        /// Revealed tasks number getter/setter
        /// </summary>
        public int NumTasksReveal
        {
            get { return _numTasksReveal; }
            set { _numTasksReveal = value; }
        }
        /// <summary>
        /// Task assignment strategy getter/setter
        /// </summary>
        public string TaskAssignmentStrategy
        {
            get { return _taskAssignmentStrategy; }
            set { _taskAssignmentStrategy = value; }
        }
        /// <summary>
        /// Robot starting positions getter/setter
        /// </summary>
        public Robot[] RobotStartPositions => _robotStartPositions;
        /// <summary>
        /// Destination start getter/setter
        /// </summary>
        public Destination[] DestStart => _destStart;
        /// <summary>
        /// Robot positions getter/setter
        /// </summary>
        public List<Robot[]> RobotPositions
        {
            get { return _robotPositions; }
            set { _robotPositions = value; }
        }
        /// <summary>
        /// Destination positions getter/setter
        /// </summary>
        public List<Destination[]> DestPositions
        {
            get { return _destPositions; }
            set { _destPositions = value; }
        }
        /// <summary>
        /// Stopwatch getter/setter
        /// </summary>
        public Stopwatch Stopwatch => _stopwatch;
        /// <summary>
        /// User destination getter/setter
        /// </summary>
        public (int, int) UserDest => _userDest;
        #endregion

        #region constructors
        /// <summary>
        /// Warehouse system constructor
        /// </summary>
        public WarehouseSystem()
        {
            _persistence = new TextFilePersistence();
            //we will get these values from persistence when user choosed a file
            _map = null!;
            _robots = null!;
            _dests = null!;

            //we will set these while process
            _numTaskFinished = 0;
            _tasks = new List<Mytask>();
            _stepNum = 0;

            //you can change the ControlUnit:
            _controlUnit = new AStarNoDeadLockCU.AStarNoDeadLockControlUnit();
            //_controlUnit = new AStarFinalCU.AStarFinalControlUnit();
            //_controlUnit = new StepAwayAStarCU.StepAwayAStarControlUnit();
            //_controlUnit = new AStarCU.AStarControlUnit();
            //_controlUnit = new RandomizedCU.RandomizedControlUnit();
            controlUnitFinishedCalculating = true;

            _userDest = (-1, -1);

            //Configfile's data
            _taskAssignmentStrategy = "roundrobin";

            //Logfile's data
            _actionModel = "MAPF_T";
            _allValid = true;
            _teamSize = 0;
            _robotStartPositions = null!;
            _plannerTimes = null!;
            _errors = null!;
            _events = null!;
            _robotPositions = null!;
            _destPositions = null!;
            _stopwatch = new Stopwatch();
            _eventsArray = null!;
            _controlUnit.TaskFinished += ControlUnitTaskFinished;

            _destStart = null!;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Call it when the user choose a destination
        /// </summary>
        /// <param name="userChoice"></param>
        public void UserChooseDest((int, int) userChoice)
        {
            if (Map[userChoice.Item1, userChoice.Item2] && _userDest == (-1, -1))
            {
                _userDest = userChoice;
            }
        }

        /// <summary>
        /// Call when the user wants to load a new map
        /// </summary>
        /// <param name="path">The path of the file including the filename</param>
        public async Task LoadConfigFile(string path)
        {
            InitSimulationData();
            (_map, _robots, _dests, _numTasksReveal, _taskAssignmentStrategy) = await _persistence.LoadConfigFileAsync(path);
            _numTasksReveal += _robots.Length;
            //Logging
            InitLogData();
            ConnectRobotsWithEventHandler();

            //Changing the view
            OnMapChanged(new MapChangedEventArgs(_map));
            OnRobotPositionsChanged(new RobotPositionsChangedEventArgs(_robots, _stepNum));

            RoundRobin(); //filling the _tasks list up
            OnDestinationChanged(new DestinationChangedEventArgs(GetDestsFromTasks(_tasks).Concat(_dests.Take(_numTasksReveal - _tasks.Count)).ToArray())); //giving _numTaskAmount of task for view
        }

        /// <summary>
        /// Call when the user want to load a logfile
        /// </summary>
        /// <param name="path">The path of the file including the filename</param>
        public async Task LoadLogFile(string path)
        {
            InitSimulationData();
            (_actionModel, _allValid, _teamSize, _robots, _numTaskFinished, _sumOfCost,
                _makeSpan, float[] planTimes, _errors, _events, List<Destination> destList) = await _persistence.LoadLogFileAsync(path);
            _plannerTimes = planTimes.ToList<float>();
            _dests = destList.ToArray();
        }

        /// <summary>
        /// Call when the user want to load a mapfile
        /// </summary>
        /// <param name="path">The path of the file including the filename</param>
        public async Task LoadMap(string path)
        {
            _map = await _persistence.LoadMapAsync(path);
            OnMapChanged(new MapChangedEventArgs(_map));
        }

        /// <summary>
        /// Call when the simulation ends
        /// </summary>
        /// <param name="path">The path of the logfile to save</param>
        public async Task SaveLogFile(string path)
        {
            _makeSpan = _stepNum;
            _sumOfCost = _makeSpan * _teamSize;
            _events = _eventsArray.ToList<List<Event>>();

            await _persistence.SaveLogFileAsync(path, _actionModel, _allValid, _teamSize, _robots, _numTaskFinished, _sumOfCost,
                    _makeSpan, _plannerTimes.ToArray(), _errors, _events, _destStart.ToList<Destination>(), _robotStartPositions);

        }

        /// <summary>
        /// Call when u want to calculate the next step for the robots.
        /// </summary>
        public void StepRobots() //we dont hadle that situatuin if the calulation time is longer than 1 sec
        {
            if (controlUnitFinishedCalculating)
            {
                controlUnitFinishedCalculating = false;
                if (_totalStepNum == _stepNum) //this will be always false if _totalStepNum is -1
                {
                    OnSimulationEnded(new SimulationEndedEventArgs());
                    controlUnitFinishedCalculating = true;
                }
                else //make a step
                {
                    _stopwatch.Start();
                    _controlUnit.Step(in _map, ref _robots, ref _tasks, ref _numTaskFinished, ref _stepNum);
                    _stopwatch.Stop();
                    float elapsedSeconds = (float)_stopwatch.Elapsed.TotalSeconds;
                    _plannerTimes.Add(elapsedSeconds);

                    if (_tasks.Count < _numTasksReveal)
                    {
                        RoundRobin();
                        if (_tasks.Count == 0)
                        {
                            OnSimulationEnded(new SimulationEndedEventArgs());
                        }
                    }
                    OnDestinationChanged(new DestinationChangedEventArgs(GetDestsFromTasks(_tasks).Concat(_dests.Take(_numTasksReveal - _tasks.Count)).ToArray())); //0 bc we delete the not wanted elements from _dests
                    OnRobotPositionsChanged(new RobotPositionsChangedEventArgs(_robots, _stepNum));
                    if (elapsedSeconds > 1) //timeout
                    {
                        foreach (Robot robot in _robots)
                        {
                            robot.LogTimeout();
                        }
                        _robots[0].ErrorTimeout();
                    }
                    controlUnitFinishedCalculating = true;
                }
            }
        }

        /// <summary>
        /// Call when simulation is start
        /// </summary>
        public void StartSimulation(string simNumber, string simPeriod)
        {
            _stepNum = 0;
            //set the total simulation step number and simulation steps period in sec
            _totalStepNum = Int32.Parse(simNumber);
            _timeLimit = Int32.Parse(simPeriod) * 1000;

            OnStartSimulation(new SimulationStartEventArgs(simPeriod, simNumber));
        }

        /// <summary>
        /// Starts the analysis when the logfile is loaded
        /// </summary>
        public void StartAnalysis(string simPeriod)
        {
            InitRobotPositionsList();
            OnRobotPositionsChanged(new RobotPositionsChangedEventArgs(_robotPositions[0], _stepNum));
            InitDestinationPositionList();
            _stepNum = 0;
            _totalStepNum = _makeSpan;
            _timeLimit = Int32.Parse(simPeriod) * 1000;
            OnStartAnalysis(new SimulationStartEventArgs(simPeriod, _makeSpan.ToString()));
        }

        /// <summary>
        /// Loads the next step from the Robot positions
        /// </summary>
        public void LoadNextStep()
        {
            if (_totalStepNum == _stepNum)
            {
                OnAnalysisEnded(new AnalysisEndedEventArgs()); //reset the logic in the OnSimulationEnded event
            }
            else
            {
                OnDestinationChanged(new DestinationChangedEventArgs(_destPositions[_stepNum]));
                OnRobotPositionsChanged(new RobotPositionsChangedEventArgs(_robotPositions[_stepNum + 1], _stepNum));
                _stepNum++;
            }
        }

        /// <summary>
        /// Loads a specific step from the Robot positions
        /// </summary>
        public void LoadSpecificStep(int stepNum)
        {
            _stepNum = stepNum;
            OnDestinationChanged(new DestinationChangedEventArgs(_destPositions[_stepNum]));
            OnRobotPositionsChanged(new RobotPositionsChangedEventArgs(_robotPositions[_stepNum + 1], _stepNum));
        }

        /// <summary>
        /// Call when timer period is changed
        /// </summary>
        public void TimerIntervalChange(string value)
        {
            if (value == "foreward")
            {
                _timeLimit = _timeLimit / 2;
                OnTimerPeriodChange(new TimerPeriodChangedEventArgs(_timeLimit));
            }
            else
            {
                _timeLimit = _timeLimit * 2;
                OnTimerPeriodChange(new TimerPeriodChangedEventArgs(_timeLimit));
            }
        }
        #endregion

        #region private methods
        private List<Destination> GetDestsFromTasks(List<Mytask> tasks) //1 lengthFromStart is the index itself
        {
            List<Destination> dests = new List<Destination>();
            for (int i = 0; i < tasks.Count; i++)
            {
                dests.Add(tasks[i].TaskDestination);
            }
            return dests;
        }

        /// <summary>
        /// Gives the free robots free tasks, and deletes the handled out dest from dests
        /// </summary>
        public void RoundRobin() //public only bc of the unitTests, should be private
        {
            int i = 0;
            if (_userDest != (-1, -1))
            {
                while (i < _robots.Length && _tasks.Count < _numTasksReveal && _userDest != (-1, -1))
                {
                    if (_robots[i].CurrentDestination == null && !(_robots[i].X == UserDest.Item1 && _robots[i].Y == UserDest.Item2))
                    {
                        Destination userDest = new Destination(_userDest.Item1, _userDest.Item2);
                        _robots[i].CurrentDestination = userDest;
                        _tasks.Add(new Mytask(i, _robots[i], userDest));
                        AddEvent(i, userDest.Id, _stepNum, "assigned");
                        _userDest = (-1, -1);
                    }
                    i++;
                }
            }
            i = 0;
            while (i < _robots.Length && _tasks.Count < _numTasksReveal && 0 < _dests.Length)
            {
                if (_robots[i].CurrentDestination == null && !(_robots[i].X == _dests[0].X && _robots[i].Y == _dests[0].Y))
                {
                    _robots[i].CurrentDestination = _dests[0]; //always 0 bc we delete the handled out dest from dests
                    _tasks.Add(new Mytask(i, _robots[i], _dests[0]));
                    AddEvent(i, _dests[0].Id, _stepNum, "assigned");
                    DeleteDest(ref _dests, 0);
                }
                i++;
            }
        }

        /// <summary>
        /// Delete dest from dests[] by a specified index
        /// </summary>
        /// <param name="dests"></param>
        /// <param name="delInd"></param>
        private void DeleteDest(ref Destination[] dests, in int delInd)
        {
            if (dests == null || delInd < 0 || delInd >= dests.Length)
            {
                // If the array is null or the index is out of bounds, exit the function.
                return;
            }

            Destination[] result = new Destination[dests.Length - 1];

            // Copy the elements before and after the specified index to the result array.
            Array.Copy(dests, 0, result, 0, delInd);
            Array.Copy(dests, delInd + 1, result, delInd, dests.Length - delInd - 1);

            dests = result;
        }
        /// <summary>
        /// Creates the positions of the robots for every step in the logfile
        /// </summary>
        private void InitRobotPositionsList()
        {
            _robotPositions = new List<Robot[]>();

            Robot[] robotpos = new Robot[_robots.Length];
            for (int k = 0; k < _robots.Length; k++)
            {
                robotpos[k] = new Robot(_robots[k]);
            }
            _robotPositions.Add(robotpos);

            for (int i = 0; i < _makeSpan; i++)
            {
                for (int j = 0; j < _teamSize; j++)
                {
                    _robots[j].StepPath(_robots[j].ActualPath[i]);
                }
                robotpos = new Robot[_robots.Length];
                for (int k = 0; k < _robots.Length; k++)
                {
                    robotpos[k] = new Robot(_robots[k]);
                }
                _robotPositions.Add(robotpos);
            }

        }
        /// <summary>
        /// Creates the positions of the destinations for every step in the logfile
        /// </summary>
        private void InitDestinationPositionList()
        {
            List<Destination>[] destPositions = new List<Destination>[_makeSpan];
            for (int i = 0; i < _makeSpan; i++)
            {
                destPositions[i] = new List<Destination>();
            }
            _tasks = new List<Mytask>();
            Mytask.RestartIdCount();

            for (int i = 0; i < _events.Count; i++)
            {
                for (int j = 0; j < _events[i].Count; j++)
                {
                    if (_events[i][j].Status == "assigned")
                    {
                        Mytask task = new Mytask(_events[i][j].TaskId, i + 1, _events[i][j].StepNum);
                        _tasks.Add(task);
                    }
                    else
                    {
                        Mytask task = SearchTaskByDestId(_events[i][j].TaskId);
                        task.FinishedStepNum = _events[i][j].StepNum;
                    }
                }
            }
            foreach (Mytask task in _tasks)
            {
                Destination dest = SearchDestById(task.EndDestId);
                dest.CurrentRobotId = task.RobotInd;
                if (task.FinishedStepNum != -1)
                {
                    for (int i = task.AssignedStepNum; i < task.FinishedStepNum; i++)
                    {
                        destPositions[i].Add(dest);
                    }
                }
                else
                {
                    for (int i = task.AssignedStepNum; i < _makeSpan; i++)
                    {
                        destPositions[i].Add(dest);
                    }
                }
            }
            _destPositions = new List<Destination[]>();
            for (int i = 0; i < _makeSpan; i++)
            {
                _destPositions.Add(destPositions[i].ToArray());
            }
        }
        /// <summary>
        /// Returns the Destination from the _dests array which has the given ID
        /// </summary>
        private Destination SearchDestById(int id)
        {
            foreach (Destination dest in _dests)
            {
                if (dest.Id == id)
                    return dest;
            }
            return _dests[0];
        }
        /// <summary>
        /// Returns the Mytask from the _tasks array which has the given ID
        /// </summary>
        private Mytask SearchTaskByDestId(int destid)
        {
            foreach (Mytask task in _tasks)
            {
                if (task.EndDestId == destid)
                    return task;
            }
            return _tasks[0];
        }
        private void SaveRobotStartPositions()
        {
            _robotStartPositions = new Robot[_robots.Length];
            for (int k = 0; k < _robots.Length; k++)
            {
                _robotStartPositions[k] = new Robot(_robots[k]);
            }
        }
        private void SaveDestStartPositions()
        {
            _destStart = new Destination[_dests.Length];
            for (int k = 0; k < _dests.Length; k++)
            {
                _destStart[k] = new Destination(_dests[k]);
            }
        }
        public void InitLogData() //public only bc of the unitTests
        {
            _actionModel = "MAPF_T";
            _allValid = true;
            _teamSize = _robots.Length;
            SaveRobotStartPositions();
            SaveDestStartPositions();
            _numTaskFinished = 0;
            _sumOfCost = 0;
            _makeSpan = 0;
            _plannerTimes = new List<float>();
            _errors = new List<Error>();
            _eventsArray = new List<Event>[_teamSize];
            for (int i = 0; i < _teamSize; i++)
            {
                _eventsArray[i] = new List<Event>();
            }
        }
        /// <summary>
        /// Initializes the simulation's data
        /// </summary>
        private void InitSimulationData()
        {
            _map = null!;
            _robots = null!;
            _dests = null!;
            _numTaskFinished = 0;
            _tasks.Clear();
            controlUnitFinishedCalculating = true;
            _stepNum = 0;
            _userDest = (-1, -1);
            _taskAssignmentStrategy = "roundrobin";
            Mytask.RestartIdCount();
            Robot.RestartIdCount();
            Destination.RestartIdCount();
        }
        /// <summary>
        /// Add en event when a robot gets a task or finishes a task
        /// </summary>
        private void AddEvent(int robotId, int destId, int stepNum, string status)
        {
            _eventsArray[robotId].Add(new Event(destId, stepNum, status));
        }
        private void ConnectRobotsWithEventHandler()
        {
            foreach (var robot in _robots)
            {
                robot.GenerateError += RobotGeneratedError;
            }
        }
        private void RobotGeneratedError(object? sender, GenerateErrorEventArgs e)
        {
            _errors.Add(new Error(e.Robot1Id, e.Robot2Id, _stepNum, e.Reason));
        }
        #endregion

        #region events/event methods
        public event EventHandler<RobotPositionsChangedEventArgs>? RobotPositionsChanged;
        public event EventHandler<MapChangedEventArgs>? MapChanged;
        public event EventHandler<DestinationChangedEventArgs>? DestinationChanged;
        public event EventHandler<SimulationStartEventArgs>? StartTheSimulation;
        public event EventHandler<TimerPeriodChangedEventArgs>? TimerPerdiodChanged;
        public event EventHandler<SimulationEndedEventArgs>? SimulationEnded;
        public event EventHandler<AnalysisEndedEventArgs>? AnalysisEnded;
        public event EventHandler<SimulationStartEventArgs>? StartTheAnalysis;

        private void OnRobotPositionsChanged(RobotPositionsChangedEventArgs e)
        {
            RobotPositionsChanged!.Invoke(this, e);
        }
        private void OnMapChanged(MapChangedEventArgs e)
        {
            MapChanged!.Invoke(this, e);
        }
        private void OnDestinationChanged(DestinationChangedEventArgs e)
        {
            DestinationChanged!.Invoke(this, e);
        }
        private void OnStartSimulation(SimulationStartEventArgs e)
        {
            StartTheSimulation!.Invoke(this, e);
        }
        private void OnTimerPeriodChange(TimerPeriodChangedEventArgs e)
        {
            TimerPerdiodChanged!.Invoke(this, e);
        }
        private void OnSimulationEnded(SimulationEndedEventArgs e)
        {
            //we need to reconstruct the values (set the values like in the ctor), to set up model for a new simulation
            SimulationEnded!.Invoke(this, e);
        }
        private void OnAnalysisEnded(AnalysisEndedEventArgs e)
        {
            //we need to reconstruct the values (set the values like in the ctor), to set up model for a new simulation
            AnalysisEnded!.Invoke(this, e);
        }
        private void OnStartAnalysis(SimulationStartEventArgs e)
        {
            StartTheAnalysis?.Invoke(this, e);
        }
        private void ControlUnitTaskFinished(object? sender, TaskFinishedEventArgs e)
        {
            AddEvent(e.RobotId, e.DestId, _stepNum, "finished");
        }
        #endregion
    }
}
