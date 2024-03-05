using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.WinForms.View
{
    internal class GridButton : Button
    {
        private int _col;
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

        public GridButton(int col, int row)
        {
            _col = col;
            _row = row;
        }
    }
}
