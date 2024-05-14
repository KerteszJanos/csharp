using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the changed time period
    /// </summary>
    public class TimerPeriodChangedEventArgs : EventArgs
    {
        private int _sec;
        /// <summary>
        ///  Time period in sec getter/setter
        /// </summary>
        public int Sec
        {
            get { return _sec; }
            set { _sec = value; }
        }
        /// <summary>
        ///  Time period cahanged eventargs constructor
        /// </summary>
        public TimerPeriodChangedEventArgs(int sec)
        {
            _sec = sec;
        }
    }

}
