using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Robot progress status
    /// </summary>
    public enum PathEnum
    {
        Left,       //C (counter-clockwise)
        Right,      //R (clockwise)
        Forward,    //F 
        Wait,       //W
        Timeout,    //T
    }
    /// <summary>
    /// Robot progress direction
    /// </summary>
    public enum Direction
    {
        North,  //N
        East,   //E
        South,  //S
        West,   //W
    }
    public class Robot
    {
        #region Private fields

        private static int _robotCount = 0;

        private int _id;
        private int _x;
        private int _y;
        private Direction _direction;
        private Destination _currentDestination;
        private List<PathEnum> _actualPath;
        private List<PathEnum> _plannerPath;

        #endregion

        #region Public properties
        /// <summary>
        /// Robot id getter/setter
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// Robot X coordinate getter/setter
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// Robot Y coordinate getter/setter
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// Robot progress direction getter/setter
        /// </summary>
        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        /// <summary>
        /// Robot current destination coordinates getter/setter
        /// </summary>
        public Destination CurrentDestination
        {
            get { return _currentDestination; }
            set { _currentDestination = value; }
        }
        /// <summary>
        /// Robot actual path getter/setter
        /// </summary>
        public List<PathEnum> ActualPath
        {
            get { return _actualPath; }
            set { _actualPath = value; }
        }
        /// <summary>
        /// Robot planner path getter/setter
        /// </summary>
        public List<PathEnum> PlannerPath
        {
            get { return _plannerPath; }
            set { _plannerPath = value; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Robot constructor with only X and Y coordinate parameters
        /// </summary>
        public Robot(int x, int y)
        {
            _id = _robotCount;
            _robotCount++;
            _x = x;
            _y = y;
            _direction = Direction.North;
            _currentDestination = null!;
            _actualPath = new List<PathEnum>();
            _plannerPath = new List<PathEnum>();
        }
        /// <summary>
        /// Robot constructor with complete metadata parameters
        /// </summary>
        public Robot(int x, int y, char dir, List<char> actpath, List<char> planpath)
        {
            _id = _robotCount;
            _robotCount++;
            _x = x;
            _y = y;
            switch (dir)
            {
                case 'N':
                    _direction = Direction.North; 
                    break;
                case 'E':
                    _direction = Direction.East;
                    break;
                case 'W':
                    _direction = Direction.West;
                    break;
                case 'S':
                    _direction = Direction.South;
                    break;
                default:
                    _direction = Direction.North;
                    break;
            }
            _currentDestination = null!;
            _actualPath = new List<PathEnum>();
            _plannerPath = new List<PathEnum>();
            foreach (char c in actpath)
            {
                PathEnum path = new PathEnum();
                switch (c)
                {
                    case 'C':
                        path = PathEnum.Left;
                        break;
                    case 'R':
                        path = PathEnum.Right;
                        break;
                    case 'F':
                        path = PathEnum.Forward;
                        break;
                    case 'W':
                        path = PathEnum.Wait;
                        break;
                    case 'T':
                        path = PathEnum.Timeout;
                        break;
                    default:
                        path = PathEnum.Wait;
                        break;
                }
                _actualPath.Add(path);
            }
            foreach (char c in planpath)
            {
                PathEnum path = new PathEnum();
                switch (c)
                {
                    case 'C':
                        path = PathEnum.Left;
                        break;
                    case 'R':
                        path = PathEnum.Right;
                        break;
                    case 'F':
                        path = PathEnum.Forward;
                        break;
                    case 'W':
                        path = PathEnum.Wait;
                        break;
                    case 'T':
                        path = PathEnum.Timeout;
                        break;
                    default:
                        path = PathEnum.Wait;
                        break;
                }
                _plannerPath.Add(path);
            }
        }
        /// <summary>
        /// Robot constructor with Robot parameter
        /// </summary>
        public Robot(Robot robot)
        {
            this._id = robot.Id;
            this._x = robot.X;
            this._y = robot.Y;
            this._direction = robot.Direction;
            this._currentDestination = null!;
            this._actualPath = null!;
            this._plannerPath = null!;
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Change the robots progress direction
        /// </summary>
        public void StepPath(PathEnum p)
        {
            switch (p)
            {
                case PathEnum.Left:
                    TurnLeft();
                    break;
                case PathEnum.Right:
                    TurnRight();
                    break;
                case PathEnum.Forward:
                    GoForward();
                    break;
                case PathEnum.Wait:
                    break;
                case PathEnum.Timeout:
                    break;
            }
        }
        /// <summary>
        /// Change the robots progress status to forward step
        /// </summary>
        public void LogForwardStep()
        {
            _actualPath.Add(PathEnum.Forward);
            _plannerPath.Add(PathEnum.Forward);
        }
        /// <summary>
        /// Change the robots progress status to left turn
        /// </summary>
        public void LogLeftTurn()
        {
            _actualPath.Add(PathEnum.Left);
            _plannerPath.Add(PathEnum.Left);
        }
        /// <summary>
        /// Change the robots progress status to right turn
        /// </summary>
        public void LogRightTurn()
        {
            _actualPath.Add(PathEnum.Right);
            _plannerPath.Add(PathEnum.Right);
        }
        /// <summary>
        /// Change the robots progress status to wait
        /// </summary>
        public void LogWait()
        {
            _actualPath.Add(PathEnum.Wait);
            _plannerPath.Add(PathEnum.Wait);
        }
        /// <summary>
        /// Change the robots progress status to timeout
        /// </summary>
        public void LogTimeout()
        {
            _actualPath.Add(PathEnum.Wait);
            _plannerPath.Add(PathEnum.Timeout);
        }
        /// <summary>
        /// Generate timeout error
        /// </summary>
        public void ErrorTimeout()
        {
            OnGenerateError(new GenerateErrorEventArgs(-1, -1, "timeout"));
        }
        /// <summary>
        /// Generate wait error
        /// </summary>
        public void ErrorWaitForOthers()
        {
            OnGenerateError(new GenerateErrorEventArgs(_id, -1, "wait for others"));
        }
        /// <summary>
        /// Restart the id counting of the robots
        /// </summary>
        public static void RestartIdCount()
        {
            _robotCount = 0;
        }
        #endregion

        #region Private methods

        private void TurnLeft()
        {
            switch (_direction)
            {
                case Direction.North:
                    _direction = Direction.West;
                    break;
                case Direction.West:
                    _direction = Direction.South;
                    break;
                case Direction.South:
                    _direction = Direction.East;
                    break;
                case Direction.East:
                    _direction = Direction.North;
                    break;
            }
        }
        private void TurnRight()
        {
            switch (_direction)
            {
                case Direction.North:
                    _direction = Direction.East;
                    break;
                case Direction.East:
                    _direction = Direction.South;
                    break;
                case Direction.South:
                    _direction = Direction.West;
                    break;
                case Direction.West:
                    _direction = Direction.North;
                    break;
            }
        }
        private void GoForward()
        {
            switch (_direction)
            {
                case Direction.North:
                    _x -= 1;
                    break;
                case Direction.East:
                    _y += 1;
                    break;
                case Direction.South:
                    _x += 1;
                    break;
                case Direction.West:
                    _y -= 1;
                    break;
            }
        }

        #endregion

        #region Events/Event handlers
        /// <summary>
        /// Generate erorrs event handler
        /// </summary>

        public event EventHandler<GenerateErrorEventArgs>? GenerateError;
        private void OnGenerateError(GenerateErrorEventArgs e)
        {
            GenerateError!.Invoke(this, e);
        }
        #endregion
    }
}
