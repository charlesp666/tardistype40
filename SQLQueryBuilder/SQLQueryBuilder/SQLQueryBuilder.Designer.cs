namespace SQLQueryBuilder
{
    partial class windowMain
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
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuFile = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openXMLFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumberOfTablesLoaded = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSQLQuery = new System.Windows.Forms.TextBox();
            this.btnCopyQuery = new System.Windows.Forms.Button();
            this.chklistQueryTables = new System.Windows.Forms.CheckedListBox();
            this.btnNewQuery = new System.Windows.Forms.Button();
            this.chklistQueryColumns = new System.Windows.Forms.CheckedListBox();
            this.grpSQLQuery = new System.Windows.Forms.GroupBox();
            this.menuFile.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.ForeColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(22, 707);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(1219, 22);
            this.txtStatus.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // menuFile
            // 
            this.menuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuFile.Location = new System.Drawing.Point(0, 0);
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(1255, 24);
            this.menuFile.TabIndex = 16;
            this.menuFile.Text = "menuFile";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openXMLFileToolStripMenuItem,
            this.showTablesToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openXMLFileToolStripMenuItem
            // 
            this.openXMLFileToolStripMenuItem.Name = "openXMLFileToolStripMenuItem";
            this.openXMLFileToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.openXMLFileToolStripMenuItem.Text = "Open XML File...";
            this.openXMLFileToolStripMenuItem.Click += new System.EventHandler(this.openXMLFileToolStripMenuItem_Click);
            // 
            // showTablesToolStripMenuItem
            // 
            this.showTablesToolStripMenuItem.Name = "showTablesToolStripMenuItem";
            this.showTablesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.showTablesToolStripMenuItem.Text = "Show Tables";
            this.showTablesToolStripMenuItem.Click += new System.EventHandler(this.showTablesToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 24);
            this.label1.TabIndex = 19;
            this.label1.Text = "Number of Tables Loaded:";
            // 
            // txtNumberOfTablesLoaded
            // 
            this.txtNumberOfTablesLoaded.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtNumberOfTablesLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfTablesLoaded.ForeColor = System.Drawing.Color.White;
            this.txtNumberOfTablesLoaded.Location = new System.Drawing.Point(273, 13);
            this.txtNumberOfTablesLoaded.Name = "txtNumberOfTablesLoaded";
            this.txtNumberOfTablesLoaded.Size = new System.Drawing.Size(48, 29);
            this.txtNumberOfTablesLoaded.TabIndex = 20;
            this.txtNumberOfTablesLoaded.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNumberOfTablesLoaded);
            this.groupBox2.Location = new System.Drawing.Point(133, 40);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 54);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // txtSQLQuery
            // 
            this.txtSQLQuery.AcceptsReturn = true;
            this.txtSQLQuery.BackColor = System.Drawing.Color.White;
            this.txtSQLQuery.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLQuery.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSQLQuery.Location = new System.Drawing.Point(506, 59);
            this.txtSQLQuery.Multiline = true;
            this.txtSQLQuery.Name = "txtSQLQuery";
            this.txtSQLQuery.ReadOnly = true;
            this.txtSQLQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSQLQuery.Size = new System.Drawing.Size(725, 586);
            this.txtSQLQuery.TabIndex = 21;
            // 
            // btnCopyQuery
            // 
            this.btnCopyQuery.BackColor = System.Drawing.Color.Black;
            this.btnCopyQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyQuery.Location = new System.Drawing.Point(638, 666);
            this.btnCopyQuery.Name = "btnCopyQuery";
            this.btnCopyQuery.Size = new System.Drawing.Size(160, 35);
            this.btnCopyQuery.TabIndex = 23;
            this.btnCopyQuery.Text = "Copy Query";
            this.btnCopyQuery.UseVisualStyleBackColor = false;
            this.btnCopyQuery.Click += new System.EventHandler(this.btnCopyQuery_Click);
            // 
            // chklistQueryTables
            // 
            this.chklistQueryTables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chklistQueryTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklistQueryTables.ForeColor = System.Drawing.Color.White;
            this.chklistQueryTables.FormattingEnabled = true;
            this.chklistQueryTables.Location = new System.Drawing.Point(22, 109);
            this.chklistQueryTables.Name = "chklistQueryTables";
            this.chklistQueryTables.Size = new System.Drawing.Size(445, 130);
            this.chklistQueryTables.TabIndex = 24;
            this.chklistQueryTables.SelectedIndexChanged += new System.EventHandler(this.chklistSQLTables_SelectedIndexChanged);
            // 
            // btnNewQuery
            // 
            this.btnNewQuery.BackColor = System.Drawing.Color.Black;
            this.btnNewQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewQuery.Location = new System.Drawing.Point(493, 666);
            this.btnNewQuery.Name = "btnNewQuery";
            this.btnNewQuery.Size = new System.Drawing.Size(139, 35);
            this.btnNewQuery.TabIndex = 25;
            this.btnNewQuery.Text = "New Query";
            this.btnNewQuery.UseVisualStyleBackColor = false;
            this.btnNewQuery.Click += new System.EventHandler(this.btnNewQuery_Click);
            // 
            // chklistQueryColumns
            // 
            this.chklistQueryColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chklistQueryColumns.CheckOnClick = true;
            this.chklistQueryColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chklistQueryColumns.ForeColor = System.Drawing.Color.White;
            this.chklistQueryColumns.FormattingEnabled = true;
            this.chklistQueryColumns.Location = new System.Drawing.Point(22, 254);
            this.chklistQueryColumns.Name = "chklistQueryColumns";
            this.chklistQueryColumns.Size = new System.Drawing.Size(445, 424);
            this.chklistQueryColumns.TabIndex = 26;
            this.chklistQueryColumns.SelectedIndexChanged += new System.EventHandler(this.chklistQueryColumns_SelectedIndexChanged);
            // 
            // grpSQLQuery
            // 
            this.grpSQLQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSQLQuery.Location = new System.Drawing.Point(493, 40);
            this.grpSQLQuery.Name = "grpSQLQuery";
            this.grpSQLQuery.Size = new System.Drawing.Size(750, 620);
            this.grpSQLQuery.TabIndex = 27;
            this.grpSQLQuery.TabStop = false;
            this.grpSQLQuery.Text = "SQLQuery";
            // 
            // windowMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1255, 730);
            this.Controls.Add(this.chklistQueryColumns);
            this.Controls.Add(this.btnNewQuery);
            this.Controls.Add(this.chklistQueryTables);
            this.Controls.Add(this.btnCopyQuery);
            this.Controls.Add(this.txtSQLQuery);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.menuFile);
            this.Controls.Add(this.grpSQLQuery);
            this.ForeColor = System.Drawing.Color.White;
            this.MainMenuStrip = this.menuFile;
            this.Name = "windowMain";
            this.Text = "SQL Query Builder";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuFile.ResumeLayout(false);
            this.menuFile.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuFile;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openXMLFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTablesToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumberOfTablesLoaded;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSQLQuery;
        private System.Windows.Forms.Button btnCopyQuery;
        private System.Windows.Forms.CheckedListBox chklistQueryTables;
        private System.Windows.Forms.Button btnNewQuery;
        private System.Windows.Forms.CheckedListBox chklistQueryColumns;
        private System.Windows.Forms.GroupBox grpSQLQuery;
    }
}

