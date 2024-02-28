using sortingAlgorithmsVizualizer_wpf.View;
using sortingAlgorithmsVizualizer_wpf.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace sortingAlgorithmsVizualizer_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private MainModel _model = null!;
        private MainViewModel _viewModel = null!;
        private MainWindow _view = null!;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell definition

            // viewmodell definition
            _viewModel = new MainViewModel(/*_main*/);
            _viewModel.ExitEvent += viewModel_Exit;

            //view definition
            _view = new MainWindow();
            _view.DataContext = _viewModel; //set the bindings source to the viewmodell
            _view.Show();
        }

        #region EventHandlers
        //model EventHandlers

        //viewModel EventHandlers
        private void viewModel_Exit(object? sender, EventArgs e)
        {
            _view.Close();
        }

        //view EventHandlers
        #endregion
    }

}
