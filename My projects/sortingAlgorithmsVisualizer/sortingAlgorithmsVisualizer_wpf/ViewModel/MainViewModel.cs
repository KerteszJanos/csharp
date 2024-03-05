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

        private double _modelSortingSpeed;
        public double modelSortingSpeed
        {
            get { return _modelSortingSpeed; }
            set
            {
                _modelSortingSpeed = value;

                modelSortingSpeedLabel = String.Format("{0:0.0}", (2 - modelSortingSpeed)) + "x";
                OnPropertyChanged(nameof(modelSortingSpeedLabel));
            }
        }
        public string modelSortingSpeedLabel { get; set; } //modelSortingSpeed setter sets it

        public ObservableCollection<VisualListItem> modelList { get; set; }

        //Commands
        public ICommand ExitCommand { get; set; }
        public ICommand StartAlgorithmCommand { get; set; }
        public ICommand SetAlgorithmToCommand { get; set; }
        public ICommand SlowDownCommand { get; set; }
        public ICommand SpeedUpCommand { get; set; }

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
            _model.SortingSpeedChanged += modelSortingSpeedChanged;
            modelSortingSpeed = 1;
            modelSortingSpeedLabel = "1.0x";


            StartAlgorithmCommand = new DelegateCommand(StartAlgorithm, CanStartAlgorithm);
            SetAlgorithmToCommand = new DelegateCommand(SetAlgorithmTo, CanSetAlgorithmTo);
            ExitCommand = new DelegateCommand(Exit, CanExit);
            SlowDownCommand = new DelegateCommand(SlowDown, CanSlowDown);
            SpeedUpCommand = new DelegateCommand(SpeedUp, CanSpeedUp);
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

        private void StartAlgorithm(object obj)
        {
            _model.StartAlgorithm(obj.ToString()!);
        }
        private bool CanStartAlgorithm(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return _model.InputListInAGoodFormat(obj.ToString()!) && !_model.algorithmIsRunning; //if the sorting is running, we cant execute
        }

        private void SetAlgorithmTo(object obj)
        {
            _model.SetAlgorithmTo(obj.ToString()!);
        }
        private bool CanSetAlgorithmTo(object obj)
        {
            return true;
        }

        private void SlowDown(object obj)
        {
            _model.SlowDown();
        }
        private bool CanSlowDown(object obj)
        {
            return true;
        }

        private void SpeedUp(object obj)
        {
            _model.SpeedUp();
        }
        private bool CanSpeedUp(object obj)
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

        private async void modelListItemChanged(object? sender, ListItemChangedEventArgs e)
        {
            VisualListItem item1 = modelList[e.swapItemIndex1];
            VisualListItem item2 = modelList[e.swapItemIndex2];

            modelList[e.swapItemIndex1].isEnabled = true;
            modelList[e.swapItemIndex2].isEnabled = true;
            modelList[e.swapItemIndex1].color = "Red";
            modelList[e.swapItemIndex2].color = "Red";
            if (e.isSwapped)
            {
                await Task.Delay((int)(400 * modelSortingSpeed));
                modelList[e.swapItemIndex1] = item2;
                modelList[e.swapItemIndex2] = item1;
            }
            await Task.Delay((int)(400 * modelSortingSpeed));
            modelList[e.swapItemIndex1].color = "White";
            modelList[e.swapItemIndex2].color = "White";
            modelList[e.swapItemIndex1].isEnabled = false;
            modelList[e.swapItemIndex2].isEnabled = false;
        }
        private void modelSortingSpeedChanged(object? sender, double sortingSpeed)
        {
            modelSortingSpeed = sortingSpeed;
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
