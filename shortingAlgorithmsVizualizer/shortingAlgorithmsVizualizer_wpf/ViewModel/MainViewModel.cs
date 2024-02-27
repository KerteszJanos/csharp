using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace shortingAlgorithmsVizualizer_wpf.ViewModel
{
    public class MainViewModel
    {
        #region Propertis

        //Commands
        public ICommand? ExitCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            ExitCommand = new Commands.Commands(Exit, CanExit);
        }
        #endregion

        #region Command methods
        private bool CanExit(object obj)
        {
            return true;
        }

        private void Exit(object obj) //the logic what we want to execute when the command is invoked
        {
            //exit the app
        }
        #endregion
    }
}
