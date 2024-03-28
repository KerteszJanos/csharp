using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

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
            _groupboxLoadConfigFile.SuspendLayout();
            SuspendLayout();
            // 
            // _buttonCancel
            // 
            _buttonCancel.Location = new Point(22, 512);
            _buttonCancel.Name = "_buttonCancel";
            _buttonCancel.Size = new Size(118, 29);
            _buttonCancel.TabIndex = 0;
            _buttonCancel.Text = "Cancel";
            _buttonCancel.UseVisualStyleBackColor = true;
            _buttonCancel.Click += cancel_Click;
            // 
            // _buttonRun
            // 
            _buttonRun.BackColor = Color.DarkOrange;
            _buttonRun.Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            _buttonRun.Location = new Point(325, 512);
            _buttonRun.Name = "_buttonRun";
            _buttonRun.Size = new Size(128, 29);
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
            _buttonChoseFile.BackColor = Color.DarkOrange;
            _buttonChoseFile.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            _buttonChoseFile.Location = new Point(117, 61);
            _buttonChoseFile.Name = "_buttonChoseFile";
            _buttonChoseFile.Size = new Size(193, 34);
            _buttonChoseFile.TabIndex = 0;
            _buttonChoseFile.Text = "Choose file to upload";
            _buttonChoseFile.UseVisualStyleBackColor = false;
            _buttonChoseFile.Click += chooseFileButton_Click;
            // 
            // labelSimSteps
            // 
            labelSimSteps.AutoSize = true;
            labelSimSteps.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            labelSimSteps.Location = new Point(12, 225);
            labelSimSteps.Name = "labelSimSteps";
            labelSimSteps.Size = new Size(217, 23);
            labelSimSteps.TabIndex = 3;
            labelSimSteps.Text = "Simualtion steps number:";
            // 
            // labelSimStepsperiod
            // 
            labelSimStepsperiod.AutoSize = true;
            labelSimStepsperiod.Font = new System.Drawing.Font("Segoe UI", 10.2F, FontStyle.Bold);
            labelSimStepsperiod.Location = new Point(12, 339);
            labelSimStepsperiod.Name = "labelSimStepsperiod";
            labelSimStepsperiod.Size = new Size(206, 23);
            labelSimStepsperiod.TabIndex = 4;
            labelSimStepsperiod.Text = "Simualtion steps period:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            label1.Location = new Point(149, 269);
            label1.Name = "label1";
            label1.Size = new Size(50, 23);
            label1.TabIndex = 5;
            label1.Text = "piece";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            label2.Location = new Point(149, 384);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 6;
            label2.Text = "sec";
            // 
            // _openFileDialog
            // 
            _openFileDialog.Title = "Load config file";
            // 
            // NewSimulationView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FloralWhite;
            ClientSize = new Size(482, 553);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelSimStepsperiod);
            Controls.Add(labelSimSteps);
            Controls.Add(_groupboxLoadConfigFile);
            Controls.Add(_buttonRun);
            Controls.Add(_buttonCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NewSimulationView";
            Text = "Simulation Settings";
            _groupboxLoadConfigFile.ResumeLayout(false);
            _groupboxLoadConfigFile.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
    }
}