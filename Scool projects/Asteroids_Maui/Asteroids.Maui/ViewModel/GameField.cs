using Asteroids.Maui.ViewModel;
using System;

namespace Asteroids.Maui.ViewModel
{
    public class GameField : ViewModelBase
    {
        private Color backgroundColor;

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                if (backgroundColor != value)
                {
                    backgroundColor = value;
                    OnPropertyChanged(nameof(BackgroundColor));
                }
            }
        }
        public int X { get; set; }

        public int Y { get; set; }

        public GameField(int x, int y)
        {
            this.X = x;
            this.Y = y;
            backgroundColor = Colors.White;
            OnPropertyChanged(nameof(BackgroundColor));
        }
    }
}
