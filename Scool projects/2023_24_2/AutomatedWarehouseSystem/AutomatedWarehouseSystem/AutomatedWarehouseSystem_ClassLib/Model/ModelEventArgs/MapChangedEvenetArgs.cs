using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedWarehouseSystem_ClassLib.Persistence;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    public class MapChangedEvenetArgs : EventArgs
    {
        public Map map;
        public MapChangedEvenetArgs(Map map)
        {
            this.map = map;
        }
    }
}
