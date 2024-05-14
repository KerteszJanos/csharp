using AutomatedWarehouseSystem_ClassLib.Model;
using AutomatedWarehouseSystem_ClassLib.Model.ModelEventArgs;
using AutomatedWarehouseSystem_ClassLib.Persistence;
using System.Diagnostics.Tracing;
using System.Text.Unicode;
using System.Windows.Forms;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace AutomatedWarehouseSystem_WinForms.View
{
    /// <summary>
    /// Create the project MainView form
    /// </summary>
    public partial class MainView : Form
    {
        #region Fields
        private GridButton[,] _buttonGrid = null!;      //View's main button grid
        private GridButton[,] _zoomButtonGrid = null!;  //Zoom's panel button grid
        private WarehouseSystem _warehouseSystem;       
        private int _warehouseWidth;
        private int _warehouseHeight;
        private int i_col = 0;              //zoomPanel clicked grid y coordinate
        private int j_row = 0;              //zoomPanel clicked grid X coordinate
        private int _simulationStepsNum;
        private int actualstep;
        private string _simulationPeriod = null!;
        private bool firstOpen = true;

        //Timers
        System.Windows.Forms.Timer timer;
        System.Windows.Forms.Timer timerLog;

        //Container of images
        List<String> imageParts = new List<String>();

        //App images
        Image warehouseBarrier;
        Image robot;
        Image package;
        Image zoombarrier;

        Label caption = new Label();
        #endregion


        #region Constructor
        /// <summary>
        /// The MainView constructor
        /// </summary>
        public MainView()
        {
            _warehouseSystem = new WarehouseSystem();
            _warehouseSystem.MapChanged += SetUpWarehouse;
            _warehouseSystem.RobotPositionsChanged += ChangeRobotsPositions;
            _warehouseSystem.DestinationChanged += ChangeDestiniton;
            _warehouseSystem.StartTheSimulation += StartSimulation;
            _warehouseSystem.TimerPerdiodChanged += ChangeTimerPeriod;
            _warehouseSystem.StartTheAnalysis += StartAnalysis;
            _warehouseSystem.SimulationEnded += SimulationEnd;
            _warehouseSystem.AnalysisEnded += AnalysisEnd;

            //Set timer for simulation
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += TimeAdvance;

            //Set timer for analysis
            timerLog = new System.Windows.Forms.Timer();
            timerLog.Interval = 1000;
            timerLog.Tick += TimeAdvanceLog;

            //Load image files
            imageParts = Directory.GetFiles("View/Images", "*.png").ToList();

            //Set images
            zoombarrier = Image.FromFile(imageParts[3]);
            warehouseBarrier = Image.FromFile(imageParts[2]);
            robot = Image.FromFile(imageParts[1]);
            package = Image.FromFile(imageParts[0]);

            InitializeComponent();
        }

        #endregion


        #region Menu event handlers
        /// <summary>
        /// Call the new simulation form window
        /// </summary>
        private void newSimulation_Click(object sender, EventArgs e)
        {
            NewSimulationView f = new NewSimulationView(_warehouseSystem);
            f.ShowDialog(); // show dialogwindow
        }

        /// <summary>
        /// Call the new analysis form window
        /// </summary>
        private void newanalysis_Click(object sender, EventArgs e)
        {
            NewAnalysisView f = new NewAnalysisView(_warehouseSystem);
            f.ShowDialog(); // show dialogwindow
        }

        /// <summary>
        /// Kill the simulation/analysis process
        /// </summary>
        private async void interruptButton_Click(object sender, EventArgs e)
        {
            if (toolStripLabel_Task.Text == "Simulation")
            {
                DialogResult result = MessageBox.Show("Are you sure to interrupt the simulation running?",
                              "Interrupt simulation",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Asterisk);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {

                    // Interrupt running process and set button/metadatas values to default
                    setAllButtonEnabled("RunningEnd");
                    gridPanel.Controls.Clear();
                    buttonEnableHandler();
                    metadataValuesHandler();
                    timer.Stop();

                    //show dialogbox
                    DialogResult result2 = MessageBox.Show("Simulation ended!" + Environment.NewLine +
                                      "Do you want to save the the simulation LogFile?",
                                      "Auotmated Warehouse System - Simulation",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Asterisk);

                    if (result2 == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //save logfile
                            await _warehouseSystem.SaveLogFile(_saveFileDialog.FileName);
                        }
                    }
                }
            }
            else if (toolStripLabel_Task.Text == "Analysis")
            {
                DialogResult result = MessageBox.Show("Are you sure to interrupt the analysis running?" + Environment.NewLine +
                              "Your analysis data will lost!",
                              "Interrupt analysis",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Asterisk);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    // Interrupt running process and set button/metadatas values to default
                    gridPanel.Controls.Clear();
                    buttonEnableHandler();
                    metadataValuesHandler();
                    timerLog.Stop();
                }
            }
            else
            {
                // Interrupt running process and set button/metadatas values to default
                gridPanel.Controls.Clear();
                buttonEnableHandler();
                metadataValuesHandler();
                _buttonGrid = null!;
                _zoomButtonGrid = null!;
            }
        }

        /// <summary>
        /// Close the main form window
        /// </summary>
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion


        #region Grid event handlers
        /// <summary>
        /// Create the warehouse map view
        /// </summary>
        private void SetUpWarehouse(object? sender, MapChangedEventArgs e)
        {
            if (gridPanel.Controls.Count != 0)
            {
                gridPanel.Controls.Clear();
            }

            _buttonGrid = new GridButton[e.Map.Height, e.Map.Width];
            _warehouseWidth = e.Map.Width;
            _warehouseHeight = e.Map.Height;
            int buttonSize = gridPanel.Width / e.Map.Width;
            buttonSize += 1;
            for (int i = 0; i < e.Map.Height; i++)
            {
                for (int j = 0; j < e.Map.Width; j++)
                {
                    _buttonGrid[i, j] = new GridButton(i, j);
                    _buttonGrid[i, j].Location = new Point(j * buttonSize, i * buttonSize);  //x = col, y = row
                    _buttonGrid[i, j].Size = new Size(buttonSize, buttonSize); //size
                    _buttonGrid[i, j].FlatStyle = FlatStyle.Flat;
                    _buttonGrid[i, j].FlatAppearance.BorderSize = 1;
                    _buttonGrid[i, j].MouseDown += buttonClickedOn;
                    _buttonGrid[i, j].MouseUp += buttonClickedOff;

                    if (e.Map[i, j] == false) //barrier cell
                    {
                        _buttonGrid[i, j].BackgroundImage = warehouseBarrier;
                        _buttonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        _buttonGrid[i, j].BackColor = Color.Black;
                    }
                    else //empty cell
                    {
                        _buttonGrid[i, j].BackColor = Color.White;
                    }

                    //Add buttons to the gridpanel
                    gridPanel.Controls.Add(_buttonGrid[i, j]);
                }
            }

            //Display warehouse name and warehouse size
            toolStripLabel_Warehouse.Text = e.Map.Type;
            toolStripLabel_Warehousesize.Text = e.Map.Width + "x" + e.Map.Height;

            //Set buttons enable
            buttonEnableHandler();

            //Safety variable
            firstOpen = false;
        }


        /// <summary>
        /// Control the robots position changes in warehoue
        /// </summary>
        private void ChangeRobotsPositions(object? sender, RobotPositionsChangedEventArgs e)
        {
            //clear all prev robot 
            foreach (var button in _buttonGrid)
            {
                if (!(button.BackgroundImage == warehouseBarrier || button.BackgroundImage == package))
                {
                    button.BackgroundImage = null;
                    button.BackgroundImageLayout = ImageLayout.Stretch;
                    button.Text = String.Empty;
                    button.TextAlign = ContentAlignment.BottomCenter;
                    button.Font = new Font("Arial", (gridPanel.Width /_warehouseHeight)/8);
                    button.BackColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = Color.Black;    
                    button.FlatAppearance.BorderSize = 1;

                }
            }
            for (int i = 0; i < e.Robots.Length; i++)
            {
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].BackgroundImage = robot;
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].BackgroundImageLayout = ImageLayout.Stretch;
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].Text = i.ToString();
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].TextAlign = ContentAlignment.MiddleCenter;
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].FlatAppearance.BorderColor = Color.Black;
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].FlatStyle = FlatStyle.Flat;
                _buttonGrid[e.Robots[i].X, e.Robots[i].Y].FlatAppearance.BorderSize = 1;
                switch (e.Robots[i].Direction)
                {
                    case Direction.East:
                        RotateImage(e.Robots[i].X, e.Robots[i].Y, 90);
                        break;
                    case Direction.South:
                        RotateImage(e.Robots[i].X, e.Robots[i].Y, 180);
                        break;
                    case Direction.West:
                        RotateImage(e.Robots[i].X, e.Robots[i].Y, 270);
                        break;
                    default:
                        break;

                }
            }
            //Display robots number
            toolStripLabel_Robotsnum.Text = e.Robots.Length.ToString();
            //CODE QUALITY: Cast the int 2-s to float
            void RotateImage(int x, int y, float angle)
            {
                Image originalImage = robot;
                Bitmap rotatedImage = new Bitmap(originalImage.Width, originalImage.Height);
                rotatedImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
                Graphics graphics = Graphics.FromImage(rotatedImage);
                graphics.TranslateTransform(originalImage.Width / (float)2, originalImage.Height / (float)2);
                graphics.RotateTransform(angle);
                graphics.TranslateTransform(-originalImage.Width / (float)2, -originalImage.Height / (float)2);
                graphics.DrawImage(originalImage, new Point(0, 0));
                graphics.Dispose();
                _buttonGrid[x, y].BackgroundImage = rotatedImage;
            }

            //Refresh the dsiplay of the simulation step text
            if (_simulationStepsNum == -1)   //infinity step
            {
                toolStripLabel_SImsteps.Text = e.ActualStep.ToString() + "/" + '\u221e';
                actualstep = e.ActualStep;
            }
            else                             //given step number
            {
                toolStripLabel_SImsteps.Text = e.ActualStep.ToString() + "/" + _simulationStepsNum.ToString();
                actualstep = e.ActualStep;
            }


            //Handle the zoompanel changes
            if (zoomPanel.Controls.Count != 0)
            {
                for (int i = 0; i <= 4; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        //copy the original buttonGrid view with validation
                        try
                        {
                            _zoomButtonGrid[i, j].BackgroundImage = _buttonGrid[i_col + i, j_row + j].BackgroundImage;
                            _zoomButtonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            _zoomButtonGrid[i, j].BackColor = _buttonGrid[i_col + i, j_row + j].BackColor;
                            _zoomButtonGrid[i, j].Text = _buttonGrid[i_col + i, j_row + j].Text;
                            _zoomButtonGrid[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            _zoomButtonGrid[i, j].ForeColor = _buttonGrid[i_col + i, j_row + j].ForeColor;
                        }
                        catch (Exception)
                        {
                            _zoomButtonGrid[i, j].BackgroundImage = zoombarrier;
                            _zoomButtonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Controls the packages position changes in warehoue
        /// </summary>
        private void ChangeDestiniton(object? sender, DestinationChangedEventArgs e)
        {
            //clear prev packages
            foreach (var button in _buttonGrid)
            {
                if (!(button.BackgroundImage == warehouseBarrier || button.BackgroundImage == robot))
                {
                    //handle the newly placed online orders display
                    if (button.Text == "-") 
                    {

                        continue;
                    }
                    else
                    {
                        button.BackgroundImage = null;
                        button.BackgroundImageLayout = ImageLayout.Stretch;
                        button.Text = String.Empty;
                        button.TextAlign = ContentAlignment.MiddleCenter;
                        button.BackColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderColor = Color.Black;
                        button.FlatAppearance.BorderSize = 1;
                    }
                }
            }
            for (int i = 0; i < e.Dests.Count(); i++)
            {
                _buttonGrid[e.Dests[i].X, e.Dests[i].Y].BackgroundImage = package;
                _buttonGrid[e.Dests[i].X, e.Dests[i].Y].BackgroundImageLayout = ImageLayout.Stretch;
                if (toolStripLabel_Task.Text == "Simulation")
                {
                    _buttonGrid[e.Dests[i].X, e.Dests[i].Y].Text = (e.Dests[i].Id).ToString();
                }
                else
                {
                    _buttonGrid[e.Dests[i].X, e.Dests[i].Y].Text = (e.Dests[i].CurrentRobotId).ToString();
                }
                _buttonGrid[e.Dests[i].X, e.Dests[i].Y].TextAlign = ContentAlignment.MiddleCenter;
                _buttonGrid[e.Dests[i].X, e.Dests[i].Y].ForeColor = Color.Black;
                _buttonGrid[e.Dests[i].X, e.Dests[i].Y].Font = new Font("Arial", (gridPanel.Width / _warehouseHeight) / 8);

            }

            //handle the zoompanel display
            if (zoomPanel.Controls.Count != 0)
            {
                for (int i = 0; i <= 4; i++)
                {
                    for (int j = 0; j <= 6; j++)
                    {
                        //copy the original buttonGrid view with validation
                        try
                        {
                            _zoomButtonGrid[i, j].BackgroundImage = _buttonGrid[i_col + i, j_row + j].BackgroundImage;
                            _zoomButtonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            _zoomButtonGrid[i, j].BackColor = _buttonGrid[i_col + i, j_row + j].BackColor;
                            _zoomButtonGrid[i, j].Text = _buttonGrid[i_col + i, j_row + j].Text;
                            _zoomButtonGrid[i, j].TextAlign = ContentAlignment.MiddleCenter;
                            _zoomButtonGrid[i, j].ForeColor = _buttonGrid[i_col + i, j_row + j].ForeColor;
                        }
                        catch (Exception)
                        {
                            _zoomButtonGrid[i, j].BackgroundImage = zoombarrier;
                            _zoomButtonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Handle the main grid size changes
        /// </summary>
        /// 
        private void MainView_SizeChanged(object sender, EventArgs e)
        {
            if (!firstOpen)
            {
                int buttonSize = gridPanel.Width / _warehouseWidth;
                for (int i = 0; i < _warehouseHeight; i++)
                {
                    for (int j = 0; j < _warehouseWidth; j++)
                    {
                        _buttonGrid[i, j].Location = new Point(j * buttonSize, i * buttonSize);  //x = col, y = row
                        _buttonGrid[i, j].Size = new Size(buttonSize, buttonSize); //size
                    }
                }
            }

           // zoomPanel.Location = new Point(gridPanel.Width/(gridPanel.Width/zoomPanel.Width), 66);
        }
        #endregion


        #region timer event handlers
        /// <summary>
        /// Simulation timer
        /// </summary>
        private void TimeAdvance(object? sender, EventArgs e)
        {
            _warehouseSystem.StepRobots();
        }
        /// <summary>
        /// Analysis timer
        /// </summary>
        private void TimeAdvanceLog(object? sender, EventArgs e)
        {
            _warehouseSystem.LoadNextStep();
        }
        #endregion


        #region Private methods
        /// <summary>
        /// Set the menu buttons Enabled
        /// </summary>
        private void buttonEnableHandler()
        {
            if (_buttonInterrupt.Enabled)
            {
                _buttonInterrupt.Enabled = false;
                _buttonNewSimulation.Enabled = true;
                _buttonNewAnalysis.Enabled = true;
            }
            else
            {
                _buttonInterrupt.Enabled = true;
                _buttonNewSimulation.Enabled = false;
                _buttonNewAnalysis.Enabled = false;
            }
        }

        /// <summary>
        /// Set the metadatas to default value
        /// </summary>
        private void metadataValuesHandler()
        {
            toolStripLabel_Warehouse.Text = "-";
            toolStripLabel_Task.Text = "-";
            toolStripLabel_Warehousesize.Text = "-";
            toolStripLabel_Robotsnum.Text = "-";
            toolStripLabel_Stepsperiod.Text = "0 sec";
            toolStripLabel_SImsteps.Text = "0/0";
        }

        /// <summary>
        /// Set all the menu buttons Enabled according the parameter
        /// </summary>
        private void setAllButtonEnabled(string param)
        {
            if (param == "RunningEnd")
            {
                _buttonBackwardRun.Enabled = false;
                _buttonForewardRun.Enabled = false;
                _buttonStartStopRun.Enabled = false;
                _buttonForewardOnestep.Visible = true;
                _buttonBackwardOneStep.Visible = true;
                textBoxSimulationStep.Visible = true;
                _buttonGoToSimulationStep.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                _buttonForewardOnestep.Enabled = false;
                _buttonBackwardOneStep.Enabled = false;
                textBoxSimulationStep.Enabled = false;
                _buttonGoToSimulationStep.Enabled = false;
            }
            else if(param == "StartSim")
            {
                _buttonBackwardRun.Enabled = true;
                _buttonForewardRun.Enabled = true;
                _buttonStartStopRun.Enabled = true;
                _buttonForewardOnestep.Visible = false;
                _buttonBackwardOneStep.Visible = false;
                textBoxSimulationStep.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                _buttonGoToSimulationStep.Visible = false;
            }
            else if(param == "StartAnal")
            {
                _buttonBackwardRun.Enabled = true;
                _buttonForewardRun.Enabled = true;
                _buttonStartStopRun.Enabled = true;
                _buttonForewardOnestep.Enabled = true;
                _buttonBackwardOneStep.Enabled = true;
                textBoxSimulationStep.Enabled = true;
                _buttonGoToSimulationStep.Enabled = true;
            }
        }

        /// <summary>
        /// Mouse click down event
        /// </summary>
        private void buttonClickedOn(object? sender, MouseEventArgs e)
        {
            GridButton? clickedButton = sender as GridButton;

            if (e.Button == MouseButtons.Right) //place new dest
            {
                if (clickedButton != null)
                {
                    int row = clickedButton.row;
                    int col = clickedButton.col;

                    //Place new package on the grid
                    if (canSetNewOrder() && toolStripLabel_Task.Text == "Simulation")
                    {
                        if (_buttonGrid[col, row].BackColor == Color.White)
                        {
                            _buttonGrid[col, row].BackgroundImage = package;
                            _buttonGrid[col, row].Text = "-";
                        }
                    }
                    _warehouseSystem.UserChooseDest((col,row));

                }
            }
            else //zoom
            {
                if (clickedButton != null)
                {
                    int row = clickedButton.row;
                    int col = clickedButton.col;

                    //set the 5x7 zoompanel
                    _zoomButtonGrid = new GridButton[5, 7];
                    int buttonSize = zoomPanel.Width / 7;

                    i_col = col - 2;
                    j_row = row - 3;

                    for (int i = 0; i <= 4; i++)
                    {
                        for (int j = 0; j <= 6; j++)
                        {
                            _zoomButtonGrid[i, j] = new GridButton(i, j);
                            _zoomButtonGrid[i, j].Location = new Point(j * buttonSize, i * buttonSize);  //x = col, y = row
                            _zoomButtonGrid[i, j].Size = new Size(buttonSize, buttonSize); //size
                            _zoomButtonGrid[i, j].FlatStyle = FlatStyle.Flat;
                            _zoomButtonGrid[i, j].FlatAppearance.BorderSize = 1;

                            //copy the original buttonGrid view with validation
                            try
                            {
                                _zoomButtonGrid[i, j].BackgroundImage = _buttonGrid[i_col + i, j_row + j].BackgroundImage;
                                _zoomButtonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                                _zoomButtonGrid[i, j].BackColor = _buttonGrid[i_col + i, j_row + j].BackColor;
                                _zoomButtonGrid[i, j].TextAlign = ContentAlignment.MiddleCenter;
                                _zoomButtonGrid[i, j].Text = _buttonGrid[i_col + i, j_row + j].Text;
                                //_zoomButtonGrid[i, j].Font = new Font("Arial", (zoomPanel.Width / _warehouseHeight) / 2);
                                _zoomButtonGrid[i, j].ForeColor = _buttonGrid[i_col + i, j_row + j].ForeColor;
                            }
                            catch (Exception)
                            {
                                _zoomButtonGrid[i, j].BackgroundImage = zoombarrier;
                                _zoomButtonGrid[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                            }

                            //Add to the zoomPanel
                            zoomPanel.Controls.Add(_zoomButtonGrid[i, j]);

                        }
                    }

                    zoomPanel.Visible = true;
                }
            }
        }


        private bool canSetNewOrder() 
        {
            foreach (GridButton button in _buttonGrid) 
            {
                if (button.Text == "-") return false;
            }
            return true;
        }

        /// <summary>
        /// Mouse click up event
        /// </summary>
        private void buttonClickedOff(object? sender, EventArgs e)
        {
            GridButton? clickedButton = sender as GridButton;

            if (clickedButton != null)
            {
                zoomPanel.Controls.Clear();
                zoomPanel.Visible = false;
            }
        }


        private void StartSimulation(object? sender, SimulationStartEventArgs e) 
        {
            timer.Enabled = true;
            timerLog.Enabled = false;

            toolStripLabel_Task.Text = "Simulation";
            _simulationStepsNum = Int32.Parse(e.SimulationStepNum);

            toolStripLabel_Stepsperiod.Text = e.SimulationPeriod + " sec";
            _simulationPeriod = toolStripLabel_Stepsperiod.Text;
            if (_simulationStepsNum == -1)
            {
                toolStripLabel_SImsteps.Text = "0/" + '\u221e';
            }
            else
            {
                toolStripLabel_SImsteps.Text = "0/" + e.SimulationStepNum;
            }

            //enable the simulation control buttons
            setAllButtonEnabled("StartSim");

            //set simulation period in sec
            timer.Interval = 1000 * Int32.Parse(e.SimulationPeriod);

            //start timer
            timer.Start();
        }

        private async void SimulationEnd(object? sender, SimulationEndedEventArgs e)
        {
            //stop timer stop
            timer.Stop();

            //enable the simulation control buttons
            setAllButtonEnabled("RunningEnd");

            //show dialogbox
            DialogResult result = MessageBox.Show("Simulation ended!" + Environment.NewLine + 
                              "Do you want to save the the simulation Log File?",
                              "Autmated Warehouse System - Simulation",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Asterisk);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //save logfile
                    await _warehouseSystem.SaveLogFile(_saveFileDialog.FileName);
                }

                gridPanel.Controls.Clear();
                buttonEnableHandler();
                metadataValuesHandler();

            }
            else
            {
                gridPanel.Controls.Clear();
                buttonEnableHandler();
                metadataValuesHandler();
            }


            //Clear the panels
            gridPanel.Controls.Clear();
            zoomPanel.Controls.Clear();

        }

        private void StartAnalysis(object? sender, SimulationStartEventArgs e)
        {
            timer.Enabled = false;
            timerLog.Enabled = true;

            toolStripLabel_Task.Text = "Analysis";
            _simulationStepsNum = Int32.Parse(e.SimulationStepNum);

            toolStripLabel_Stepsperiod.Text = e.SimulationPeriod.ToString() + " sec";
            _simulationPeriod = toolStripLabel_Stepsperiod.Text;
            toolStripLabel_SImsteps.Text = "0/" + e.SimulationStepNum;

            //enable the analysis control buttons
            setAllButtonEnabled("StartAnal");

            //set analysis period in sec
            timerLog.Interval = 1000 * Int32.Parse(e.SimulationPeriod);

            //start timer
            timerLog.Start();
        }

        private void AnalysisEnd(object? sender, AnalysisEndedEventArgs e)
        {
            //stop timer
            timerLog.Stop();

            toolStripLabel_SImsteps.Text = (actualstep+1) + "/" + _simulationStepsNum;

            //enable the analysis control buttons
            setAllButtonEnabled("RunningEnd");

            //show dialogbox
            DialogResult result = MessageBox.Show("Analysis ended!" + Environment.NewLine +
                              "Total analysis steps number: " + (actualstep+1),
                              "Automated Warehouse System - Analysis",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Asterisk);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                
                gridPanel.Controls.Clear();
                buttonEnableHandler();
                metadataValuesHandler();

                //Clear the panels
                gridPanel.Controls.Clear();
                zoomPanel.Controls.Clear();
            }
        }

        /// <summary>
        /// change timer period in sec
        /// </summary>
        /// 
        private void ChangeTimerPeriod(object? sender, TimerPeriodChangedEventArgs e)
        {

            if (e.Sec > 0)
            {
                timer.Interval = e.Sec;
                timerLog.Interval = e.Sec;
                if (e.Sec < 1000 && e.Sec > 99) //less then 1sec and grater then 0.09sec
                {
                    toolStripLabel_Stepsperiod.Text = "0." + (e.Sec / 100).ToString() + " sec";
                    _simulationPeriod = toolStripLabel_Stepsperiod.Text;
                    _buttonForewardRun.Enabled = true;
                }
                else if (e.Sec < 10) //less then 0.01sec
                {
                    toolStripLabel_Stepsperiod.Text = "0.00" + (e.Sec / 1).ToString() + " sec";
                    _simulationPeriod = toolStripLabel_Stepsperiod.Text;
                    _buttonForewardRun.Enabled = false;
                }
                else if (e.Sec < 100) //less then 0.1sec
                {
                    toolStripLabel_Stepsperiod.Text = "0.0" + (e.Sec / 10).ToString() + " sec";
                    _simulationPeriod = toolStripLabel_Stepsperiod.Text;
                    _buttonForewardRun.Enabled = true;
                }
                else
                {
                    toolStripLabel_Stepsperiod.Text = (e.Sec / 1000).ToString() + " sec";
                    _simulationPeriod = toolStripLabel_Stepsperiod.Text;
                    _buttonForewardRun.Enabled = true;
                }
            }
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            //handle the simulation buttons
            if (toolStripLabel_Task.Text == "Simulation")
            {
                if (timer.Enabled == true)
                {
                    timer.Stop();
                    toolStripLabel_Stepsperiod.Text = "Stopped";
                    _buttonBackwardRun.Enabled = false;
                    _buttonForewardRun.Enabled = false;
                    _buttonForewardOnestep.Enabled = false;
                    _buttonBackwardOneStep.Enabled = false;
                    textBoxSimulationStep.Enabled = false;
                    _buttonGoToSimulationStep.Enabled = false;
                }
                else
                {
                    timer.Start();
                    toolStripLabel_Stepsperiod.Text = _simulationPeriod;
                    _buttonBackwardRun.Enabled = true;
                    _buttonForewardRun.Enabled = true;
                    _buttonForewardOnestep.Enabled = false;
                    _buttonBackwardOneStep.Enabled = false;
                    textBoxSimulationStep.Enabled = false;
                    _buttonGoToSimulationStep.Enabled = false;
                }
            }

            //handle the analysis buttons
            if (toolStripLabel_Task.Text == "Analysis")
            {
                if (timerLog.Enabled == true)
                {
                    timerLog.Stop();
                    toolStripLabel_Stepsperiod.Text = "Stopped";
                    _buttonBackwardRun.Enabled = false;
                    _buttonForewardRun.Enabled = false;
                }
                else
                {
                    timerLog.Start();
                    toolStripLabel_Stepsperiod.Text = _simulationPeriod;
                    _buttonBackwardRun.Enabled = true;
                    _buttonForewardRun.Enabled = true;
                }
            }
        }

        private void forewardButton_Click(object sender, EventArgs e)
        {
            _warehouseSystem.TimerIntervalChange("foreward");
        }

        private void backwardButton_Click(object sender, EventArgs e)
        {
            _warehouseSystem.TimerIntervalChange("backward");
        }

        private void backwardOneStepButton_Click(object sender, EventArgs e)
        {
            if (isInputValid((actualstep-1).ToString()))
            {
                _warehouseSystem.LoadSpecificStep(actualstep-1);

            }
            else
            {
                MessageBox.Show("Invalid step period input. Number must between 1 and " + _simulationStepsNum.ToString() + "!");
            }
        }

        private void forewardOneStepButton_Click(object sender, EventArgs e)
        {
            if (isInputValid((actualstep+1).ToString()))
            {
                _warehouseSystem.LoadSpecificStep(actualstep+1);

            }
            else
            {
                MessageBox.Show("Invalid step period input. Number must between 1 and " + _simulationStepsNum.ToString() + "!");
            }
        }

        private void goToSimulationStep_Button(object sender, EventArgs e)
        {
            string goToSimulationStep = textBoxSimulationStep.Text;

            if (isInputValid(goToSimulationStep))
            {
                int stepnum = Convert.ToInt32(goToSimulationStep);
                _warehouseSystem.LoadSpecificStep(stepnum);

            }
            else
            {
                MessageBox.Show("Invalid step period input. Number must between 1 and " + _simulationStepsNum.ToString() + "!");
            }

        }

        private bool isInputValid(string str)
        {
            if (Int32.Parse(str) > 0 && Int32.Parse(str) <= _simulationStepsNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
