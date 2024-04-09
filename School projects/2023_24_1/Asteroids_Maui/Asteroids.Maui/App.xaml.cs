using AsteroidsClassLib.Model;
using Asteroids.Maui.ViewModel;
using AsteroidsClassLib.Persistence;

namespace Asteroids.Maui
{
    public partial class App : Application
    {
        private AppShell _appShell = null!;

        private GameModel _model = null!;
        private AsteroidsViewModel _viewModel = null!;


        public App()
        {
            InitializeComponent();

            _model = new GameModel(new FileManager());
            _viewModel = new AsteroidsViewModel(_model);

            _appShell = new AppShell(_model, _viewModel)
            {
                BindingContext = _viewModel
            };
            _appShell.appQuit += App_appQuit;
            MainPage = _appShell;
        }
        private void App_appQuit(object sender, EventArgs e)
        {
            Quit();
        }
    }
}
