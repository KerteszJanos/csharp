using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmsVisualizer_wpf.ViewModel
{
    public class VisualListItem : INotifyPropertyChanged
    {
        #region properties / fields
        public int value { get; set; }
        public double height { get; set; }
        //maybe color or context
        #endregion

        #region constructors
        public VisualListItem(int v, double h)
        {
            value = v;
            OnPropertyChanged(nameof(v));
            height = h;
            OnPropertyChanged(nameof(h));
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
