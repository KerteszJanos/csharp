using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    public enum Path
    {
        Left,
        Right,
        Forward,
    }
    public enum Direction
    {
        North,
        East,
        South,
        West,
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
        private List<Path> _actualPath;
        private List<Path> _plannerPath;

        #endregion

        #region Public properties

        public int Id
        {
            get { return _id; }
        }
        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
        }
        public Direction Direction
        {
            get { return _direction; }
        }
        public Destination CurrentDestination
        {
            get { return _currentDestination; }
        }
        public List<Path> ActualPath
        {
            get { return _actualPath; }
        }
        public List<Path> PlannerPath
        {
            get { return _plannerPath; }
        }

        #endregion

        #region Constructors

        public Robot(int x, int y)
        {
            _id = _robotCount;
            _robotCount++;
            _x = x;
            _y = y;
            _direction = Direction.North;
            _currentDestination = null!;
            _actualPath = new List<Path>();
            _plannerPath = new List<Path>();
        }

        #endregion
    }
}
