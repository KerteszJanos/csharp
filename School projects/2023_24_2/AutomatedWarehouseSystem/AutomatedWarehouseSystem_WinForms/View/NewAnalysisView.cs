using AutomatedWarehouseSystem_ClassLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomatedWarehouseSystem_WinForms.View
{
    /// <summary>
    /// Create the project NewAnalysisView from
    /// </summary>
    public partial class NewAnalysisView : Form
    {
        #region Fields
        private WarehouseSystem _warehouseSystem;
        private bool _logfileloaded = false;
        private bool _mapfileloaded = false;

        #endregion


        #region Constructor
        /// <summary>
        /// The NewAnalysisView constructor
        /// </summary>
        public NewAnalysisView(WarehouseSystem warehouseSystem)
        {
            this._warehouseSystem = warehouseSystem;

            InitializeComponent();
        }
        #endregion


        #region Menu event handlers
        private void cancel_Click(object sender, EventArgs e)
        {
            labelConfigFileName.Text = "Loaded log file: -";
            _logfileloaded = false;
            _mapfileloaded = false;
            _buttonRun.Enabled = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void runSimulationButton_Click(object sender, EventArgs e)
        {

            if (_buttonRun.Enabled == true)
            {
                Close();
                string stepPeriod = "1"; //timerLog set to 1sec tick
                _warehouseSystem.StartAnalysis(stepPeriod);

            }
            else
            {
                MessageBox.Show("Invalid command. Log file must be loaded!");
            }
        }

        #endregion


        private async void chooseFileButton_Click(object sender, EventArgs e)
        {

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // load log file
                    labelConfigFileName.Text = "Loaded log file: " + _openFileDialog.SafeFileName;
                    await _warehouseSystem.LoadLogFile(_openFileDialog.FileName);
                    _logfileloaded = true;
                    _buttonChoseMap.Enabled = true;
                    _buttonChoseMap.BackColor = Color.DimGray;

                    if (_mapfileloaded == true && _logfileloaded == true)
                    {
                        _buttonRun.Enabled = true;
                    }

                }
                catch (AutomatedWarehouseSystem_ClassLib.Persistence.DataException.LoadLogFileFailed ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _logfileloaded = false;
                    _buttonChoseMap.Enabled = false;
                    _buttonChoseMap.BackColor = Color.Silver;
                }
            }
        }

        private async void chooseMapButton_Click(object sender, EventArgs e)
        {
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // load map file
                    labelMapFileName.Text = "Loaded map file: " + _openFileDialog.SafeFileName;
                    await _warehouseSystem.LoadMap(_openFileDialog.FileName);
                    _mapfileloaded = true;

                    if (_mapfileloaded == true && _logfileloaded == true)
                    {
                        _buttonRun.Enabled = true;
                    }
                }
                catch (AutomatedWarehouseSystem_ClassLib.Persistence.DataException.LoadMapFailed ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
