using Asteroids.WinForms.View;
using Asteroids.Model;
using Asteroids.WinForms.Persistence;
using System.Xml;
using Asteroids.WinForms.Model;

namespace Asteroids.WinForms
{
    public partial class Form1 : Form
    {
        #region fields

        private GameModel _gameModel;
        private FileManager _fileManager;

        private GridButton[,] _buttonGrid;

        private System.Windows.Forms.Timer _asteroidGeneratorTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer _tableRefreshingTimer = new System.Windows.Forms.Timer();

        private DateTime _startTime;
        private int _secondsBeforePause = 0;
        #endregion

        public Form1()
        {
            InitializeComponent();

            _buttonGrid = new GridButton[11, 11];
            _fileManager = new FileManager();
            _gameModel = new GameModel(_fileManager);
        }

        #region menuEvents
        private void startNewGame_Click(object sender, EventArgs e)
        {
            tableSetUp();
            _gameModel.startNewGame(); // 5 9-es kezdés is jó
            _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.DarkBlue; //6th column in 11th row
            starGame();
        }
        private void tableSetUp()
        {
            if (_gameModel.gameIsStarted)
            {
                resetGame();
            }
            else
            {
                menuStrip.Visible = false;
                for (int i = 0; i < 11; i++) //form size is 550*580 (11*11 50 sized button)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        _buttonGrid[i, j] = new GridButton(i, j);
                        _buttonGrid[i, j].Location = new Point(i * 48, j * 48); //location
                        _buttonGrid[i, j].Size = new Size(50, 50); //size
                        _buttonGrid[i, j].Enabled = false; //not pressable
                        _buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                        _buttonGrid[i, j].BackColor = Color.White;

                        Controls.Add(_buttonGrid[i, j]);
                        //Add to the form screen
                    }
                }
            }
        }
        private void loadGame_Click(object sender, EventArgs e)
        {
            String filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    for (int i = 0; i < _gameModel.asteroids.Count; i++)
                    {
                        _buttonGrid[_gameModel.asteroids[i].col, _gameModel.asteroids[i].row].BackColor = Color.White;
                    }
                    if (_gameModel.gameIsStarted)
                    {
                        _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.White;
                    }
                    tableSetUp();
                    _gameModel.Load(filePath);
                    _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.DarkBlue; //6th column in 11th row
                    for (int i = 0; i < _gameModel.asteroids.Count; i++)
                    {
                        _buttonGrid[_gameModel.asteroids[i].col, _gameModel.asteroids[i].row].BackColor = Color.Black;
                    }
                    starGame();
                }
            }
        }
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void saveGame_Click(object sender, EventArgs e)
        {
            String filePath;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 2;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = saveFileDialog.FileName;
                    _fileManager.Save(filePath, _gameModel._gameTable, _gameModel._player);
                }
            }
        }
        #endregion

        #region keyboardEvents
        private void esc_a_d_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (menuStrip.Visible == false)
                    {
                        _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.White;
                        _gameModel.playerMoveA();
                        _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.DarkBlue;
                        if (_gameModel._gameOver)
                        {
                            gameOver();
                        }
                    }
                    break;
                case Keys.D:
                    if (menuStrip.Visible == false)
                    {
                        _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.White;
                        _gameModel.playerMoveD();
                        _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.DarkBlue;
                        if (_gameModel._gameOver)
                        {
                            gameOver();
                        }
                    }
                    break;
                case Keys.Escape:
                    if (_gameModel.gameIsStarted)
                    {
                        if (menuStrip.Visible == false) //game is running
                        {
                            menuStrip.Visible = true;
                            stopGame();
                        }
                        else                            //game is paused
                        {
                            menuStrip.Visible = false;
                            starGame();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region privateMethods
        private void starGame()
        {
            _startTime = DateTime.Now;

            if (asteroidGenerating != null)
            {
                _asteroidGeneratorTimer.Tick += asteroidGenerating;
            }
            _asteroidGeneratorTimer.Start();

            _tableRefreshingTimer.Tick += refreshTable;
            _tableRefreshingTimer.Start();

            if (!_gameModel.gameIsStarted)
            {
                _asteroidGeneratorTimer.Interval = 
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    ;
                _tableRefreshingTimer.Interval = 500;
            }
            _gameModel.gameIsStarted = true;
        }
        private void stopGame()
        {
            _asteroidGeneratorTimer.Tick -= asteroidGenerating;
            _tableRefreshingTimer.Tick -= refreshTable;
            _asteroidGeneratorTimer.Stop();
            _tableRefreshingTimer.Stop();
            saveGame.Visible = true;
            menuTime.Visible = true;
            _secondsBeforePause += (DateTime.Now - _startTime).Seconds;
            menuTime.Text = _secondsBeforePause.ToString();
        }
        private void resetGame()
        {
            for (int i = 0; i < _gameModel.asteroids.Count; i++)
            {
                _buttonGrid[_gameModel.asteroids[i].col, _gameModel.asteroids[i].row].BackColor = Color.White;
            }
            _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.White;
            _gameModel.resetGame();
            _secondsBeforePause = 0;
            menuStrip.Visible = false;
            _asteroidGeneratorTimer.Interval = 1000;
            _tableRefreshingTimer.Interval = 500;
        }
        private void asteroidGenerating(object? sender, EventArgs e)
        {
            if (_asteroidGeneratorTimer.Interval > 50)
            {
                _asteroidGeneratorTimer.Interval -= 10;
            }
            int newAsteroid = _gameModel.asteroidGenerating();
            _buttonGrid[newAsteroid, 0].BackColor = Color.Black;
        }
        private void refreshTable(object? sender, EventArgs e)
        {
            if (_tableRefreshingTimer.Interval > 70)
            {
                _tableRefreshingTimer.Interval -= 3;
            }
            //coloring old asteroids back to white
            for (int i = 0; i < _gameModel.asteroids.Count; i++)
            {
                _buttonGrid[_gameModel.asteroids[i].col, _gameModel.asteroids[i].row].BackColor = Color.White;
            }
            _gameModel.refreshTable();
            if (_gameModel._gameOver)
            {
                gameOver();
            }
            //coloring new asteroids to black
            for (int i = 0; i < _gameModel.asteroids.Count; i++)
            {
                _buttonGrid[_gameModel.asteroids[i].col, _gameModel.asteroids[i].row].BackColor = Color.Black;
            }
        }
        private void gameOver()
        {
            for (int i = 0; i < _gameModel.asteroids.Count; i++)
            {
                _buttonGrid[_gameModel.asteroids[i].col, _gameModel.asteroids[i].row].BackColor = Color.Black;
            }
            _buttonGrid[_gameModel._player.col, _gameModel._player.row].BackColor = Color.Red;
            stopGame();
            MessageBox.Show("Game over :(");
            //delete the save file if its exists
            Close();
        }
        #endregion
    }
}