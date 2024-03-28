﻿using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;
using Font = System.Drawing.Font;

namespace AutomatedWarehouseSystem_WinForms.View
{
    partial class MainView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            menuStrip1 = new MenuStrip();
            _buttonNewSimulation = new ToolStripMenuItem();
            _buttonNewAnalysis = new ToolStripMenuItem();
            _buttonInterrupt = new ToolStripMenuItem();
            _buttonExit = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripLabel_Task = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel3 = new ToolStripLabel();
            toolStripLabel_Warehouse = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel2 = new ToolStripLabel();
            toolStripLabel_Warehousesize = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripLabel4 = new ToolStripLabel();
            toolStripLabel_Robotsnum = new ToolStripLabel();
            toolStripSeparator4 = new ToolStripSeparator();
            toolStripLabel5 = new ToolStripLabel();
            toolStripLabel_Stepsperiod = new ToolStripLabel();
            toolStripSeparator5 = new ToolStripSeparator();
            toolStripLabel_SImsteps = new ToolStripLabel();
            toolStripLabel6 = new ToolStripLabel();
            tableLayoutPanel1 = new TableLayoutPanel();
            gridPanel = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            _buttonForewardRun = new Button();
            _buttonBackwardRun = new Button();
            _buttonStartStopRun = new Button();
            _buttonBackwardOneStep = new Button();
            labelForeward = new Label();
            _buttonForewardOnestep = new Button();
            _buttonGoToSimulationStep = new Button();
            textBoxSimulationStep = new TextBox();
            label2 = new Label();
            label3 = new Label();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 10.2F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { _buttonNewSimulation, _buttonNewAnalysis, _buttonInterrupt, _buttonExit });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1182, 31);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // _buttonNewSimulation
            // 
            _buttonNewSimulation.Name = "_buttonNewSimulation";
            _buttonNewSimulation.Size = new Size(144, 27);
            _buttonNewSimulation.Text = "New Simulation";
            _buttonNewSimulation.Click += newSimulation_Click;
            // 
            // _buttonNewAnalysis
            // 
            _buttonNewAnalysis.Name = "_buttonNewAnalysis";
            _buttonNewAnalysis.Size = new Size(123, 27);
            _buttonNewAnalysis.Text = "New Analysis";
            _buttonNewAnalysis.Click += newanalysis_Click;
            // 
            // _buttonInterrupt
            // 
            _buttonInterrupt.Enabled = false;
            _buttonInterrupt.Name = "_buttonInterrupt";
            _buttonInterrupt.Size = new Size(154, 27);
            _buttonInterrupt.Text = "Interrupt Process";
            _buttonInterrupt.Click += interruptButton_Click;
            // 
            // _buttonExit
            // 
            _buttonExit.Name = "_buttonExit";
            _buttonExit.Size = new Size(51, 27);
            _buttonExit.Text = "Exit";
            _buttonExit.Click += exit_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.BackColor = SystemColors.ControlLight;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Font = new Font("Segoe UI", 10.2F);
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripLabel_Task, toolStripSeparator1, toolStripLabel3, toolStripLabel_Warehouse, toolStripSeparator2, toolStripLabel2, toolStripLabel_Warehousesize, toolStripSeparator3, toolStripLabel4, toolStripLabel_Robotsnum, toolStripSeparator4, toolStripLabel5, toolStripLabel_Stepsperiod, toolStripSeparator5, toolStripLabel_SImsteps, toolStripLabel6 });
            toolStrip1.Location = new Point(0, 727);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(1182, 26);
            toolStrip1.Stretch = true;
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(45, 23);
            toolStripLabel1.Text = "Task:";
            // 
            // toolStripLabel_Task
            // 
            toolStripLabel_Task.Name = "toolStripLabel_Task";
            toolStripLabel_Task.Size = new Size(17, 23);
            toolStripLabel_Task.Text = "-";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 26);
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(147, 23);
            toolStripLabel3.Text = "Warehouse name:";
            // 
            // toolStripLabel_Warehouse
            // 
            toolStripLabel_Warehouse.Name = "toolStripLabel_Warehouse";
            toolStripLabel_Warehouse.Size = new Size(17, 23);
            toolStripLabel_Warehouse.Text = "-";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 26);
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(132, 23);
            toolStripLabel2.Text = "Warehouse size:";
            // 
            // toolStripLabel_Warehousesize
            // 
            toolStripLabel_Warehousesize.Name = "toolStripLabel_Warehousesize";
            toolStripLabel_Warehousesize.Size = new Size(36, 23);
            toolStripLabel_Warehousesize.Text = "0x0";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 26);
            // 
            // toolStripLabel4
            // 
            toolStripLabel4.Name = "toolStripLabel4";
            toolStripLabel4.Size = new Size(132, 23);
            toolStripLabel4.Text = "Robots number:";
            // 
            // toolStripLabel_Robotsnum
            // 
            toolStripLabel_Robotsnum.Name = "toolStripLabel_Robotsnum";
            toolStripLabel_Robotsnum.Size = new Size(17, 23);
            toolStripLabel_Robotsnum.Text = "-";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 26);
            // 
            // toolStripLabel5
            // 
            toolStripLabel5.Name = "toolStripLabel5";
            toolStripLabel5.Size = new Size(142, 23);
            toolStripLabel5.Text = "Steps timeperiod:";
            // 
            // toolStripLabel_Stepsperiod
            // 
            toolStripLabel_Stepsperiod.Name = "toolStripLabel_Stepsperiod";
            toolStripLabel_Stepsperiod.Size = new Size(48, 23);
            toolStripLabel_Stepsperiod.Text = "0 sec";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 26);
            // 
            // toolStripLabel_SImsteps
            // 
            toolStripLabel_SImsteps.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel_SImsteps.Name = "toolStripLabel_SImsteps";
            toolStripLabel_SImsteps.Size = new Size(45, 23);
            toolStripLabel_SImsteps.Text = "0 / 0";
            // 
            // toolStripLabel6
            // 
            toolStripLabel6.Alignment = ToolStripItemAlignment.Right;
            toolStripLabel6.Name = "toolStripLabel6";
            toolStripLabel6.Size = new Size(183, 23);
            toolStripLabel6.Text = "Actual simulation step:";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(gridPanel, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Location = new Point(0, 31);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 77.28571F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 22.7142849F));
            tableLayoutPanel1.Size = new Size(1182, 700);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // gridPanel
            // 
            gridPanel.AllowDrop = true;
            gridPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridPanel.AutoScroll = true;
            gridPanel.AutoScrollMinSize = new Size(1000, 0);
            gridPanel.BackColor = SystemColors.ControlLight;
            gridPanel.Location = new Point(23, 3);
            gridPanel.Name = "gridPanel";
            gridPanel.Size = new Size(1136, 535);
            gridPanel.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 11;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.8521128F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.32042253F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.9084511F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.40845072F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.996479F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.398382F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.1577358F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.87967658F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.9170876F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.87967658F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.1577358F));
            tableLayoutPanel2.Controls.Add(label1, 0, 2);
            tableLayoutPanel2.Controls.Add(_buttonForewardRun, 4, 1);
            tableLayoutPanel2.Controls.Add(_buttonBackwardRun, 0, 1);
            tableLayoutPanel2.Controls.Add(_buttonStartStopRun, 2, 1);
            tableLayoutPanel2.Controls.Add(_buttonBackwardOneStep, 6, 1);
            tableLayoutPanel2.Controls.Add(labelForeward, 4, 2);
            tableLayoutPanel2.Controls.Add(_buttonForewardOnestep, 10, 1);
            tableLayoutPanel2.Controls.Add(_buttonGoToSimulationStep, 8, 1);
            tableLayoutPanel2.Controls.Add(textBoxSimulationStep, 8, 2);
            tableLayoutPanel2.Controls.Add(label2, 6, 2);
            tableLayoutPanel2.Controls.Add(label3, 10, 2);
            tableLayoutPanel2.Location = new Point(23, 544);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20.1005F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 49.74874F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30.150753F));
            tableLayoutPanel2.Size = new Size(1136, 153);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(3, 106);
            label1.Name = "label1";
            label1.Size = new Size(140, 47);
            label1.TabIndex = 10;
            label1.Text = "2x";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // _buttonForewardRun
            // 
            _buttonForewardRun.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _buttonForewardRun.BackColor = SystemColors.ControlLightLight;
            _buttonForewardRun.BackgroundImage = (System.Drawing.Image)resources.GetObject("_buttonForewardRun.BackgroundImage");
            _buttonForewardRun.BackgroundImageLayout = ImageLayout.Center;
            _buttonForewardRun.Enabled = false;
            _buttonForewardRun.Location = new Point(338, 33);
            _buttonForewardRun.Name = "_buttonForewardRun";
            _buttonForewardRun.Size = new Size(153, 70);
            _buttonForewardRun.TabIndex = 4;
            _buttonForewardRun.UseVisualStyleBackColor = false;
            _buttonForewardRun.Click += forewardButton_Click;
            // 
            // _buttonBackwardRun
            // 
            _buttonBackwardRun.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _buttonBackwardRun.BackColor = SystemColors.ControlLightLight;
            _buttonBackwardRun.BackgroundImage = (System.Drawing.Image)resources.GetObject("_buttonBackwardRun.BackgroundImage");
            _buttonBackwardRun.BackgroundImageLayout = ImageLayout.Center;
            _buttonBackwardRun.Enabled = false;
            _buttonBackwardRun.Location = new Point(3, 33);
            _buttonBackwardRun.Name = "_buttonBackwardRun";
            _buttonBackwardRun.Size = new Size(140, 70);
            _buttonBackwardRun.TabIndex = 5;
            _buttonBackwardRun.UseVisualStyleBackColor = false;
            _buttonBackwardRun.Click += backwardButton_Click;
            // 
            // _buttonStartStopRun
            // 
            _buttonStartStopRun.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _buttonStartStopRun.BackColor = Color.DarkOrange;
            _buttonStartStopRun.BackgroundImage = (System.Drawing.Image)resources.GetObject("_buttonStartStopRun.BackgroundImage");
            _buttonStartStopRun.BackgroundImageLayout = ImageLayout.Center;
            _buttonStartStopRun.Enabled = false;
            _buttonStartStopRun.Location = new Point(164, 33);
            _buttonStartStopRun.Name = "_buttonStartStopRun";
            _buttonStartStopRun.Size = new Size(152, 70);
            _buttonStartStopRun.TabIndex = 3;
            _buttonStartStopRun.UseVisualStyleBackColor = false;
            _buttonStartStopRun.Click += startStopButton_Click;
            // 
            // _buttonBackwardOneStep
            // 
            _buttonBackwardOneStep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _buttonBackwardOneStep.BackColor = SystemColors.ControlLightLight;
            _buttonBackwardOneStep.BackgroundImage = (System.Drawing.Image)resources.GetObject("_buttonBackwardOneStep.BackgroundImage");
            _buttonBackwardOneStep.BackgroundImageLayout = ImageLayout.Center;
            _buttonBackwardOneStep.Enabled = false;
            _buttonBackwardOneStep.Location = new Point(603, 33);
            _buttonBackwardOneStep.Name = "_buttonBackwardOneStep";
            _buttonBackwardOneStep.Size = new Size(143, 70);
            _buttonBackwardOneStep.TabIndex = 7;
            _buttonBackwardOneStep.UseVisualStyleBackColor = false;
            _buttonBackwardOneStep.Click += backwardOneStepButton_Click;
            // 
            // labelForeward
            // 
            labelForeward.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelForeward.AutoSize = true;
            labelForeward.Location = new Point(338, 106);
            labelForeward.Name = "labelForeward";
            labelForeward.Size = new Size(153, 47);
            labelForeward.TabIndex = 6;
            labelForeward.Text = "2x";
            labelForeward.TextAlign = ContentAlignment.TopCenter;
            // 
            // _buttonForewardOnestep
            // 
            _buttonForewardOnestep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _buttonForewardOnestep.BackColor = SystemColors.ControlLightLight;
            _buttonForewardOnestep.BackgroundImage = (System.Drawing.Image)resources.GetObject("_buttonForewardOnestep.BackgroundImage");
            _buttonForewardOnestep.BackgroundImageLayout = ImageLayout.Center;
            _buttonForewardOnestep.Enabled = false;
            _buttonForewardOnestep.Location = new Point(986, 33);
            _buttonForewardOnestep.Name = "_buttonForewardOnestep";
            _buttonForewardOnestep.Size = new Size(147, 70);
            _buttonForewardOnestep.TabIndex = 9;
            _buttonForewardOnestep.UseVisualStyleBackColor = false;
            _buttonForewardOnestep.Click += forewardOneStepButton_Click;
            // 
            // _buttonGoToSimulationStep
            // 
            _buttonGoToSimulationStep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _buttonGoToSimulationStep.BackColor = Color.DarkOrange;
            _buttonGoToSimulationStep.Enabled = false;
            _buttonGoToSimulationStep.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            _buttonGoToSimulationStep.Location = new Point(773, 33);
            _buttonGoToSimulationStep.Name = "_buttonGoToSimulationStep";
            _buttonGoToSimulationStep.Size = new Size(186, 70);
            _buttonGoToSimulationStep.TabIndex = 11;
            _buttonGoToSimulationStep.Text = "Go to simulation step";
            _buttonGoToSimulationStep.UseVisualStyleBackColor = false;
            _buttonGoToSimulationStep.Click += goToSimulationStep_Button;
            // 
            // textBoxSimulationStep
            // 
            textBoxSimulationStep.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSimulationStep.Enabled = false;
            textBoxSimulationStep.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            textBoxSimulationStep.Location = new Point(773, 109);
            textBoxSimulationStep.Name = "textBoxSimulationStep";
            textBoxSimulationStep.Size = new Size(186, 34);
            textBoxSimulationStep.TabIndex = 12;
            textBoxSimulationStep.Text = "0";
            textBoxSimulationStep.TextAlign = HorizontalAlignment.Center;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(603, 106);
            label2.Name = "label2";
            label2.Size = new Size(143, 47);
            label2.TabIndex = 13;
            label2.Text = "Step backward";
            label2.TextAlign = ContentAlignment.TopCenter;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(986, 106);
            label3.Name = "label3";
            label3.Size = new Size(147, 47);
            label3.TabIndex = 14;
            label3.Text = "Step foreward";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // MainView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FloralWhite;
            ClientSize = new Size(1182, 753);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainView";
            Text = "Warehouse system app";
            SizeChanged += MainView_SizeChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem _buttonNewSimulation;
        private ToolStripMenuItem _buttonNewAnalysis;
        private ToolStripMenuItem _buttonInterrupt;
        private ToolStripMenuItem _buttonExit;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel toolStripLabel_Task;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel3;
        private ToolStripLabel toolStripLabel_Warehouse;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripLabel toolStripLabel2;
        private ToolStripLabel toolStripLabel_Warehousesize;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel toolStripLabel4;
        private ToolStripLabel toolStripLabel_Robotsnum;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripLabel toolStripLabel5;
        private ToolStripLabel toolStripLabel_Stepsperiod;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripLabel toolStripLabel6;
        private ToolStripLabel toolStripLabel_SImsteps;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel gridPanel;
        private Button _buttonStartStopRun;
        private Button _buttonForewardRun;
        private Button _buttonBackwardRun;
        private TableLayoutPanel tableLayoutPanel2;
        private Label labelForeward;
        private Button _buttonBackwardOneStep;
        private Button _buttonForewardOnestep;
        private Label label1;
        private Button _buttonGoToSimulationStep;
        private TextBox textBoxSimulationStep;
        private Label label2;
        private Label label3;
    }
}
