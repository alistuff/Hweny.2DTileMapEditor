namespace TileMapEditor.Test.Setting
{
    partial class frm_CharSet
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
            this.lsbCharset = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picCharSet = new System.Windows.Forms.PictureBox();
            this.lblCharSetInfo = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFilesPath = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCharSet)).BeginInit();
            this.SuspendLayout();
            // 
            // lsbCharset
            // 
            this.lsbCharset.FormattingEnabled = true;
            this.lsbCharset.ItemHeight = 12;
            this.lsbCharset.Location = new System.Drawing.Point(1, 3);
            this.lsbCharset.Name = "lsbCharset";
            this.lsbCharset.Size = new System.Drawing.Size(97, 268);
            this.lsbCharset.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = global::TileMapEditor.Properties.Resources.transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picCharSet);
            this.panel1.Location = new System.Drawing.Point(101, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 241);
            this.panel1.TabIndex = 1;
            // 
            // picCharSet
            // 
            this.picCharSet.BackColor = System.Drawing.Color.Transparent;
            this.picCharSet.Location = new System.Drawing.Point(0, 0);
            this.picCharSet.Name = "picCharSet";
            this.picCharSet.Size = new System.Drawing.Size(100, 50);
            this.picCharSet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picCharSet.TabIndex = 0;
            this.picCharSet.TabStop = false;
            // 
            // lblCharSetInfo
            // 
            this.lblCharSetInfo.AutoSize = true;
            this.lblCharSetInfo.ForeColor = System.Drawing.Color.Black;
            this.lblCharSetInfo.Location = new System.Drawing.Point(104, 252);
            this.lblCharSetInfo.Name = "lblCharSetInfo";
            this.lblCharSetInfo.Size = new System.Drawing.Size(0, 12);
            this.lblCharSetInfo.TabIndex = 0;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(188, 272);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(269, 272);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFilesPath
            // 
            this.btnFilesPath.Location = new System.Drawing.Point(1, 272);
            this.btnFilesPath.Name = "btnFilesPath";
            this.btnFilesPath.Size = new System.Drawing.Size(97, 23);
            this.btnFilesPath.TabIndex = 4;
            this.btnFilesPath.Text = "打开目录位置";
            this.btnFilesPath.UseVisualStyleBackColor = true;
            this.btnFilesPath.Click += new System.EventHandler(this.btnFilesPath_Click);
            // 
            // frm_CharSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 298);
            this.Controls.Add(this.lblCharSetInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnFilesPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lsbCharset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_CharSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CharSet";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCharSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbCharset;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCharSetInfo;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picCharSet;
        private System.Windows.Forms.Button btnFilesPath;
    }
}