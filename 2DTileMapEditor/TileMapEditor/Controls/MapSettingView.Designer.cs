namespace TileMapEditor.Controls
{
    partial class MapSettingView
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
            this.cbShowEvent = new System.Windows.Forms.CheckBox();
            this.cbShowBlock = new System.Windows.Forms.CheckBox();
            this.rbtnEditMode = new System.Windows.Forms.RadioButton();
            this.cbShowGrid = new System.Windows.Forms.CheckBox();
            this.rbtnDrawMode = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtnEditEvent = new System.Windows.Forms.RadioButton();
            this.rbtnEditBlock = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageLayer = new System.Windows.Forms.TabPage();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbShowEvent
            // 
            this.cbShowEvent.AutoSize = true;
            this.cbShowEvent.Enabled = false;
            this.cbShowEvent.ForeColor = System.Drawing.Color.Black;
            this.cbShowEvent.Location = new System.Drawing.Point(121, 39);
            this.cbShowEvent.Name = "cbShowEvent";
            this.cbShowEvent.Size = new System.Drawing.Size(96, 16);
            this.cbShowEvent.TabIndex = 0;
            this.cbShowEvent.Text = "显示事件标记";
            this.cbShowEvent.UseVisualStyleBackColor = true;
            // 
            // cbShowBlock
            // 
            this.cbShowBlock.AutoSize = true;
            this.cbShowBlock.ForeColor = System.Drawing.Color.Black;
            this.cbShowBlock.Location = new System.Drawing.Point(121, 21);
            this.cbShowBlock.Name = "cbShowBlock";
            this.cbShowBlock.Size = new System.Drawing.Size(84, 16);
            this.cbShowBlock.TabIndex = 0;
            this.cbShowBlock.Text = "显示障碍物";
            this.cbShowBlock.UseVisualStyleBackColor = true;
            // 
            // rbtnEditMode
            // 
            this.rbtnEditMode.AutoSize = true;
            this.rbtnEditMode.ForeColor = System.Drawing.Color.Black;
            this.rbtnEditMode.Location = new System.Drawing.Point(121, 5);
            this.rbtnEditMode.Name = "rbtnEditMode";
            this.rbtnEditMode.Size = new System.Drawing.Size(71, 16);
            this.rbtnEditMode.TabIndex = 1;
            this.rbtnEditMode.TabStop = true;
            this.rbtnEditMode.Text = "编辑模式";
            this.rbtnEditMode.UseVisualStyleBackColor = true;
            // 
            // cbShowGrid
            // 
            this.cbShowGrid.AutoSize = true;
            this.cbShowGrid.ForeColor = System.Drawing.Color.Black;
            this.cbShowGrid.Location = new System.Drawing.Point(121, 3);
            this.cbShowGrid.Name = "cbShowGrid";
            this.cbShowGrid.Size = new System.Drawing.Size(72, 16);
            this.cbShowGrid.TabIndex = 0;
            this.cbShowGrid.Text = "显示网格";
            this.cbShowGrid.UseVisualStyleBackColor = true;
            // 
            // rbtnDrawMode
            // 
            this.rbtnDrawMode.AutoSize = true;
            this.rbtnDrawMode.ForeColor = System.Drawing.Color.Black;
            this.rbtnDrawMode.Location = new System.Drawing.Point(5, 5);
            this.rbtnDrawMode.Name = "rbtnDrawMode";
            this.rbtnDrawMode.Size = new System.Drawing.Size(71, 16);
            this.rbtnDrawMode.TabIndex = 0;
            this.rbtnDrawMode.TabStop = true;
            this.rbtnDrawMode.Text = "绘图模式";
            this.rbtnDrawMode.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 99);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbtnEditMode);
            this.panel2.Controls.Add(this.rbtnDrawMode);
            this.panel2.Location = new System.Drawing.Point(3, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(233, 27);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cbShowEvent);
            this.panel3.Controls.Add(this.rbtnEditEvent);
            this.panel3.Controls.Add(this.cbShowBlock);
            this.panel3.Controls.Add(this.rbtnEditBlock);
            this.panel3.Controls.Add(this.cbShowGrid);
            this.panel3.Location = new System.Drawing.Point(3, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(233, 58);
            this.panel3.TabIndex = 3;
            // 
            // rbtnEditEvent
            // 
            this.rbtnEditEvent.AutoSize = true;
            this.rbtnEditEvent.Enabled = false;
            this.rbtnEditEvent.ForeColor = System.Drawing.Color.Black;
            this.rbtnEditEvent.Location = new System.Drawing.Point(5, 27);
            this.rbtnEditEvent.Name = "rbtnEditEvent";
            this.rbtnEditEvent.Size = new System.Drawing.Size(71, 16);
            this.rbtnEditEvent.TabIndex = 1;
            this.rbtnEditEvent.TabStop = true;
            this.rbtnEditEvent.Text = "编辑事件";
            this.rbtnEditEvent.UseVisualStyleBackColor = true;
            // 
            // rbtnEditBlock
            // 
            this.rbtnEditBlock.AutoSize = true;
            this.rbtnEditBlock.ForeColor = System.Drawing.Color.Black;
            this.rbtnEditBlock.Location = new System.Drawing.Point(5, 7);
            this.rbtnEditBlock.Name = "rbtnEditBlock";
            this.rbtnEditBlock.Size = new System.Drawing.Size(83, 16);
            this.rbtnEditBlock.TabIndex = 0;
            this.rbtnEditBlock.TabStop = true;
            this.rbtnEditBlock.Text = "编辑障碍物";
            this.rbtnEditBlock.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPageLayer);
            this.tabControl1.Controls.Add(this.tabPageHistory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 99);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(248, 301);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 7;
            // 
            // tabPageLayer
            // 
            this.tabPageLayer.Location = new System.Drawing.Point(4, 25);
            this.tabPageLayer.Name = "tabPageLayer";
            this.tabPageLayer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLayer.Size = new System.Drawing.Size(240, 272);
            this.tabPageLayer.TabIndex = 0;
            this.tabPageLayer.Text = "图层编辑";
            this.tabPageLayer.UseVisualStyleBackColor = true;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Location = new System.Drawing.Point(4, 25);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistory.Size = new System.Drawing.Size(240, 272);
            this.tabPageHistory.TabIndex = 1;
            this.tabPageHistory.Text = "历史记录";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // MapSettingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "MapSettingView";
            this.Size = new System.Drawing.Size(248, 400);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbShowEvent;
        private System.Windows.Forms.CheckBox cbShowBlock;
        private System.Windows.Forms.RadioButton rbtnEditMode;
        private System.Windows.Forms.CheckBox cbShowGrid;
        private System.Windows.Forms.RadioButton rbtnDrawMode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageLayer;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtnEditEvent;
        private System.Windows.Forms.RadioButton rbtnEditBlock;

    }
}
