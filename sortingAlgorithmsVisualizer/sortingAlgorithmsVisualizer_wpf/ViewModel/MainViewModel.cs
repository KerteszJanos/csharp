using sortingAlgorithmsVisualizer_classLib.Model;
using sortingAlgorithmsVisualizer_wpf.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace sortingAlgorithmsVisualizer_wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties/fields
        private readonly MainModel _model;
        public string modelSortingTypeAsMenuItemHeader { get; set; }

        public ObservableCollection<VisualListItem> modelList { get; set; }

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
            modelSortingTypeAsMenuItemHeader = "Choose sorting algorythm (InsertionSort)";
            OnPropertyChanged(nameof(modelSortingTypeAsMenuItemHeader));

            modelList = new ObservableCollection<VisualListItem>();
            _model.ListInitialised += modelListInitialised;
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

        private void modelListInitialised(object? sender, List<int> e)
        {
            modelList.Clear();
            int max = e.Max();
            for (int i = 0; i < e.Count; i++)
            {
                modelList.Add(new VisualListItem(e[i], (400 * ((double)e[i] / max)) + 5, "White", false)); //405 is the hight of the ItemsControl panel

            }
        }

        private void modelListItemChanged(object? sender, ListItemChangedEventArgs e)
        {
            modelList[e.swapItemIndex1].isEnabled = true;
            modelList[e.swapItemIndex2].isEnabled = true;
            modelList[e.swapItemIndex1].color = "LightBlue";
            modelList[e.swapItemIndex2].color = "LightGreen";
            if (e.isSwapped)
            {
                Thread.Sleep(100);
                VisualListItem temp = modelList[e.swapItemIndex1];
                modelList[e.swapItemIndex1] = modelList[e.swapItemIndex2];
                modelList[e.swapItemIndex2] = temp;
            }
            Thread.Sleep(100);
            modelList[e.swapItemIndex1].color = "White";
            modelList[e.swapItemIndex2].color = "White";
            modelList[e.swapItemIndex1].isEnabled = false;
            modelList[e.swapItemIndex2].isEnabled = false; //somewhy doesnt work
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
