using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace sortingAlgorithmsVisualizer_wpf.ViewModel
{
    public class VisualListItem : INotifyPropertyChanged
    {
        #region properties / fields
        public int value { get; }
        public double height { get; }

        private string _color;
        public string color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged(nameof(_color));
            }
        }

        private bool _isEnabled { get; set; }
        public bool isEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(_isEnabled));
            }
        }
        #endregion

        #region constructors
        public VisualListItem(int v, double h, string c, bool ie)
        {
            value = v;
            OnPropertyChanged(nameof(value));
            height = h;
            OnPropertyChanged(nameof(height));
            _color = c;
            OnPropertyChanged(nameof(color));
            _isEnabled = ie;
            OnPropertyChanged(nameof(isEnabled));
        }
        #endregion

        #region events / event methods
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
