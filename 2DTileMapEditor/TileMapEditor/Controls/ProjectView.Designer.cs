namespace TileMapEditor.Controls
{
    partial class ProjectView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectView));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenu_NewMap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenu_EditMap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenu_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenu_NewMap,
            this.toolStripSeparator3,
            this.contextMenu_EditMap,
            this.toolStripSeparator1,
            this.contextMenu_Del});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(157, 116);
            // 
            // contextMenu_NewMap
            // 
            this.contextMenu_NewMap.Image = ((System.Drawing.Image)(resources.GetObject("contextMenu_NewMap.Image")));
            this.contextMenu_NewMap.Name = "contextMenu_NewMap";
            this.contextMenu_NewMap.Size = new System.Drawing.Size(156, 26);
            this.contextMenu_NewMap.Text = "新建地图";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // contextMenu_EditMap
            // 
            this.contextMenu_EditMap.Image = ((System.Drawing.Image)(resources.GetObject("contextMenu_EditMap.Image")));
            this.contextMenu_EditMap.Name = "contextMenu_EditMap";
            this.contextMenu_EditMap.Size = new System.Drawing.Size(156, 26);
            this.contextMenu_EditMap.Text = "编辑地图";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // contextMenu_Del
            // 
            this.contextMenu_Del.Image = ((System.Drawing.Image)(resources.GetObject("contextMenu_Del.Image")));
            this.contextMenu_Del.Name = "contextMenu_Del";
            this.contextMenu_Del.Size = new System.Drawing.Size(156, 26);
            this.contextMenu_Del.Text = "删除地图";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "games_folder.png");
            this.imageList1.Images.SetKeyName(1, "new_document.png");
            this.imageList1.Images.SetKeyName(2, "check.png");
            // 
            // ctl_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ctl_Project";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_NewMap;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_EditMap;
        private System.Windows.Forms.ToolStripMenuItem contextMenu_Del;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ImageList imageList1;

    }
}
