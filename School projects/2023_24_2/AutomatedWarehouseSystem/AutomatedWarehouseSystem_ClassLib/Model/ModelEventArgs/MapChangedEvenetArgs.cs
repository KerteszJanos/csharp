using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the map change
    /// </summary>
    public class MapChangedEventArgs : EventArgs
    {
        private Map _map;
        /// <summary>
        /// Warehouse map getter/setter
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }
        /// <summary>
        ///  Map changed eventargs constructor
        /// </summary>
        public MapChangedEventArgs(Map map)
        {
            _map = map;
        }
    }

}
