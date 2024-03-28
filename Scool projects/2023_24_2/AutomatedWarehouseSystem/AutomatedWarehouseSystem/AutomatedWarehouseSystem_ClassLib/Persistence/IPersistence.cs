using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedWarehouseSystem_ClassLib.Persistence
{
    public interface IPersistence
    {
        Task<(Map, Robot[], Destination[], int, string)> LoadConfigFileAsync(String path);
        //Task LoadLogFileAsync(String path); //TODO
        //Task SaveLogFileAsync(String path); //TODO
    }
}
