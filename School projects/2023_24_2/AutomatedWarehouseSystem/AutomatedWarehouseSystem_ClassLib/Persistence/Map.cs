using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    /// <summary>
    /// Warehouse map class
    /// </summary>
    public class Map
    {
        #region Private fields

        private bool[,] _table; //True: Empty cell, False: Barrier cell

        #endregion

        #region Public properties
        /// <summary>
        /// Warehouse type getter/setter
        /// </summary>
        public string Type { get; private set; }
        /// <summary>
        /// Warehouse height getter/setter
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Warehouse width getter/setter
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Validate the warehouse coordinates
        /// </summary>
        public bool this[int x, int y] //The map can be indexed by itself (Map map; -> map[1,2];)
        {
            get
            {
                if (x < 0 || x >= _table.GetLength(0))
                    throw new ArgumentException("Bad column index.", nameof(x));
                if (y < 0 || y >= _table.GetLength(1))
                    throw new ArgumentException("Bad row index.", nameof(y));

                return _table[x, y];
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Warehouse map constructor
        /// </summary>
        public Map(string type, int height, int width, bool[,] table)
        {
            Type = type;
            Height = height;
            Width = width;
            _table = table;
        }

        #endregion
    }
}
