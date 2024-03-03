using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sortingAlgorithmsVisualizer_classLib.Model
{
    public class ListItemChangedEventArgs : EventArgs
    {
        #region properties / fields
        public int swapItemIndex1 { get; }
        public int swapItemIndex2 { get; }
        public bool isSwapped { get; }
        #endregion

        #region constructors
        public ListItemChangedEventArgs(int index1, int index2, bool isS)
        {
            swapItemIndex1 = index1;
            swapItemIndex2 = index2;
            isSwapped = isS;
        }
        #endregion
    }
}
