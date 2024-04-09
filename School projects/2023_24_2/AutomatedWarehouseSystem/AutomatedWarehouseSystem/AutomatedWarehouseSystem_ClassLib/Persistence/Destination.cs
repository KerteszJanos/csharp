using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    public class Destination
    {
        #region Private fields

        private static int _destCount = 0;

        private int _id;
        private int _x;
        private int _y;

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

        #endregion

        #region Constructor

        public Destination(int x, int y)
        {
            _id = _destCount;
            _destCount++;
            _x = x;
            _y = y;
        }

        #endregion

    }
}
