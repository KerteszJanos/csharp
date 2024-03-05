using Asteroids.WPF.ViewModel;
using AsteroidsConsole.Model;
using AsteroidsConsole.Persistence;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Asteroids.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private GameModel _model = null!;
        private AsteroidsViewModel _viewModel = null!;
        private MainWindow _view = null!;

        private DateTime _startTime;
        public int secondsBeforePause { get; set; }
        private DispatcherTimer _asteroidGeneratorTimer = null!;
        private DispatcherTimer _tableRefreshingTimer = null!;

        private bool _escaped = false;
        #endregion

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        #region Application event handlers

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            // modell létrehozása
            _model = new GameModel(new FileManager());
            _model.gameOver += ViewModel_GameOver;

            // nézemodell létrehozása
            _viewModel = new AsteroidsViewModel(_model);
            _viewModel.NewGame += new EventHandler(ViewModel_NewGame);
            _viewModel.ExitGame += new EventHandler(ViewModel_ExitGame);
            _viewModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _viewModel.AButtonPressed += new EventHandler(ViewModel_AButtonAPressed);
            _viewModel.DButtonPressed += new EventHandler(ViewModel_DButtonAPressed);
            _viewModel.EscButtonPressed += new EventHandler(ViewModel_EscButtonAPressed);

            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing); // eseménykezelés a bezáráshoz
            _view.Show();

            //időzítő létrehozása
            _asteroidGeneratorTimer = new DispatcherTimer();
            _tableRefreshingTimer = new DispatcherTimer();
        }
        #endregion

        #region View event handlers
        private void View_Closing(object? sender, CancelEventArgs e)
        {
            stopGame();
            //if (MessageBox.Show("Biztos, hogy ki akar lépni?", "Asteroids", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            //{
            //    e.Cancel = true;
            //    startGame();
            //}
        }
        #endregion

        #region ViewModel event handlers

        private void ViewModel_NewGame(object? sender, EventArgs e)
        {
            if (_model.gameIsStarted)
            {
                _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
                _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);

                _model.resetGame();
            }
            resetGame();
            _model.startNewGame();
            startGame();

        }

        private void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            String filePath;
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                resetGame();
                filePath = openFileDialog.FileName;
                _model.Load(filePath);
                startGame();
            }
        }

        private void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            if (_model.gameIsStarted)
            {
                String filePath;
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == true)
                {
                    //Get the path of specified file
                    filePath = saveFileDialog.FileName;
                    _model.FileManager.Save(filePath, _model._gameTable, _model._player);
                }
            }
        }

        private void ViewModel_ExitGame(object? sender, EventArgs e)
        {
            _view.Close();
        }

        private void ViewModel_GameOver(object? sender, EventArgs e)
        {
            _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);
            _asteroidGeneratorTimer.Stop();
            _tableRefreshingTimer.Stop();
            if (MessageBox.Show("Game over :( Szeretnél új játékot kezdeni?", "Asteroids", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
            {
                _model.resetGame();
                _model.startNewGame();
                _asteroidGeneratorTimer.Start();
                _tableRefreshingTimer.Start();
            }
            else
            {
                _view.Close();
            }

        }

        private void ViewModel_AButtonAPressed(object? sender, EventArgs e)
        {
            if (!_escaped)
            {
                _model.playerMoveA();
            }
        }
        private void ViewModel_DButtonAPressed(object? sender, EventArgs e)
        {

            //if (menuStrip.Visibility == Visibility.Hidden)
            //{

            if (!_escaped)
            {
                _model.playerMoveD();
            }

        }
        private void ViewModel_EscButtonAPressed(object? sender, EventArgs e)
        {
            if (_model.gameIsStarted)
            {
                if (!_escaped) //game is running
                {
                    _escaped = true;
                    stopGame();
                }
                else           //game is paused
                {
                    _escaped = false;
                    startGame();
                }
            }
        }
        #endregion

        #region private methods
        private void startGame()
        {
            _viewModel.menuVisibilityChanged(false);
            _escaped = false;
            _startTime = DateTime.Now;

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
            _asteroidGeneratorTimer.Tick -= asteroidGenerating;
            _tableRefreshingTimer.Tick -= refreshTable;
            _asteroidGeneratorTimer.Stop();
            _tableRefreshingTimer.Stop();
            _viewModel.menuVisibilityChanged(true);
            secondsBeforePause += (DateTime.Now - _startTime).Seconds;
            _viewModel.timeLabelChanged(secondsBeforePause);
        }

        private void resetGame()
        {
            if (_model.gameIsStarted)
            {
                _model.resetGame();
            }
            _model.resetGame();
            secondsBeforePause = 0;
            _viewModel.timeLabelChanged(secondsBeforePause);
            _asteroidGeneratorTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tableRefreshingTimer.Interval = TimeSpan.FromMilliseconds(500);
        }
        private void asteroidGenerating(object? sender, EventArgs e)
        {
            if (_asteroidGeneratorTimer.Interval > TimeSpan.FromMilliseconds(50))
            {
                _asteroidGeneratorTimer.Interval -= TimeSpan.FromMilliseconds(10);
            }
            _model.asteroidGenerating();
        }
        private void refreshTable(object? sender, EventArgs e)
        {
            if (_tableRefreshingTimer.Interval > TimeSpan.FromMilliseconds(70))
            {
                _tableRefreshingTimer.Interval -= TimeSpan.FromMilliseconds(3);
            }
            _model.refreshTable();
        }
        #endregion
    }
}
