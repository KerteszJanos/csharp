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
    /// Create the project NewSimulationView from
    /// </summary>
    public partial class NewSimulationView : Form
    {
        #region Fields
        private WarehouseSystem _warehouseSystem;

        #endregion


        #region Constructor
        /// <summary>
        /// The NewSimulationView constructor
        /// </summary>
        public NewSimulationView(WarehouseSystem warehouseSystem)
        {
            this._warehouseSystem = warehouseSystem;

            InitializeComponent();
        }
        #endregion

        #region Menu event handlers

        private void cancel_Click(object sender, EventArgs e)
        {
            labelConfigFileName.Text = "Loaded config file: -";
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void runSimulationButton_Click(object sender, EventArgs e)
        {
            string userinputSimulationNumber = textBoxSimulationNumber.Text;
            string userinputSimulationPeriod = textBoxSimulationPeriod.Text;

            if (isInputValid(userinputSimulationPeriod) && (isInputValid(userinputSimulationNumber) || Int32.Parse(userinputSimulationNumber) == -1))
            {
                Close();

                _warehouseSystem.StartSimulation(userinputSimulationNumber,userinputSimulationPeriod);
            }
            else
            {
                MessageBox.Show("Invalid input. Number must bigger than 0!");
            }
        }

        #endregion

        private bool isInputValid(string str) 
        {
            if (Int32.Parse(str) > 0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private async void chooseFileButton_Click(object sender, EventArgs e)
        {
                
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // load game
                    labelConfigFileName.Text = "Loaded config file: " + _openFileDialog.SafeFileName;
                    await _warehouseSystem.LoadConfigFile(_openFileDialog.FileName);
                    _buttonRun.Enabled = true;

                }
                catch (AutomatedWarehouseSystem_ClassLib.Persistence.DataException.LoadConfigFileFailed ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _buttonRun.Enabled = false;
                }
            }
        }

    }
}
