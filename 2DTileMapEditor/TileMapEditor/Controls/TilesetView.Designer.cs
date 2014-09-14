namespace TileMapEditor.Controls
{
    partial class TilesetView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetView));
            this.cbbTileset = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_Pos = new System.Windows.Forms.ToolStripStatusLabel();
            this.status_offset = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenu_AddTileset = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu_DelAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbbTileset
            // 
            this.cbbTileset.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbbTileset.FormattingEnabled = true;
            this.cbbTileset.Location = new System.Drawing.Point(0, 0);
            this.cbbTileset.Name = "cbbTileset";
            this.cbbTileset.Size = new System.Drawing.Size(280, 20);
            this.cbbTileset.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.status_Pos,
            this.status_offset});
            this.statusStrip1.Location = new System.Drawing.Point(0, 363);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(280, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel1.Text = " 静态图素：";
            // 
            // status_Pos
            // 
            this.status_Pos.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.status_Pos.Name = "status_Pos";
            this.status_Pos.Size = new System.Drawing.Size(97, 17);
            this.status_Pos.Spring = true;
            this.status_Pos.Text = "(0,0)  (0,0)";
            this.status_Pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // status_offset
            // 
            this.status_offset.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.status_offset.Name = "status_offset";
            this.status_offset.Size = new System.Drawing.Size(97, 17);
            this.status_offset.Spring = true;
            this.status_offset.Text = "Offset:0";
            this.status_offset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(280, 343);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(272, 315);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "静态图素面板";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenu_AddTileset,
            this.contextMenu_Del,
            this.contextMenu_DelAll,
            this.toolStripSeparator1,
            this.取消ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 98);
            // 
            // contextMenu_AddTileset
            // 
            this.contextMenu_AddTileset.Image = ((System.Drawing.Image)(resources.GetObject("contextMenu_AddTileset.Image")));
            this.contextMenu_AddTileset.Name = "contextMenu_AddTileset";
            this.contextMenu_AddTileset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.contextMenu_AddTileset.Size = new System.Drawing.Size(176, 22);
            this.contextMenu_AddTileset.Text = "AddTileset...";
            this.contextMenu_AddTileset.Click += new System.EventHandler(this.contextMenu_AddTileset_Click);
            // 
            // contextMenu_Del
            // 
            this.contextMenu_Del.Image = ((System.Drawing.Image)(resources.GetObject("contextMenu_Del.Image")));
            this.contextMenu_Del.Name = "contextMenu_Del";
            this.contextMenu_Del.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.contextMenu_Del.Size = new System.Drawing.Size(176, 22);
            this.contextMenu_Del.Text = "删除(&D)";
            this.contextMenu_Del.Click += new System.EventHandler(this.contextMenu_Del_Click);
            // 
            // contextMenu_DelAll
            // 
            this.contextMenu_DelAll.Image = ((System.Drawing.Image)(resources.GetObject("contextMenu_DelAll.Image")));
            this.contextMenu_DelAll.Name = "contextMenu_DelAll";
            this.contextMenu_DelAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.contextMenu_DelAll.Size = new System.Drawing.Size(176, 22);
            this.contextMenu_DelAll.Text = "清空(&A)";
            this.contextMenu_DelAll.Click += new System.EventHandler(this.contextMenu_DelAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.取消ToolStripMenuItem.Text = "取消(&C)";
            // 
            // ctl_TilesetCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cbbTileset);
            this.Name = "ctl_TilesetCanvas";
            this.Size = new System.Drawing.Size(280, 385);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbTileset;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_AddTileset;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Del;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel status_Pos;
        private System.Windows.Forms.ToolStripStatusLabel status_offset;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_DelAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
