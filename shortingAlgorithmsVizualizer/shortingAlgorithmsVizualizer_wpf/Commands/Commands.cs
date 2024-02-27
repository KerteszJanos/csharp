﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace shortingAlgorithmsVizualizer_wpf.Commands
{
    public class Commands : ICommand
    {

        #region propertis/fields
        public event EventHandler? CanExecuteChanged;
        
        private Action<object> ExecuteAction { get; set; } //a generic delegate that can get a function which has an object parameter, return a value that is void
        private Predicate<object> CanExecutePredicate { get; set; } //a generic delegate that can decide if a function can be executed in the current state, returns a bool
        #endregion

        #region constructors
        public Commands(Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod) //gets to method
        {
            ExecuteAction = ExecuteMethod;
            CanExecutePredicate = CanExecuteMethod;
        }
        #endregion

        #region public methods
        public bool CanExecute(object? parameter)
        {
            if (parameter == null)
            {
                return false;
            }
            return CanExecutePredicate(parameter);
        }

        public void Execute(object? parameter)
        {
            if (parameter != null)
            {
                ExecuteAction(parameter);
            }
        }
        #endregion
    }
}
