namespace TileMapEditor.Test.Setting
{
    partial class PlayerSettingPage
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbRowSplit = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.lsvAnimations = new System.Windows.Forms.ListView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSelectCharSet = new System.Windows.Forms.Button();
            this.txtSpriteSheet = new TileMapEditor.Controls.ControlsEx.PromptTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new TileMapEditor.Controls.ControlsEx.PromptTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtASpeed = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCollisionHeight = new System.Windows.Forms.NumericUpDown();
            this.txtCollisionWidth = new System.Windows.Forms.NumericUpDown();
            this.txtCollisionY = new System.Windows.Forms.NumericUpDown();
            this.txtCollisionX = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lsbPlayers = new System.Windows.Forms.ListBox();
            this.picSpriteSheet = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtASpeed)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionX)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSpriteSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 395);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角色参数";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbRowSplit);
            this.groupBox6.Controls.Add(this.btnEdit);
            this.groupBox6.Controls.Add(this.lsvAnimations);
            this.groupBox6.Location = new System.Drawing.Point(113, 232);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(281, 159);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "动画设置";
            // 
            // cbRowSplit
            // 
            this.cbRowSplit.AutoSize = true;
            this.cbRowSplit.Checked = true;
            this.cbRowSplit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRowSplit.Location = new System.Drawing.Point(179, 136);
            this.cbRowSplit.Name = "cbRowSplit";
            this.cbRowSplit.Size = new System.Drawing.Size(96, 16);
            this.cbRowSplit.TabIndex = 3;
            this.cbRowSplit.Text = "行创建动画帧";
            this.toolTip1.SetToolTip(this.cbRowSplit, "提示：\r\n勾选此项从精灵表单“行”创建动画帧\r\n不勾选则从精灵表单“列”创建动画帧");
            this.cbRowSplit.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(6, 132);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(45, 23);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // lsvAnimations
            // 
            this.lsvAnimations.Location = new System.Drawing.Point(7, 17);
            this.lsvAnimations.Name = "lsvAnimations";
            this.lsvAnimations.Size = new System.Drawing.Size(268, 112);
            this.lsvAnimations.TabIndex = 0;
            this.lsvAnimations.UseCompatibleStateImageBehavior = false;
            this.lsvAnimations.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lsvAnimations_ColumnWidthChanging);
            this.lsvAnimations.SelectedIndexChanged += new System.EventHandler(this.lsvAnimations_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSelectCharSet);
            this.groupBox5.Controls.Add(this.txtSpriteSheet);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtName);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtHeight);
            this.groupBox5.Controls.Add(this.txtWidth);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txtMaxSpeed);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.txtASpeed);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(113, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(281, 125);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "基本属性";
            // 
            // btnSelectCharSet
            // 
            this.btnSelectCharSet.Location = new System.Drawing.Point(230, 43);
            this.btnSelectCharSet.Name = "btnSelectCharSet";
            this.btnSelectCharSet.Size = new System.Drawing.Size(40, 23);
            this.btnSelectCharSet.TabIndex = 20;
            this.btnSelectCharSet.Text = "浏览";
            this.btnSelectCharSet.UseVisualStyleBackColor = true;
            this.btnSelectCharSet.Click += new System.EventHandler(this.btnSelectCharSet_Click);
            // 
            // txtSpriteSheet
            // 
            this.txtSpriteSheet.ForeColor = System.Drawing.Color.Gray;
            this.txtSpriteSheet.Location = new System.Drawing.Point(79, 44);
            this.txtSpriteSheet.Name = "txtSpriteSheet";
            this.txtSpriteSheet.PromptText = "请选择角色图像文件";
            this.txtSpriteSheet.ReadOnly = true;
            this.txtSpriteSheet.Size = new System.Drawing.Size(149, 21);
            this.txtSpriteSheet.TabIndex = 19;
            this.txtSpriteSheet.Text = "请选择角色图像文件";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(39, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "图像：";
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.Color.Gray;
            this.txtName.Location = new System.Drawing.Point(79, 17);
            this.txtName.Name = "txtName";
            this.txtName.PromptText = "请输入名称";
            this.txtName.Size = new System.Drawing.Size(118, 21);
            this.txtName.TabIndex = 17;
            this.txtName.Text = "请输入名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "角色名称：";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(208, 70);
            this.txtHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(57, 21);
            this.txtHeight.TabIndex = 11;
            this.txtHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(79, 70);
            this.txtWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(60, 21);
            this.txtWidth.TabIndex = 10;
            this.txtWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "角色宽度：";
            // 
            // txtMaxSpeed
            // 
            this.txtMaxSpeed.Location = new System.Drawing.Point(208, 95);
            this.txtMaxSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtMaxSpeed.Name = "txtMaxSpeed";
            this.txtMaxSpeed.Size = new System.Drawing.Size(57, 21);
            this.txtMaxSpeed.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "角色高度：";
            // 
            // txtASpeed
            // 
            this.txtASpeed.Location = new System.Drawing.Point(79, 95);
            this.txtASpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtASpeed.Name = "txtASpeed";
            this.txtASpeed.Size = new System.Drawing.Size(60, 21);
            this.txtASpeed.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "最大速度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "加速度：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCollisionHeight);
            this.groupBox4.Controls.Add(this.txtCollisionWidth);
            this.groupBox4.Controls.Add(this.txtCollisionY);
            this.groupBox4.Controls.Add(this.txtCollisionX);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(113, 154);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(282, 67);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "碰撞边框";
            // 
            // txtCollisionHeight
            // 
            this.txtCollisionHeight.Location = new System.Drawing.Point(209, 37);
            this.txtCollisionHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtCollisionHeight.Name = "txtCollisionHeight";
            this.txtCollisionHeight.Size = new System.Drawing.Size(60, 21);
            this.txtCollisionHeight.TabIndex = 20;
            // 
            // txtCollisionWidth
            // 
            this.txtCollisionWidth.Location = new System.Drawing.Point(209, 13);
            this.txtCollisionWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txtCollisionWidth.Name = "txtCollisionWidth";
            this.txtCollisionWidth.Size = new System.Drawing.Size(60, 21);
            this.txtCollisionWidth.TabIndex = 18;
            // 
            // txtCollisionY
            // 
            this.txtCollisionY.Location = new System.Drawing.Point(80, 37);
            this.txtCollisionY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtCollisionY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.txtCollisionY.Name = "txtCollisionY";
            this.txtCollisionY.Size = new System.Drawing.Size(60, 21);
            this.txtCollisionY.TabIndex = 19;
            // 
            // txtCollisionX
            // 
            this.txtCollisionX.Location = new System.Drawing.Point(80, 13);
            this.txtCollisionX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtCollisionX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.txtCollisionX.Name = "txtCollisionX";
            this.txtCollisionX.Size = new System.Drawing.Size(60, 21);
            this.txtCollisionX.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 24;
            this.label9.Text = "高度：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(174, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "宽度：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "Y偏移：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "X偏移：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDel);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.lsbPlayers);
            this.groupBox2.Controls.Add(this.picSpriteSheet);
            this.groupBox2.Location = new System.Drawing.Point(8, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(101, 378);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(52, 246);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(45, 23);
            this.btnDel.TabIndex = 5;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(5, 246);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(45, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lsbPlayers
            // 
            this.lsbPlayers.FormattingEnabled = true;
            this.lsbPlayers.ItemHeight = 12;
            this.lsbPlayers.Location = new System.Drawing.Point(6, 13);
            this.lsbPlayers.Name = "lsbPlayers";
            this.lsbPlayers.Size = new System.Drawing.Size(89, 232);
            this.lsbPlayers.TabIndex = 3;
            this.lsbPlayers.SelectedIndexChanged += new System.EventHandler(this.lsbPlayers_SelectedIndexChanged);
            // 
            // picSpriteSheet
            // 
            this.picSpriteSheet.BackColor = System.Drawing.Color.Lime;
            this.picSpriteSheet.BackgroundImage = global::TileMapEditor.Properties.Resources.transparent;
            this.picSpriteSheet.Location = new System.Drawing.Point(6, 270);
            this.picSpriteSheet.Name = "picSpriteSheet";
            this.picSpriteSheet.Size = new System.Drawing.Size(89, 104);
            this.picSpriteSheet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSpriteSheet.TabIndex = 2;
            this.picSpriteSheet.TabStop = false;
            // 
            // PlayerSettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "PlayerSettingPage";
            this.Size = new System.Drawing.Size(403, 399);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtASpeed)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCollisionX)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSpriteSheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picSpriteSheet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lsbPlayers;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox cbRowSplit;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.ListView lsvAnimations;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown txtHeight;
        private System.Windows.Forms.NumericUpDown txtWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtMaxSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtASpeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown txtCollisionHeight;
        private System.Windows.Forms.NumericUpDown txtCollisionWidth;
        private System.Windows.Forms.NumericUpDown txtCollisionY;
        private System.Windows.Forms.NumericUpDown txtCollisionX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSelectCharSet;
        private Controls.ControlsEx.PromptTextBox txtSpriteSheet;
        private System.Windows.Forms.Label label10;
        private Controls.ControlsEx.PromptTextBox txtName;
        private System.Windows.Forms.Label label1;
    }
}
