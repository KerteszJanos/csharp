using sortingAlgorithmsVisualizer_classLib.Model;
using sortingAlgorithmsVisualizer_wpf.View;
using sortingAlgorithmsVisualizer_wpf.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace sortingAlgorithmsVisualizer_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region properties / fileds
        private MainModel _model = null!;
        private MainViewModel _viewModel = null!;
        private MainWindow _view = null!;

        private System.Windows.Threading.DispatcherTimer elapsedSecondsTimer = null!;
        #endregion

        #region constructors
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }
        #endregion

        #region private methods
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell definition
            _model = new MainModel();
            _model.AlgorithmIsRunningChanged += model_AlgorithmIsRunningChanged;

            // viewmodell definition
            _viewModel = new MainViewModel(_model);
            _viewModel.ExitEvent += viewModel_Exit;

            //view definition
            _view = new MainWindow();
            _view.DataContext = _viewModel; //set the bindings source to the viewmodell
            _view.Show();

            elapsedSecondsTimer = new System.Windows.Threading.DispatcherTimer();
            elapsedSecondsTimer.Interval = new TimeSpan(0, 0, 1);
            elapsedSecondsTimer.Tick += ElapsedSecondsTimer_Tick;
        }

        private void ElapsedSecondsTimer_Tick(object? sender, EventArgs e)
        {
            _model.timeGoesBy();
        }
        #endregion

        #region EventHandlers
        //model EventHandlers
        private void model_AlgorithmIsRunningChanged(object? sender, bool e)
        {
            if (e)
            {
                elapsedSecondsTimer.Start();
            }
            else
            {
                elapsedSecondsTimer.Stop();
            }
        }

        //viewModel EventHandlers
        private void viewModel_Exit(object? sender, EventArgs e)
        {
            _view.Close();
        }

        //view EventHandlers

        #endregion
    }

}
