using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmsVizualizer_wpf.ViewModel
{
    class VisualListItem : INotifyPropertyChanged
    {
        #region properties / fields
        public int value { get; set; }
        //maybe index?
        //maybe hight (it can scale depends with the maximum elements)
        #endregion

        #region constructors
        public VisualListItem()
        {
            //some logic       
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
