using sortingAlgorithmsVizualizer_wpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace sortingAlgorithmsVizualizer_wpf.ViewModel
{
    public class MainViewModel
    {
        #region Propertis

        //Commands
        public ICommand ExitCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            ExitCommand = new DelegateCommand(Exit, CanExit);
        }
        #endregion

        #region Command methods
        private bool CanExit(object obj)
        {
            return true;
        }

        private void Exit(object obj) //the logic what we want to execute when the command is invoked
        {
            OnExitEvent();
        }
        #endregion

        #region events/event methods
        public event EventHandler? ExitEvent; //event that we can invoke
        private void OnExitEvent() //method that we can call from viewModel
        {
            ExitEvent!.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
