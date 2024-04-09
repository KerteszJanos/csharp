using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model
{
    public class WarehouseSystem
    {
        #region fields/properties
        private IPersistence _persistence;
        private Map _map;
        private Robot[] _robots;
        private Destination[] _dests;
        private List<Mytask> _tasks;
        private IControlUnit _controlUnit;
        private int _stepNum;
        private float _timeLimit;
        private int _totalStepNum;
        private string _strategy;
        private string _actionModel;
        private bool _allValid;
        private int _numTaskFinished;
        private int _sumOfCost;
        private int _makeSpan;
        private List<float> _plannerTimes;
        private List<Error> _errors;

        private int _numTasksReveal;
        private string _taskAssignmentStrategy;
        #endregion

        #region constructors
        public WarehouseSystem()
        {
            _persistence = new TextFilePersistence();
            _map = null!;
            _robots = null!;
            _dests = null!;
            _taskAssignmentStrategy = null!;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Call when the user want to load a new map
        /// </summary>
        /// <param name="path">The path of the file including the filename</param>
        public async Task LoadMap(string path)
        {
            (_map, _robots, _dests, _numTasksReveal, _taskAssignmentStrategy) = await _persistence.LoadConfigFileAsync(path);
            OnMapChanged(new MapChangedEvenetArgs(_map));
            OnRobotPositionsChanged(new RobotPositionsChangedEvenetArgs(_robots));
            OnDestinationChanged(new DestinationChangedEventArgs(_dests));
        }
        public void StartSimulation()
        {
            throw new NotImplementedException();
        }
        public void ReplaySimulation()
        {
            throw new NotImplementedException();
        }
        public void StartStop()
        {
            throw new NotImplementedException();
        }
        public void ForwardStep()
        {
            throw new NotImplementedException();
        }
        public void BackwardStep()
        {
            throw new NotImplementedException();
        }
        public void SpeedUp()
        {
            throw new NotImplementedException();
        }
        public void SpeedDown()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region private methods

        #endregion

        #region events/event methods
        public event EventHandler<RobotPositionsChangedEvenetArgs>? RobotPositionsChanged;
        public event EventHandler<MapChangedEvenetArgs>? MapChanged;
        public event EventHandler<DestinationChangedEventArgs>? DestinationChanged;

        private void OnRobotPositionsChanged(RobotPositionsChangedEvenetArgs e)
        {
            RobotPositionsChanged!.Invoke(this, e);
        }
        private void OnMapChanged(MapChangedEvenetArgs e)
        {
            MapChanged!.Invoke(this, e);
        }
        private void OnDestinationChanged(DestinationChangedEventArgs e)
        {
            DestinationChanged!.Invoke(this, e);
        }
        #endregion
    }
}
