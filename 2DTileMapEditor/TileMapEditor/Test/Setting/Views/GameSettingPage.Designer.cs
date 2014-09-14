namespace TileMapEditor.Test.Setting
{
    partial class GameSettingPage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbbPlayer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbGameType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetFps = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtScreenHeight = new System.Windows.Forms.NumericUpDown();
            this.btnSetDefault = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtScreenWidth = new System.Windows.Forms.NumericUpDown();
            this.txtTitle = new TileMapEditor.Controls.ControlsEx.PromptTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetFps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScreenHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScreenWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbbPlayer);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbbGameType);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtTargetFps);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtScreenHeight);
            this.groupBox3.Controls.Add(this.btnSetDefault);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtScreenWidth);
            this.groupBox3.Controls.Add(this.txtTitle);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 188);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "游戏参数";
            // 
            // cbbPlayer
            // 
            this.cbbPlayer.FormattingEnabled = true;
            this.cbbPlayer.Location = new System.Drawing.Point(80, 155);
            this.cbbPlayer.Name = "cbbPlayer";
            this.cbbPlayer.Size = new System.Drawing.Size(129, 20);
            this.cbbPlayer.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "当前角色：";
            // 
            // cbbGameType
            // 
            this.cbbGameType.FormattingEnabled = true;
            this.cbbGameType.Items.AddRange(new object[] {
            "Top-down",
            "Side-scrolling"});
            this.cbbGameType.Location = new System.Drawing.Point(79, 129);
            this.cbbGameType.Name = "cbbGameType";
            this.cbbGameType.Size = new System.Drawing.Size(129, 20);
            this.cbbGameType.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "游戏类型：";
            // 
            // txtTargetFps
            // 
            this.txtTargetFps.Location = new System.Drawing.Point(79, 102);
            this.txtTargetFps.Name = "txtTargetFps";
            this.txtTargetFps.Size = new System.Drawing.Size(60, 21);
            this.txtTargetFps.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(146, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "像素";
            // 
            // txtScreenHeight
            // 
            this.txtScreenHeight.Location = new System.Drawing.Point(80, 76);
            this.txtScreenHeight.Name = "txtScreenHeight";
            this.txtScreenHeight.Size = new System.Drawing.Size(60, 21);
            this.txtScreenHeight.TabIndex = 10;
            // 
            // btnSetDefault
            // 
            this.btnSetDefault.Location = new System.Drawing.Point(308, 22);
            this.btnSetDefault.Name = "btnSetDefault";
            this.btnSetDefault.Size = new System.Drawing.Size(85, 23);
            this.btnSetDefault.TabIndex = 6;
            this.btnSetDefault.Text = "使用默认值";
            this.btnSetDefault.UseVisualStyleBackColor = true;
            this.btnSetDefault.Click += new System.EventHandler(this.btnSetDefault_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "屏幕高度：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(146, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "像素";
            // 
            // txtScreenWidth
            // 
            this.txtScreenWidth.Location = new System.Drawing.Point(80, 50);
            this.txtScreenWidth.Name = "txtScreenWidth";
            this.txtScreenWidth.Size = new System.Drawing.Size(60, 21);
            this.txtScreenWidth.TabIndex = 6;
            // 
            // txtTitle
            // 
            this.txtTitle.ForeColor = System.Drawing.Color.Gray;
            this.txtTitle.Location = new System.Drawing.Point(80, 24);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PromptText = "请输入游戏标题";
            this.txtTitle.Size = new System.Drawing.Size(193, 21);
            this.txtTitle.TabIndex = 3;
            this.txtTitle.Text = "请输入游戏标题";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "帧速率：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "屏幕宽度：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(19, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "游戏标题：";
            // 
            // GameSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Name = "GameSettingPage";
            this.Size = new System.Drawing.Size(403, 196);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTargetFps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScreenHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtScreenWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown txtTargetFps;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown txtScreenHeight;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown txtScreenWidth;
        private Controls.ControlsEx.PromptTextBox txtTitle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cbbGameType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbbPlayer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetDefault;
    }
}
