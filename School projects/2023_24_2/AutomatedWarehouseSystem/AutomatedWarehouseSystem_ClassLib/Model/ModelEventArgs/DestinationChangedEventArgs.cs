using AutomatedWarehouseSystem_ClassLib.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs
{
    /// <summary>
    ///  Event argument of the destination change
    /// </summary>
    public class DestinationChangedEventArgs : EventArgs
{
    private Destination[] _dests;

    /// <summary>
    ///  Destination getter/setter
    /// </summary>
    public Destination[] Dests
    {
        get { return _dests; }
        set { _dests = value; }
    }
    /// <summary>
    ///  Destination eventargs constructor
    /// </summary>
    public DestinationChangedEventArgs(Destination[] dests)
    {
        _dests = dests;
    }
}

}
