using sortingAlgorithmsVizualizer_classLib.Model;
using sortingAlgorithmsVizualizer_wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace sortingAlgorithmsVizualizer_wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties/fields
        private readonly MainModel _model;
        public string modelSortingTypeAsMenuItemHeader { get; set; }

        public ObservableCollection<int> modelList { get; set; }

        //Commands
        public ICommand ExitCommand { get; set; }
        public ICommand Start_Stop_AlgorithmCommand { get; set; }
        public ICommand SetAlgorithmToCommand { get; set; }
        #endregion

        #region Constructors
        public MainViewModel(MainModel model) //dependency injection
        {
            _model = model;
            _model.SortingTypeChanged += modelSortingTypeChanged;
            modelSortingTypeAsMenuItemHeader = "Choose sorting algorythm (QuickSort)";
            OnPropertyChanged(nameof(modelSortingTypeAsMenuItemHeader));

            modelList = new ObservableCollection<int>();
            _model.ListItemChanged += modelListItemChanged;


            Start_Stop_AlgorithmCommand = new DelegateCommand(Start_Stop_Algorithm, CanStart_Stop_Algorithm);
            SetAlgorithmToCommand = new DelegateCommand(SetAlgorithmTo, CanSetAlgorithmTo);
            ExitCommand = new DelegateCommand(Exit, CanExit);
        }
        #endregion

        #region Command methods
        private void Exit(object obj) //the logic what we want to execute when the command is invoked
        {
            OnExitEvent();
        }
        private bool CanExit(object obj)
        {
            return true;
        }

        private void Start_Stop_Algorithm(object obj)
        {
            _model.StartAlgorithm(obj.ToString()!);
        }
        private bool CanStart_Stop_Algorithm(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return _model.InputListInAGoodFormat(obj.ToString()!);
        }

        private void SetAlgorithmTo(object obj)
        {
            _model.SetAlgorithmTo(obj.ToString()!);
        }
        private bool CanSetAlgorithmTo(object obj)
        {
            return true;
        }
        #endregion

        #region model event handlers
        private void modelSortingTypeChanged(object? sender, string e)
        {
            modelSortingTypeAsMenuItemHeader = $"Choose sorting algorythm ({e})";
            OnPropertyChanged(nameof(modelSortingTypeAsMenuItemHeader));
        }

        private void modelListItemChanged(object? sender, List<int> e)
        {
            throw new NotImplementedException();
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
