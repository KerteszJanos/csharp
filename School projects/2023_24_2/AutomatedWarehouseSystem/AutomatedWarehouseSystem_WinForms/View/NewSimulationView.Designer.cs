using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics.Tracing;

namespace AutomatedWarehouseSystem_WinForms.View
{
    partial class NewSimulationView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewSimulationView));
            _buttonCancel = new Button();
            _buttonRun = new Button();
            _groupboxLoadConfigFile = new GroupBox();
            labelConfigFileName = new Label();
            _buttonChoseFile = new Button();
            labelSimSteps = new Label();
            labelSimStepsperiod = new Label();
            label1 = new Label();
            label2 = new Label();
            _openFileDialog = new OpenFileDialog();
            textBoxSimulationNumber = new TextBox();
            textBoxSimulationPeriod = new TextBox();
            label3 = new Label();
            _groupBox2SettingSimulation = new GroupBox();
            _groupboxLoadConfigFile.SuspendLayout();
            _groupBox2SettingSimulation.SuspendLayout();
            SuspendLayout();
            // 
            // _buttonCancel
            // 
            _buttonCancel.BackColor = Color.Gray;
            _buttonCancel.ForeColor = Color.White;
            _buttonCancel.Location = new Point(22, 498);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new Size(129, 43);
            _buttonCancel.TabIndex = 0;
            _buttonCancel.Text = "Cancel";
            _buttonCancel.UseVisualStyleBackColor = false;
            _buttonCancel.Click += cancel_Click;
            // 
            // _buttonRun
            // 
            _buttonRun.BackColor = Color.DimGray;
            _buttonRun.Enabled = false;
            _buttonRun.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            _buttonRun.ForeColor = Color.White;
            _buttonRun.Location = new Point(315, 498);
            _buttonRun.Name = "_buttonRun";
            _buttonRun.Size = new Size(138, 43);
            _buttonRun.TabIndex = 1;
            _buttonRun.Text = "Run simulation";
            _buttonRun.UseVisualStyleBackColor = false;
            _buttonRun.Click += runSimulationButton_Click;
            // 
            // _groupboxLoadConfigFile
            // 
            _groupboxLoadConfigFile.BackColor = SystemColors.ScrollBar;
            _groupboxLoadConfigFile.Controls.Add(labelConfigFileName);
            _groupboxLoadConfigFile.Controls.Add(_buttonChoseFile);
            _groupboxLoadConfigFile.Location = new Point(12, 12);
            _groupboxLoadConfigFile.Name = "_groupboxLoadConfigFile";
            _groupboxLoadConfigFile.Size = new Size(458, 171);
            _groupboxLoadConfigFile.TabIndex = 2;
            _groupboxLoadConfigFile.TabStop = false;
            _groupboxLoadConfigFile.Text = "Load config file";
            // 
            // labelConfigFileName
            // 
            labelConfigFileName.AutoSize = true;
            labelConfigFileName.Font = new System.Drawing.Font("Segoe UI", 7.8F, FontStyle.Italic);
            labelConfigFileName.Location = new Point(149, 112);
            labelConfigFileName.Name = "labelConfigFileName";
            labelConfigFileName.Size = new Size(126, 17);
            labelConfigFileName.TabIndex = 7;
            labelConfigFileName.Text = "Loaded config file:   -";
            // 
            // _buttonChoseFile
            // 
            _buttonChoseFile.BackColor = Color.DimGray;
            _buttonChoseFile.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            _buttonChoseFile.ForeColor = Color.White;
            _buttonChoseFile.Location = new Point(118, 47);
            _buttonChoseFile.Name = "_buttonChoseFile";
            _buttonChoseFile.Size = new Size(199, 50);
            _buttonChoseFile.TabIndex = 0;
            _buttonChoseFile.Text = "Choose file to upload";
            _buttonChoseFile.UseVisualStyleBackColor = false;
            _buttonChoseFile.Click += chooseFileButton_Click;
            // 
            // labelSimSteps
            // 
            labelSimSteps.AutoSize = true;
            labelSimSteps.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            labelSimSteps.Location = new Point(6, 51);
            labelSimSteps.Name = "labelSimSteps";
            labelSimSteps.Size = new Size(217, 23);
            labelSimSteps.TabIndex = 3;
            labelSimSteps.Text = "Simualtion steps number:";
            // 
            // labelSimStepsperiod
            // 
            labelSimStepsperiod.AutoSize = true;
            labelSimStepsperiod.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            labelSimStepsperiod.Location = new Point(10, 143);
            labelSimStepsperiod.Name = "labelSimStepsperiod";
            labelSimStepsperiod.Size = new Size(206, 23);
            labelSimStepsperiod.TabIndex = 4;
            labelSimStepsperiod.Text = "Simualtion steps period:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            label1.Location = new Point(141, 78);
            label1.Name = "label1";
            label1.Size = new Size(50, 23);
            label1.TabIndex = 5;
            label1.Text = "piece";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            label2.Location = new Point(141, 173);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 6;
            label2.Text = "sec";
            // 
            // _openFileDialog
            // 
            _openFileDialog.Title = "Load config file";
            // 
            // textBoxSimulationNumber
            // 
            textBoxSimulationNumber.BackColor = Color.DimGray;
            textBoxSimulationNumber.Location = new Point(10, 77);
            textBoxSimulationNumber.Name = "textBoxSimulationNumber";
            textBoxSimulationNumber.Size = new Size(125, 27);
            textBoxSimulationNumber.TabIndex = 7;
            textBoxSimulationNumber.Text = "-1";
            textBoxSimulationNumber.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxSimulationPeriod
            // 
            textBoxSimulationPeriod.BackColor = Color.DimGray;
            textBoxSimulationPeriod.Location = new Point(10, 169);
            textBoxSimulationPeriod.Name = "textBoxSimulationPeriod";
            textBoxSimulationPeriod.Size = new Size(125, 27);
            textBoxSimulationPeriod.TabIndex = 8;
            textBoxSimulationPeriod.Text = "1";
            textBoxSimulationPeriod.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new System.Drawing.Font("Segoe UI", 6F, FontStyle.Italic, GraphicsUnit.Point, 238);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(10, 116);
            label3.Name = "label3";
            label3.Size = new Size(363, 12);
            label3.TabIndex = 10;
            label3.Text = "Note that the default -1 value set the simulation steps number until the final step";
            // 
            // _groupBox2SettingSimulation
            // 
            _groupBox2SettingSimulation.BackColor = SystemColors.ScrollBar;
            _groupBox2SettingSimulation.Controls.Add(textBoxSimulationNumber);
            _groupBox2SettingSimulation.Controls.Add(label2);
            _groupBox2SettingSimulation.Controls.Add(textBoxSimulationPeriod);
            _groupBox2SettingSimulation.Controls.Add(label3);
            _groupBox2SettingSimulation.Controls.Add(labelSimSteps);
            _groupBox2SettingSimulation.Controls.Add(labelSimStepsperiod);
            _groupBox2SettingSimulation.Controls.Add(label1);
            _groupBox2SettingSimulation.Location = new Point(12, 189);
            _groupBox2SettingSimulation.Name = "_groupBox2SettingSimulation";
            _groupBox2SettingSimulation.Size = new Size(458, 212);
            _groupBox2SettingSimulation.TabIndex = 2;
            _groupBox2SettingSimulation.TabStop = false;
            _groupBox2SettingSimulation.Text = "Simulation settings";
            // 
            // NewSimulationView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FloralWhite;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(482, 553);
            Controls.Add(_groupBox2SettingSimulation);
            Controls.Add(_groupboxLoadConfigFile);
            Controls.Add(_buttonRun);
            Controls.Add(_buttonCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NewSimulationView";
            Text = "Simulation Settings";
            _groupboxLoadConfigFile.ResumeLayout(false);
            _groupboxLoadConfigFile.PerformLayout();
            _groupBox2SettingSimulation.ResumeLayout(false);
            _groupBox2SettingSimulation.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button _buttonCancel;
        private Button _buttonRun;
        private GroupBox _groupboxLoadConfigFile;
        private Button _buttonChoseFile;
        private Label labelSimSteps;
        private Label labelSimStepsperiod;
        private Label label1;
        private Label label2;
        private Label labelConfigFileName;
        private OpenFileDialog _openFileDialog;
        private TextBox textBoxSimulationNumber;
        private TextBox textBoxSimulationPeriod;
        private Label label3;
        private GroupBox _groupBox2SettingSimulation;
    }
}