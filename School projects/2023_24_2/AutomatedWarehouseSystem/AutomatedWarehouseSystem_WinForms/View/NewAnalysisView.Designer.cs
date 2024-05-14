using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AutomatedWarehouseSystem_WinForms.View
{
    partial class NewAnalysisView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAnalysisView));
            _buttonCancel = new Button();
            _buttonRun = new Button();
            _groupboxLoadConfigFile = new GroupBox();
            _buttonChoseMap = new Button();
            labelMapFileName = new Label();
            labelConfigFileName = new Label();
            _buttonChoseFile = new Button();
            _openFileDialog = new OpenFileDialog();
            _groupboxLoadConfigFile.SuspendLayout();
            SuspendLayout();
            // 
            // _buttonCancel
            // 
            _buttonCancel.BackColor = Color.Gray;
            _buttonCancel.ForeColor = Color.White;
            _buttonCancel.Location = new Point(12, 430);
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
            _buttonRun.Location = new Point(332, 430);
            _buttonRun.Name = "_buttonRun";
            _buttonRun.Size = new Size(138, 43);
            _buttonRun.TabIndex = 1;
            _buttonRun.Text = "Run analysis";
            _buttonRun.UseVisualStyleBackColor = false;
            _buttonRun.Click += runSimulationButton_Click;
            // 
            // _groupboxLoadConfigFile
            // 
            _groupboxLoadConfigFile.BackColor = SystemColors.ScrollBar;
            _groupboxLoadConfigFile.Controls.Add(_buttonChoseMap);
            _groupboxLoadConfigFile.Controls.Add(labelMapFileName);
            _groupboxLoadConfigFile.Controls.Add(labelConfigFileName);
            _groupboxLoadConfigFile.Controls.Add(_buttonChoseFile);
            _groupboxLoadConfigFile.Location = new Point(12, 12);
            _groupboxLoadConfigFile.Name = "_groupboxLoadConfigFile";
            _groupboxLoadConfigFile.Size = new Size(458, 298);
            _groupboxLoadConfigFile.TabIndex = 2;
            _groupboxLoadConfigFile.TabStop = false;
            _groupboxLoadConfigFile.Text = "Load log file and map file";
            // 
            // _buttonChoseMap
            // 
            _buttonChoseMap.BackColor = Color.Silver;
            _buttonChoseMap.Enabled = false;
            _buttonChoseMap.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            _buttonChoseMap.ForeColor = Color.White;
            _buttonChoseMap.Location = new Point(91, 166);
            _buttonChoseMap.Name = "_buttonChoseMap";
            _buttonChoseMap.Size = new Size(252, 50);
            _buttonChoseMap.TabIndex = 9;
            _buttonChoseMap.Text = "Choose map file to upload";
            _buttonChoseMap.UseVisualStyleBackColor = false;
            _buttonChoseMap.Click += chooseMapButton_Click;
            // 
            // labelMapFileName
            // 
            labelMapFileName.AutoSize = true;
            labelMapFileName.Font = new System.Drawing.Font("Segoe UI", 7.8F, FontStyle.Italic);
            labelMapFileName.Location = new Point(149, 231);
            labelMapFileName.Name = "labelMapFileName";
            labelMapFileName.Size = new Size(117, 17);
            labelMapFileName.TabIndex = 8;
            labelMapFileName.Text = "Loaded map file:   -";
            // 
            // labelConfigFileName
            // 
            labelConfigFileName.AutoSize = true;
            labelConfigFileName.Font = new System.Drawing.Font("Segoe UI", 7.8F, FontStyle.Italic);
            labelConfigFileName.Location = new Point(149, 112);
            labelConfigFileName.Name = "labelConfigFileName";
            labelConfigFileName.Size = new Size(109, 17);
            labelConfigFileName.TabIndex = 7;
            labelConfigFileName.Text = "Loaded log file:   -";
            // 
            // _buttonChoseFile
            // 
            _buttonChoseFile.BackColor = Color.DimGray;
            _buttonChoseFile.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            _buttonChoseFile.ForeColor = Color.White;
            _buttonChoseFile.Location = new Point(105, 50);
            _buttonChoseFile.Name = "_buttonChoseFile";
            _buttonChoseFile.Size = new Size(228, 50);
            _buttonChoseFile.TabIndex = 0;
            _buttonChoseFile.Text = "Choose log file to upload";
            _buttonChoseFile.UseVisualStyleBackColor = false;
            _buttonChoseFile.Click += chooseFileButton_Click;
            // 
            // _openFileDialog
            // 
            _openFileDialog.Title = "Load log file";
            // 
            // NewAnalysisView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FloralWhite;
            BackgroundImage = (System.Drawing.Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(482, 485);
            Controls.Add(_groupboxLoadConfigFile);
            Controls.Add(_buttonRun);
            Controls.Add(_buttonCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NewAnalysisView";
            Text = "Simulation Settings";
            _groupboxLoadConfigFile.ResumeLayout(false);
            _groupboxLoadConfigFile.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button _buttonCancel;
        private Button _buttonRun;
        private GroupBox _groupboxLoadConfigFile;
        private Button _buttonChoseFile;
        private Label labelConfigFileName;
        private OpenFileDialog _openFileDialog;
        private Label labelMapFileName;
        private Button _buttonChoseMap;
    }
}