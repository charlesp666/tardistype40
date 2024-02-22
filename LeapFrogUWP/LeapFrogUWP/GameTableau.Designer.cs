namespace LeapFrog
{
    partial class GameTableau
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
            this.components = new System.ComponentModel.Container();
            this.dataGridGameBoard = new System.Windows.Forms.DataGridView();
            this.menuStripGame = new System.Windows.Forms.MenuStrip();
            this.gameStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMoveCount = new System.Windows.Forms.Label();
            this.txtMoveCount = new System.Windows.Forms.TextBox();
            this.gameTime = new System.Windows.Forms.Timer(this.components);
            this.lblGameTimer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGameBoard)).BeginInit();
            this.menuStripGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridGameBoard
            // 
            this.dataGridGameBoard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridGameBoard.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridGameBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridGameBoard.ColumnHeadersVisible = false;
            this.dataGridGameBoard.EnableHeadersVisualStyles = false;
            this.dataGridGameBoard.Location = new System.Drawing.Point(4, 34);
            this.dataGridGameBoard.Name = "dataGridGameBoard";
            this.dataGridGameBoard.RowHeadersVisible = false;
            this.dataGridGameBoard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridGameBoard.Size = new System.Drawing.Size(1015, 412);
            this.dataGridGameBoard.TabIndex = 0;
            this.dataGridGameBoard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridGameBoard_CellClick);
            // 
            // menuStripGame
            // 
            this.menuStripGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.menuStripGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStripGame.Location = new System.Drawing.Point(0, 0);
            this.menuStripGame.Name = "menuStripGame";
            this.menuStripGame.Size = new System.Drawing.Size(1020, 24);
            this.menuStripGame.TabIndex = 3;
            this.menuStripGame.Text = "menuStripGame";
            // 
            // gameStripMenuItem
            // 
            this.gameStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.playerStatisticsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.gameStripMenuItem.Name = "gameStripMenuItem";
            this.gameStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameStripMenuItem.Text = "&Game";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newGameToolStripMenuItem.Text = "&New Game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // playerStatisticsToolStripMenuItem
            // 
            this.playerStatisticsToolStripMenuItem.Name = "playerStatisticsToolStripMenuItem";
            this.playerStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.playerStatisticsToolStripMenuItem.Text = "Player &Statistics";
            this.playerStatisticsToolStripMenuItem.Click += new System.EventHandler(this.playerStatisticsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // gameHelpToolStripMenuItem
            // 
            this.gameHelpToolStripMenuItem.Name = "gameHelpToolStripMenuItem";
            this.gameHelpToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.gameHelpToolStripMenuItem.Text = "&Help";
            this.gameHelpToolStripMenuItem.Click += new System.EventHandler(this.gameHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lblMoveCount
            // 
            this.lblMoveCount.AutoSize = true;
            this.lblMoveCount.BackColor = System.Drawing.Color.Blue;
            this.lblMoveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoveCount.ForeColor = System.Drawing.Color.White;
            this.lblMoveCount.Location = new System.Drawing.Point(6, 454);
            this.lblMoveCount.Name = "lblMoveCount";
            this.lblMoveCount.Size = new System.Drawing.Size(65, 20);
            this.lblMoveCount.TabIndex = 4;
            this.lblMoveCount.Text = "Moves:";
            // 
            // txtMoveCount
            // 
            this.txtMoveCount.BackColor = System.Drawing.Color.Blue;
            this.txtMoveCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoveCount.ForeColor = System.Drawing.Color.White;
            this.txtMoveCount.Location = new System.Drawing.Point(78, 452);
            this.txtMoveCount.Name = "txtMoveCount";
            this.txtMoveCount.ReadOnly = true;
            this.txtMoveCount.Size = new System.Drawing.Size(100, 26);
            this.txtMoveCount.TabIndex = 5;
            this.txtMoveCount.Tag = "Number of Moves Made";
            this.txtMoveCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblGameTimer
            // 
            this.lblGameTimer.AutoSize = true;
            this.lblGameTimer.BackColor = System.Drawing.Color.Blue;
            this.lblGameTimer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGameTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameTimer.ForeColor = System.Drawing.Color.White;
            this.lblGameTimer.Location = new System.Drawing.Point(201, 454);
            this.lblGameTimer.Name = "lblGameTimer";
            this.lblGameTimer.Size = new System.Drawing.Size(121, 22);
            this.lblGameTimer.TabIndex = 6;
            this.lblGameTimer.Text = "lblGameTimer";
            // 
            // GameTableau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 483);
            this.Controls.Add(this.lblGameTimer);
            this.Controls.Add(this.txtMoveCount);
            this.Controls.Add(this.lblMoveCount);
            this.Controls.Add(this.menuStripGame);
            this.Controls.Add(this.dataGridGameBoard);
            this.Name = "GameTableau";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LeapFrog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameTableau_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGameBoard)).EndInit();
            this.menuStripGame.ResumeLayout(false);
            this.menuStripGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridGameBoard;
        private System.Windows.Forms.MenuStrip menuStripGame;
        private System.Windows.Forms.ToolStripMenuItem gameStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerStatisticsToolStripMenuItem;
        private System.Windows.Forms.Label lblMoveCount;
        private System.Windows.Forms.TextBox txtMoveCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.Timer gameTime;
        private System.Windows.Forms.Label lblGameTimer;
    }
}