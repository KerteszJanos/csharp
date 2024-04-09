

using Asteroids.WPF.ViewModel;
using System;

namespace Asteroids.WPF.ViewModel
{
    public class GameField : ViewModelBase
    {
        public enum FieldStatus { Nothing, Asteroid, Player}
        private FieldStatus _fieldStatus;

        public FieldStatus fieldStatus
        {
            get { return _fieldStatus; }
            set
            {
                if (_fieldStatus != value)
                {
                    _fieldStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        //public Tuple<Int32, Int32> XY
        //{
        //    get { return new(X, Y); }
        //}

        public GameField(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
