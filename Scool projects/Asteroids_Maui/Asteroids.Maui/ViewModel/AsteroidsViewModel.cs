using System;
using System.Collections.ObjectModel;
using AsteroidsClassLib.Model;
using static Asteroids.Maui.ViewModel.GameField;

namespace Asteroids.Maui.ViewModel
{
    /// <summary>
    /// Sudoku nézetmodell típusa.
    /// </summary>
    public class AsteroidsViewModel : ViewModelBase
    {
        #region Fields

        private GameModel _model; // modell
        public bool isVisible { get; set; }
        public int secondsBeforePause { get; set; }
        #endregion

        #region Properties

        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand LoadGameCommand { get; private set; }

        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand ExitCommand { get; private set; }

        public DelegateCommand AButtonPressedCommand { get; private set; }
        public DelegateCommand DButtonPressedCommand { get; private set; }
        public DelegateCommand EscButtonPressedCommand { get; private set; }

        /// <summary>
        /// Játékmező gyűjtemény lekérdezése.
        /// </summary>
        public ObservableCollection<GameField> Fields { get; set; }
        #endregion

        public AsteroidsViewModel(GameModel model)
        {
            _model = model;
            _model.FieldChanged += Model_FieldChanged;
            _model.calculateTime += Model_calculateTime;

            NewGameCommand = new DelegateCommand(param => OnNewGame());
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExitGame());
            AButtonPressedCommand = new DelegateCommand(param => OnAButtonPressed());
            DButtonPressedCommand = new DelegateCommand(param => OnDButtonPressed());
            EscButtonPressedCommand = new DelegateCommand(param => OnEscButtonPressed());

            isVisible = true;
            OnPropertyChanged(nameof(isVisible));
            //secondsBeforePause = 0;

            Fields = new ObservableCollection<GameField>();
            for (Int32 i = 0; i < 11; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < 11; j++)
                {
                    Fields.Add(new GameField(i, j));
                }
            }
        }

        #region Legal methods
        public void Model_calculateTime(object sender, int e)
        {
            secondsBeforePause = e;
            OnPropertyChanged(nameof(secondsBeforePause));
        }

        #endregion

        #region Game event handlers
        private void Model_FieldChanged(object sender, FieldChangedEventArgs e)
        {
            switch (e.fieldStatus)
            {
                case FieldChangedEventArgs.FieldStatus.Nothing:
                    Fields[e.X * 10 + e.X + e.Y].BackgroundColor = Colors.White;
                    break;
                case FieldChangedEventArgs.FieldStatus.Asteroid:
                    Fields[e.X * 10 + e.X + e.Y].BackgroundColor = Colors.Black;
                    break;
                case FieldChangedEventArgs.FieldStatus.Player:
                    Fields[e.X * 10 + e.X + e.Y].BackgroundColor = Colors.DarkBlue;
                    break;
                default:
                    break;
            }
        }


        #endregion


        #region Events

        public event EventHandler NewGame;

        public event EventHandler LoadGame;

        public event EventHandler SaveGame;

        public event EventHandler ExitGame;

        public event EventHandler AButtonPressed;
        public event EventHandler DButtonPressed;
        public event EventHandler EscButtonPressed;

        #endregion

        #region Event methods
        private void OnNewGame()
        {
            isVisible = false;
            OnPropertyChanged(nameof(isVisible));
            NewGame.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadGame()
        {
            isVisible = false;
            OnPropertyChanged(nameof(isVisible));
            LoadGame.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveGame()
        {
            SaveGame.Invoke(this, EventArgs.Empty);
        }

        private void OnExitGame()
        {
            isVisible = false;
            OnPropertyChanged(nameof(isVisible));
            ExitGame.Invoke(this, EventArgs.Empty);
        }

        private void OnAButtonPressed()
        {
            AButtonPressed.Invoke(this, new EventArgs());
        }
        private void OnDButtonPressed()
        {
            DButtonPressed.Invoke(this, new EventArgs());
        }
        private void OnEscButtonPressed()
        {
            EscButtonPressed.Invoke(this, new EventArgs());
            if (isVisible)
            {
                isVisible = false;
            }
            else
            {
                isVisible = true;
            }
            OnPropertyChanged(nameof(isVisible));
        }

        #endregion
    }
}
