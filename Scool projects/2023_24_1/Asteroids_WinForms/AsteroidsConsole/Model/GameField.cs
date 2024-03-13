using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.WinForms.Model
{
    public class GameField
    {
        private bool _isAsteroid = false;
        public bool isAsteroid
        {
            get { return _isAsteroid; }
            set { _isAsteroid = value; }
        }

        private int _col;
        public int col
        {
            get { return _col; }
            set { _col = value; }
        }

        private int _row;
        public int row
        {
            get { return _row; }
            set { _row = value; }
        }

        public GameField(int col, int row)
        {
            _col = col;
            _row = row;
        }
    }// isAsteroid, col, row
}
