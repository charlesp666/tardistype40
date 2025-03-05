namespace LeapFrog
{
    partial class frmGameAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGameAbout));
            this.btnOK = new System.Windows.Forms.Button();
            this.picFroggy = new System.Windows.Forms.PictureBox();
            this.lblNameOfGame = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblRights = new System.Windows.Forms.Label();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picFroggy)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Gray;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(345, 211);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // picFroggy
            // 
            this.picFroggy.Image = ((System.Drawing.Image)(resources.GetObject("picFroggy.Image")));
            this.picFroggy.Location = new System.Drawing.Point(12, 12);
            this.picFroggy.Name = "picFroggy";
            this.picFroggy.Size = new System.Drawing.Size(119, 222);
            this.picFroggy.TabIndex = 1;
            this.picFroggy.TabStop = false;
            // 
            // lblNameOfGame
            // 
            this.lblNameOfGame.AutoSize = true;
            this.lblNameOfGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNameOfGame.Location = new System.Drawing.Point(148, 12);
            this.lblNameOfGame.Name = "lblNameOfGame";
            this.lblNameOfGame.Size = new System.Drawing.Size(163, 24);
            this.lblNameOfGame.TabIndex = 2;
            this.lblNameOfGame.Text = "lblNameOfGame";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(148, 171);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(103, 20);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "lblCopyright";
            // 
            // lblRights
            // 
            this.lblRights.AutoSize = true;
            this.lblRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRights.Location = new System.Drawing.Point(149, 191);
            this.lblRights.Name = "lblRights";
            this.lblRights.Size = new System.Drawing.Size(68, 16);
            this.lblRights.TabIndex = 4;
            this.lblRights.Text = "lblRights";
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.Location = new System.Drawing.Point(149, 47);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(93, 20);
            this.lblSubTitle.TabIndex = 5;
            this.lblSubTitle.Text = "lblSubTitle";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.Location = new System.Drawing.Point(150, 77);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(147, 20);
            this.lblCompanyName.TabIndex = 6;
            this.lblCompanyName.Text = "lblCompanyName";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(154, 101);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(80, 17);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "lblVersion";
            // 
            // frmGameAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(449, 246);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblSubTitle);
            this.Controls.Add(this.lblRights);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblNameOfGame);
            this.Controls.Add(this.picFroggy);
            this.Controls.Add(this.btnOK);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "frmGameAbout";
            this.Text = "GameAbout";
            ((System.ComponentModel.ISupportInitialize)(this.picFroggy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox picFroggy;
        private System.Windows.Forms.Label lblNameOfGame;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblRights;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label lblVersion;
    }
}