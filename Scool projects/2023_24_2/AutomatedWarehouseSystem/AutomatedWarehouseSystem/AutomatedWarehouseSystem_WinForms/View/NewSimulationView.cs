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
    public partial class NewSimulationView : Form
    {
        #region Fields
        private WarehouseSystem _warehouseSystem;

        #endregion


        #region Constructor
        public NewSimulationView(WarehouseSystem warehouseSystem)
        {
            this._warehouseSystem = warehouseSystem;

            InitializeComponent();
        }
        #endregion

        #region Menu event handlers

        private void cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private async void chooseFileButton_Click(object sender, EventArgs e)
        {
                
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // load game
                    labelConfigFileName.Text = "Loaded config file: " + _openFileDialog.SafeFileName;
                    await _warehouseSystem.LoadMap(_openFileDialog.FileName);

                }
                catch (DataException)
                {
                    MessageBox.Show("Configuration file load failed!" + Environment.NewLine + "The path or file format is incorrect.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void runSimulationButton_Click(object sender, EventArgs e)
        {

        }
    }
}
