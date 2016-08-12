namespace LeapFrog
{
    partial class LeapFrogSplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LeapFrogSplashScreen));
            this.lblGameTitle = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.picGameImage = new System.Windows.Forms.PictureBox();
            this.textSubtitle = new System.Windows.Forms.TextBox();
            this.textRights = new System.Windows.Forms.TextBox();
            this.pbGameIntro = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.picGameImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGameTitle
            // 
            this.lblGameTitle.AutoSize = true;
            this.lblGameTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameTitle.ForeColor = System.Drawing.Color.White;
            this.lblGameTitle.Location = new System.Drawing.Point(160, 28);
            this.lblGameTitle.Name = "lblGameTitle";
            this.lblGameTitle.Size = new System.Drawing.Size(303, 55);
            this.lblGameTitle.TabIndex = 0;
            this.lblGameTitle.Text = "lblGameTitle";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.White;
            this.lblCopyright.Location = new System.Drawing.Point(166, 135);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(103, 20);
            this.lblCopyright.TabIndex = 1;
            this.lblCopyright.Text = "lblCopyright";
            // 
            // picGameImage
            // 
            this.picGameImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picGameImage.Image = ((System.Drawing.Image)(resources.GetObject("picGameImage.Image")));
            this.picGameImage.Location = new System.Drawing.Point(22, 28);
            this.picGameImage.Name = "picGameImage";
            this.picGameImage.Size = new System.Drawing.Size(132, 183);
            this.picGameImage.TabIndex = 2;
            this.picGameImage.TabStop = false;
            // 
            // textSubtitle
            // 
            this.textSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.textSubtitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textSubtitle.ForeColor = System.Drawing.Color.White;
            this.textSubtitle.Location = new System.Drawing.Point(170, 95);
            this.textSubtitle.Name = "textSubtitle";
            this.textSubtitle.Size = new System.Drawing.Size(352, 22);
            this.textSubtitle.TabIndex = 3;
            this.textSubtitle.Text = "textSubtitle";
            this.textSubtitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textRights
            // 
            this.textRights.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.textRights.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textRights.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textRights.ForeColor = System.Drawing.Color.White;
            this.textRights.Location = new System.Drawing.Point(170, 167);
            this.textRights.Name = "textRights";
            this.textRights.Size = new System.Drawing.Size(120, 15);
            this.textRights.TabIndex = 4;
            this.textRights.Text = "textRights";
            this.textRights.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbGameIntro
            // 
            this.pbGameIntro.Location = new System.Drawing.Point(170, 198);
            this.pbGameIntro.Name = "pbGameIntro";
            this.pbGameIntro.Size = new System.Drawing.Size(352, 12);
            this.pbGameIntro.TabIndex = 5;
            // 
            // LeapFrogSplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(534, 231);
            this.Controls.Add(this.pbGameIntro);
            this.Controls.Add(this.textRights);
            this.Controls.Add(this.textSubtitle);
            this.Controls.Add(this.picGameImage);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblGameTitle);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "LeapFrogSplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LeapFrog";
            ((System.ComponentModel.ISupportInitialize)(this.picGameImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGameTitle;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.PictureBox picGameImage;
        private System.Windows.Forms.TextBox textSubtitle;
        private System.Windows.Forms.TextBox textRights;
        private System.Windows.Forms.ProgressBar pbGameIntro;
    }
}

