using AutomatedWarehouseSystem_ClassLib.Model;
using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;

namespace AutomatedWarehouseSystem_WinForms.View
{
    public partial class MainView : Form
    {
        #region Fields
        private GridButton[,] _buttonGrid = null!;
        private WarehouseSystem _warehouseSystem;
        private int _warehouseWidth;
        private int _warehouseHeight;

        //Container of images
        List<String> imageParts = new List<String>();

        //App images
        Image warehouseBarrier;
        Image robot;
        Image package;

        Label caption = new Label();
        #endregion


        #region Constructor
        public MainView()
        {
            _warehouseSystem = new WarehouseSystem();
            _warehouseSystem.MapChanged += SetUpWarehouse;
            _warehouseSystem.RobotPositionsChanged += ChangeRobotsPositions;
            _warehouseSystem.DestinationChanged += ChangeDestiniton;

            //Load image files to the list
            imageParts = Directory.GetFiles("View/Images", "*.png").ToList();

            //Set images
            warehouseBarrier = Image.FromFile(imageParts[2]);
            robot = Image.FromFile(imageParts[1]);
            package = Image.FromFile(imageParts[0]);


            InitializeComponent();


        }
        #endregion


        #region Menu event handlers
        private void newSimulation_Click(object sender, EventArgs e)
        {
            NewSimulationView f = new NewSimulationView(_warehouseSystem);
            f.ShowDialog(); // show dialogwindow
        }

        private void newanalysis_Click(object sender, EventArgs e)
        {

        }

        private void interruptButton_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure interrupt simulation running?" + Environment.NewLine +
                                "Your simulation data will lost!",
                                "Interrupt simulation",
                                MessageBoxButtons.OKCancel,
                                MessageBoxIcon.Asterisk) ;

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // Interrupt running process
                gridPanel.Controls.Clear();
                buttonEnableHandler();
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Grid event handlers
        /// <summary>
        /// Creates the warehouse map view
        /// </summary>
        private void SetUpWarehouse(object? sender, MapChangedEvenetArgs e)
        {
            if (gridPanel.Controls.Count != 0)
            {
                gridPanel.Controls.Clear();
            }

            _buttonGrid = new GridButton[e.map.Width, e.map.Height];
            _warehouseWidth = e.map.Width;
            _warehouseHeight = e.map.Height;
            int buttonSize = gridPanel.Width / e.map.Width;
            buttonSize += 2;
            for (int i = 0; i < e.map.Width; i++)
            {
                for (int j = 0; j < e.map.Height; j++)
                {

                    _buttonGrid[i, j] = new GridButton(i, j);
                    _buttonGrid[i, j].Location = new Point(j * buttonSize, i * buttonSize);  //x = col, y = row
                    _buttonGrid[i, j].Size = new Size(buttonSize, buttonSize); //size
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                    _buttonGrid[i, j].FlatAppearance.BorderSize = 0;
                    if (e.map[i, j] == false) //barrier cell
                    {
                        _buttonGrid[i, j].BackgroundImage = warehouseBarrier;
                        _buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else //empty cell
                    {
                        _buttonGrid[i, j].BackColor = Color.White;
                    }

                    //Add to the gameTable Panel
                    gridPanel.Controls.Add(_buttonGrid[i, j]);
                }
            }

            //Set metadatas
            toolStripLabel_Warehouse.Text = e.map.Type;
            toolStripLabel_Task.Text = "Simulation";
            toolStripLabel_Warehousesize.Text = e.map.Width + "x" + e.map.Height;

            //Set buttons enable
            buttonEnableHandler();
        }


        /// <summary>
        /// Controls the robots position changes in warehoue
        /// </summary>
        private void ChangeRobotsPositions(object? sender, RobotPositionsChangedEvenetArgs e)
        {
            for (int i = 0; i < e.robots.Length; i++)
            {
                _buttonGrid[e.robots[i].X, e.robots[i].Y].BackgroundImage = robot;
                _buttonGrid[e.robots[i].X, e.robots[i].Y].BackgroundImageLayout = ImageLayout.Stretch;
                _buttonGrid[e.robots[i].X, e.robots[i].Y].Text = (i + 1).ToString();
                _buttonGrid[e.robots[i].X, e.robots[i].Y].TextAlign = ContentAlignment.MiddleCenter;
                _buttonGrid[e.robots[i].X, e.robots[i].Y].FlatAppearance.BorderColor = Color.White;
                _buttonGrid[e.robots[i].X, e.robots[i].Y].ForeColor = Color.Orange;
            }

            //Set meta datas
            toolStripLabel_Robotsnum.Text = e.robots.Length.ToString();
        }


        /// <summary>
        /// Controls the packages position changes in warehoue
        /// </summary>
        private void ChangeDestiniton(object? sender, DestinationChangedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                _buttonGrid[e.dests[i].X, e.dests[i].Y].BackgroundImage = package;
                _buttonGrid[e.dests[i].X, e.dests[i].Y].BackgroundImageLayout = ImageLayout.Stretch;
                _buttonGrid[e.dests[i].X, e.dests[i].Y].Text = (i + 1).ToString();
                _buttonGrid[e.dests[i].X, e.dests[i].Y].TextAlign = ContentAlignment.MiddleCenter;
                _buttonGrid[e.dests[i].X, e.dests[i].Y].ForeColor = Color.SandyBrown;

            }
        }


        /// <summary>
        /// Sets the new grid size
        /// </summary>
        /// 
        private void MainView_SizeChanged(object sender, EventArgs e)
        {
            int buttonSize = gridPanel.Width / _warehouseWidth;
            buttonSize += 2;
            for (int i = 0; i < _warehouseWidth; i++)
            {
                for (int j = 0; j < _warehouseHeight; j++)
                {
                    _buttonGrid[i, j].Location = new Point(j * buttonSize, i * buttonSize);  //x = col, y = row
                    _buttonGrid[i, j].Size = new Size(buttonSize, buttonSize); //size
                }
            }
        }
        #endregion


        #region Private methods
        /// <summary>
        /// Set the buttons Enabled
        /// </summary>
        private void buttonEnableHandler()
        {
            if (_buttonInterrupt.Enabled)
            {
                _buttonInterrupt.Enabled = false;
                _buttonInterrupt.BackColor = Color.FloralWhite;
                _buttonNewSimulation.Enabled = true;
                _buttonNewAnalysis.Enabled = true;
            }
            else
            {
                _buttonInterrupt.Enabled = true;
                _buttonInterrupt.BackColor = Color.DarkOrange;
                _buttonNewSimulation.Enabled = false;
                _buttonNewAnalysis.Enabled = false;
            }

        }

        private void startStopButton_Click(object sender, EventArgs e)
        {

        }

        private void forewardButton_Click(object sender, EventArgs e)
        {

        }

        private void backwardButton_Click(object sender, EventArgs e)
        {

        }

        private void backwardOneStepButton_Click(object sender, EventArgs e)
        {

        }

        private void forewardOneStepButton_Click(object sender, EventArgs e)
        {

        }

        private void goToSimulationStep_Button(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
