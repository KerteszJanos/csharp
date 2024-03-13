namespace Asteroids.WinForms
{
    partial class Form1
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
            menuStrip = new MenuStrip();
            startNewGame = new ToolStripMenuItem();
            loadGame = new ToolStripMenuItem();
            exit = new ToolStripMenuItem();
            saveGame = new ToolStripMenuItem();
            menuTime = new ToolStripTextBox();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { startNewGame, loadGame, exit, saveGame, menuTime });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(532, 28);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "Menu";
            // 
            // startNewGame
            // 
            startNewGame.Name = "startNewGame";
            startNewGame.Size = new Size(127, 24);
            startNewGame.Text = "Start new game";
            startNewGame.Click += startNewGame_Click;
            // 
            // loadGame
            // 
            loadGame.Name = "loadGame";
            loadGame.Size = new Size(98, 24);
            loadGame.Text = "Load game";
            loadGame.Click += loadGame_Click;
            // 
            // exit
            // 
            exit.Name = "exit";
            exit.Size = new Size(47, 24);
            exit.Text = "Exit";
            exit.Click += exit_Click;
            // 
            // saveGame
            // 
            saveGame.Name = "saveGame";
            saveGame.Size = new Size(96, 27);
            saveGame.Text = "Save game";
            saveGame.Visible = false;
            saveGame.Click += saveGame_Click;
            // 
            // menuTime
            // 
            menuTime.Name = "menuTime";
            menuTime.Size = new Size(100, 27);
            menuTime.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(532, 533);
            Controls.Add(menuStrip);
            Name = "Form1";
            Text = "Asteroids";
            KeyDown += esc_a_d_KeyDown;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip;
        private ToolStripMenuItem startNewGame;
        private ToolStripMenuItem loadGame;
        private ToolStripMenuItem exit;
        private ToolStripMenuItem saveGame;
        private ToolStripTextBox menuTime;
    }
}