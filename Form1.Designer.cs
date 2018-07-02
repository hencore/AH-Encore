namespace auto_h_encore {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.barProgress = new System.Windows.Forms.ProgressBar();
            this.lblVersion = new System.Windows.Forms.Label();
            this.bt_About = new System.Windows.Forms.Button();
            this.chk_QCMA = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(15, 293);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(569, 179);
            this.txtLog.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(348, 177);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(233, 34);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(330, 242);
            this.lblInfo.TabIndex = 8;
            this.lblInfo.Click += new System.EventHandler(this.lblInfo_Click);
            // 
            // barProgress
            // 
            this.barProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barProgress.Location = new System.Drawing.Point(12, 257);
            this.barProgress.Maximum = 17;
            this.barProgress.Name = "barProgress";
            this.barProgress.Size = new System.Drawing.Size(569, 30);
            this.barProgress.TabIndex = 9;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(339, 475);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(255, 13);
            this.lblVersion.TabIndex = 10;
            this.lblVersion.Text = "ah-encore version x";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bt_About
            // 
            this.bt_About.Location = new System.Drawing.Point(348, 217);
            this.bt_About.Name = "bt_About";
            this.bt_About.Size = new System.Drawing.Size(233, 34);
            this.bt_About.TabIndex = 11;
            this.bt_About.Text = "About";
            this.bt_About.UseVisualStyleBackColor = true;
            this.bt_About.Click += new System.EventHandler(this.bt_About_Click);
            // 
            // chk_QCMA
            // 
            this.chk_QCMA.Location = new System.Drawing.Point(348, 137);
            this.chk_QCMA.Name = "chk_QCMA";
            this.chk_QCMA.Size = new System.Drawing.Size(233, 34);
            this.chk_QCMA.TabIndex = 12;
            this.chk_QCMA.Text = "Проверить наличие QCMA";
            this.chk_QCMA.UseVisualStyleBackColor = true;
            this.chk_QCMA.Click += new System.EventHandler(this.chk_QCMA_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "back.jpg";
            this.pictureBox1.Location = new System.Drawing.Point(348, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(236, 119);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 497);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chk_QCMA);
            this.Controls.Add(this.bt_About);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.barProgress);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AH-Encore";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ProgressBar barProgress;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button bt_About;
        private System.Windows.Forms.Button chk_QCMA;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

