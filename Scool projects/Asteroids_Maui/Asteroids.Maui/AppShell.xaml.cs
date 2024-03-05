using AsteroidsClassLib.Model;
using Asteroids.Maui.ViewModel;


namespace Asteroids.Maui
{
    public partial class AppShell : Shell
    {
        #region Fields

        private GameModel _model = null!;
        private AsteroidsViewModel _viewModel = null!;

        private IDispatcherTimer _asteroidGeneratorTimer = null!;
        private IDispatcherTimer _tableRefreshingTimer = null!;
        private IDispatcherTimer _advanceTime = null!;

        private bool _escaped = true;
        #endregion
        public AppShell(GameModel model, AsteroidsViewModel viewModel)
        {
            InitializeComponent();

            _model = model;
            _model.gameOver += ViewModel_GameOver;

            _viewModel = viewModel;
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _viewModel.AButtonPressed += new EventHandler(ViewModel_AButtonAPressed);
            _viewModel.DButtonPressed += new EventHandler(ViewModel_DButtonAPressed);
            _viewModel.EscButtonPressed += new EventHandler(ViewModel_EscButtonAPressed);

            //időzítő létrehozása
            _asteroidGeneratorTimer = Dispatcher.CreateTimer();
            _tableRefreshingTimer = Dispatcher.CreateTimer();
            _advanceTime = Dispatcher.CreateTimer();
            _advanceTime.Interval = TimeSpan.FromMilliseconds(1000);
            _advanceTime.Tick += _model.advanceTime;
        }

        #region ViewModel event handlers

        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            if (_model.gameIsStarted)
            {
                _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
                _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);

                _model.resetGame();
            }
            _advanceTime.Start();
            resetGame();
            _model.startNewGame();
            startGame();

        }

        private async void ViewModel_LoadGame(object sender, EventArgs e)
        {
            var options = new PickOptions
            {
                PickerTitle = "Válassza ki a játék mentését",
            };

            var result = await FilePicker.PickAsync(options);

            if (result != null)
            {
                resetGame();
                var filePath = result.FullPath;
                _model.Load(filePath);
                startGame();
            }
        }

        private void ViewModel_SaveGame(object sender, EventArgs e)
        {
            _advanceTime.Stop();
            if (_model.gameIsStarted)
            {
                _model.Save(_model._gameTable, _model._player);
            }
        }


        private void ViewModel_ExitGame(object sender, EventArgs e)
        {
            onAppQuit();
        }


        private async void ViewModel_GameOver(object sender, EventArgs e)
        {
            _advanceTime.Stop();
            _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);
            _asteroidGeneratorTimer.Stop();
            _tableRefreshingTimer.Stop();
            bool answer = await DisplayAlert("Question?", "Would you like to play a new game", "Yes", "No");
            if (answer)
            {
                _model.resetGame();
                _model.startNewGame();
                _asteroidGeneratorTimer.Start();
                _tableRefreshingTimer.Start();
            }
            else
            {
                onAppQuit();
            }
        }

        private void ViewModel_AButtonAPressed(object sender, EventArgs e)
        {
            if (!_escaped)
            {
                _model.playerMoveA();
            }
        }
        private void ViewModel_DButtonAPressed(object sender, EventArgs e)
        {
            if (!_escaped)
            {
                _model.playerMoveD();
            }

        }
        private void ViewModel_EscButtonAPressed(object sender, EventArgs e)
        {
            if (_model.gameIsStarted)
            {
                if (!_escaped) //game is running
                {
                    _escaped = true;
                    stopGame();
                    _advanceTime.Stop();
                }
                else           //game is paused
                {
                    _escaped = false;
                    startGame();
                    _advanceTime.Start();
                }
            }
        }
        #endregion

        #region private methods
        private void startGame()
        {
            _advanceTime.Start();
            _escaped = false;

            if (asteroidGenerating != null)
            {
                _asteroidGeneratorTimer.Tick += asteroidGenerating;
            }
            _asteroidGeneratorTimer.Start();

            _tableRefreshingTimer.Tick += refreshTable;
            _tableRefreshingTimer.Start();

            if (!_model.gameIsStarted)
            {
                _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
                _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);
            }
            _model.gameIsStarted = true;
        }

        private void stopGame()
        {
            _advanceTime.Stop();
            _asteroidGeneratorTimer.Tick -= asteroidGenerating;
            _tableRefreshingTimer.Tick -= refreshTable;
            _asteroidGeneratorTimer.Stop();
            _tableRefreshingTimer.Stop();
        }

        private void resetGame()
        {
            if (_model.gameIsStarted)
            {
                _model.resetGame();
            }
            _model.resetGame();
            _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);
        }
        private void asteroidGenerating(object sender, EventArgs e)
        {
            if (_asteroidGeneratorTimer.Interval > TimeSpan.FromMilliseconds(50))
            {
                _asteroidGeneratorTimer.Interval -= TimeSpan.FromMilliseconds(10);
            }
            _model.asteroidGenerating();
        }
        private void refreshTable(object sender, EventArgs e)
        {
            if (_tableRefreshingTimer.Interval > TimeSpan.FromMilliseconds(70))
            {
                _tableRefreshingTimer.Interval -= TimeSpan.FromMilliseconds(3);
            }
            _model.refreshTable();
        }
        #endregion

        public event EventHandler appQuit;

        private void onAppQuit()
        {
            appQuit?.Invoke(this, EventArgs.Empty);
        }
    }
}