namespace SlidingTilePuzzleGame
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
            menuStrip1 = new MenuStrip();
            choosePictureToolStripMenuItem = new ToolStripMenuItem();
            gridSizeToolStripMenuItem = new ToolStripMenuItem();
            _3x3ToolStripMenuItem = new ToolStripMenuItem();
            _4x4ToolStripMenuItem = new ToolStripMenuItem();
            _5x5ToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            lblWelcome = new Label();
            lblHelp = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { choosePictureToolStripMenuItem, gridSizeToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(532, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // choosePictureToolStripMenuItem
            // 
            choosePictureToolStripMenuItem.Name = "choosePictureToolStripMenuItem";
            choosePictureToolStripMenuItem.Size = new Size(130, 24);
            choosePictureToolStripMenuItem.Text = "Choose Picture...";
            choosePictureToolStripMenuItem.Click += choosePictureToolStripMenuItem_Click;
            // 
            // gridSizeToolStripMenuItem
            // 
            gridSizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { _3x3ToolStripMenuItem, _4x4ToolStripMenuItem, _5x5ToolStripMenuItem });
            gridSizeToolStripMenuItem.Name = "gridSizeToolStripMenuItem";
            gridSizeToolStripMenuItem.Size = new Size(82, 24);
            gridSizeToolStripMenuItem.Text = "Grid Size";
            // 
            // _3x3ToolStripMenuItem
            // 
            _3x3ToolStripMenuItem.Checked = true;
            _3x3ToolStripMenuItem.CheckState = CheckState.Checked;
            _3x3ToolStripMenuItem.Name = "_3x3ToolStripMenuItem";
            _3x3ToolStripMenuItem.Size = new Size(115, 26);
            _3x3ToolStripMenuItem.Text = "3x3";
            _3x3ToolStripMenuItem.Click += _3x3ToolStripMenuItem_Click;
            // 
            // _4x4ToolStripMenuItem
            // 
            _4x4ToolStripMenuItem.Name = "_4x4ToolStripMenuItem";
            _4x4ToolStripMenuItem.Size = new Size(115, 26);
            _4x4ToolStripMenuItem.Text = "4x4";
            _4x4ToolStripMenuItem.Click += _4x4ToolStripMenuItem_Click;
            // 
            // _5x5ToolStripMenuItem
            // 
            _5x5ToolStripMenuItem.Name = "_5x5ToolStripMenuItem";
            _5x5ToolStripMenuItem.Size = new Size(115, 26);
            _5x5ToolStripMenuItem.Text = "5x5";
            _5x5ToolStripMenuItem.Click += _5x5ToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(55, 24);
            helpToolStripMenuItem.Text = "Help";
            helpToolStripMenuItem.Click += helpToolStripMenuItem_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(205, 180);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(50, 20);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "label1";
            // 
            // lblHelp
            // 
            lblHelp.AutoSize = true;
            lblHelp.Location = new Point(205, 391);
            lblHelp.Name = "lblHelp";
            lblHelp.Size = new Size(50, 20);
            lblHelp.TabIndex = 2;
            lblHelp.Text = "label2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(532, 467);
            Controls.Add(lblHelp);
            Controls.Add(lblWelcome);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Sliding Tile Puzzle";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem choosePictureToolStripMenuItem;
        private ToolStripMenuItem gridSizeToolStripMenuItem;
        private ToolStripMenuItem _3x3ToolStripMenuItem;
        private ToolStripMenuItem _4x4ToolStripMenuItem;
        private ToolStripMenuItem _5x5ToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private Label lblWelcome;
        private Label lblHelp;
    }
}
