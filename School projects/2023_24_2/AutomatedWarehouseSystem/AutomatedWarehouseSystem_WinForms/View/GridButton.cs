using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_WinForms.View
{
    /// <summary>
    /// Program gridsystem view
    /// </summary>
    internal class GridButton : Button
    {
        private int _col;
        /// <summary>
        /// Gridsystem columns getter/setter
        /// </summary>
        public int col
        {
            get
            {
                return _col;
            }
            set
            {
                _col = value;
            }
        }
        private int _row;
        /// <summary>
        /// Gridsystem rows getter/setter
        /// </summary>
        public int row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        /// <summary>
        /// Gridsystem constructor
        /// </summary>
        public GridButton(int col, int row)
        {
            _col = col;
            _row = row;
        }
    }
}
