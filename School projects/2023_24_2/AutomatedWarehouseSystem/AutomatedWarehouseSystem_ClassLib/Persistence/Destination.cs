using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Warehouse destinations class
    /// </summary>
    public class Destination
    {
        #region Private fields

        private static int _destCount = 0;

        private int _id;
        private int _x;
        private int _y;
        private int _currentRobotId;

        #endregion

        #region Public properties
        /// <summary>
        /// Destination id getter/setter
        /// </summary>
        public int Id
        {
            get { return _id; }
        }
        /// <summary>
        /// Destination X coordinate getter/setter
        /// </summary>
        public int X
        {
            get { return _x; }
        }
        /// <summary>
        /// Destination Y coordinate getter/setter
        /// </summary>
        public int Y
        {
            get { return _y; }
        }
        /// <summary>
        /// Current robot id getter/setter
        /// </summary>
        public int CurrentRobotId
        {
            get { return _currentRobotId; }
            set { _currentRobotId = value; }
        }

        #endregion

        #region Constructor

        public Destination(int x, int y)
        {
            _id = _destCount;
            _destCount++;
            _x = x;
            _y = y;
            _currentRobotId = -1;
        }
        public Destination(Destination dest)
        {
            _id = dest.Id;
            _x = dest.X;
            _y = dest.Y;
            _currentRobotId = dest.CurrentRobotId;
        }
        public Destination(int id, int x, int y)
        {
            _id = id;
            _x = x;
            _y = y;
            _currentRobotId = -1;
        }

        #endregion
        /* CODE QUALITY: The Equals and GetHashCode methods are not needed for this class
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Destination other = (Destination)obj;
            return _id == other._id;
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        */

        /// <summary>
        /// Set the destination counter to zero
        /// </summary>
        public static void RestartIdCount()
        {
            _destCount = 0;
        }
    }
}
