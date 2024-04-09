using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    public class DataException : Exception
    {
        public DataException(String message) : base(message) { }
    }
}
