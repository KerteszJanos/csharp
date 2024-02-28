using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace sortingAlgorithmsVizualizer_wpf.Commands
{
    public class DelegateCommand : ICommand
    {

        #region properties/fields
        public event EventHandler? CanExecuteChanged;

        private Action<object> ExecuteAction { get; set; } //a generic delegate that can get a function which has an object parameter, return a value that is void
        private Predicate<object> CanExecutePredicate { get; set; } //a generic delegate that can decide if a function can be executed in the current state, returns a bool
        #endregion

        #region constructors
        public DelegateCommand(Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod) //gets to method
        {
            ExecuteAction = ExecuteMethod;
            CanExecutePredicate = CanExecuteMethod;
        }
        #endregion

        #region public methods
        public bool CanExecute(object? parameter) //function will called when a command binding appear
        {
            //if (parameter == null) //parameter is always null here unless we set CommandParameter in UI, but we fine with that
            //{
            //    return false;
            //}
            return CanExecutePredicate(parameter);
        }

        public void Execute(object? parameter)
        {
            //if (parameter != null)
            //{
            ExecuteAction(parameter);
            //}
        }
        #endregion
    }
}
