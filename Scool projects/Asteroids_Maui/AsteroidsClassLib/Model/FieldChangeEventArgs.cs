using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidsClassLib.Model
{
    public class FieldChangedEventArgs : EventArgs
    {
        public enum FieldStatus { Nothing, Asteroid, Player }
        private FieldStatus _fieldStatus;

        public FieldStatus fieldStatus
        {
            get { return _fieldStatus; }
            set { _fieldStatus = value; }
        }

        public int X { get; set; }

        public int Y { get; set; }

        //public Tuple<Int32, Int32> XY
        //{
        //    get { return new(X, Y); }
        //}

        public FieldChangedEventArgs(int x, int y, FieldStatus fs)
        {
            this.X = x;
            this.Y = y;
            _fieldStatus = fs;
        }
    }
}
