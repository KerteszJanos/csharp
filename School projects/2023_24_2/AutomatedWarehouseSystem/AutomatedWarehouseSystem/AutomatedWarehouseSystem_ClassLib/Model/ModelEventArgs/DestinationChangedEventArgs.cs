using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    public class DestinationChangedEventArgs : EventArgs
    {
        public Destination[] dests;
        public DestinationChangedEventArgs(Destination[] dests)
        {
            this.dests = dests;
        }
    }
}
