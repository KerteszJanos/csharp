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

        private VisualListItem prevPivot = null!;
        public string modelComparisons { get; set; }
        public string modelArrayAcces { get; set; }
        public string arrayInputTextboxContent { get; set; }

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
            _model.PivotChanged += modelPivotChanged;
            _model.ComparisonCounterChanged += modelComparisonCounterChanged;
            _model.ArrayAccesCounterChanged += modelArrayAccesCounterChanged;
            modelComparisons = "Comparisons: 0";
            OnPropertyChanged(nameof(modelComparisons));
            modelArrayAcces = "Array acces: 0";
            OnPropertyChanged(nameof(modelArrayAcces));
            arrayInputTextboxContent = "";
            OnPropertyChanged(nameof(arrayInputTextboxContent));

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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < e.Count; i++)
            {
                if (i == e.Count - 1)
                {
                    sb.Append(e[i].ToString());
                }
                else
                {
                    sb.Append(e[i].ToString());
                    sb.Append(',');
                }
            }
            arrayInputTextboxContent = sb.ToString();
            OnPropertyChanged(nameof(arrayInputTextboxContent));

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

            if (!item1.isPivot)
            {
                item1.isEnabled = true;
                item1.color = "Red";
            }
            if (!item2.isPivot)
            {
                item2.isEnabled = true;
                item2.color = "Red";
            }
            if (e.isSwapped)
            {
                await Task.Delay((int)(400 * modelSortingSpeed));
                modelList[e.swapItemIndex1] = item2;
                modelList[e.swapItemIndex2] = item1;
            }
            await Task.Delay((int)(400 * modelSortingSpeed));
            if (!item1.isPivot)
            {
                item1.color = "White";
                item1.isEnabled = false;
            }
            if (!item2.isPivot)
            {
                item2.color = "White";
                item2.isEnabled = false;
            }
        }
        private void modelSortingSpeedChanged(object? sender, double sortingSpeed)
        {
            modelSortingSpeed = sortingSpeed;
        }

        private void modelPivotChanged(object? sender, int e)
        {
            if (prevPivot != null)
            {
                //sets back prev pivot to a normal element
                prevPivot.isPivot = false;
                prevPivot.color = "White";
                prevPivot.isEnabled = false;
            }
            if (e != -1)
            {
                modelList[e].isEnabled = true;
                modelList[e].color = "Green";
                modelList[e].isPivot = true;
                prevPivot = modelList[e];
            }
        }


        private void modelComparisonCounterChanged(object? sender, string e)
        {
            modelComparisons = "Comparisons " + e;
            OnPropertyChanged(nameof(modelComparisons));
        }
        private void modelArrayAccesCounterChanged(object? sender, string e)
        {
            modelArrayAcces = "Array acces: " + e;
            OnPropertyChanged(nameof(modelArrayAcces));
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
