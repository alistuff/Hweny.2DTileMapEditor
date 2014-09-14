namespace TileMapEditor.Controls
{
    partial class EditableMapView
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
            this.contextMenu_SelectBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolItem_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItem_Paster = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItem_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItem_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolItem_Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenu_SelectBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu_SelectBox
            // 
            this.contextMenu_SelectBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItem_Cut,
            this.toolItem_Copy,
            this.toolItem_Paster,
            this.toolItem_Del,
            this.toolStripSeparator1,
            this.toolItem_SelectAll,
            this.toolItem_Cancel});
            this.contextMenu_SelectBox.Name = "contextMenu_SelectBox";
            this.contextMenu_SelectBox.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenu_SelectBox.Size = new System.Drawing.Size(153, 164);
            // 
            // toolItem_Cut
            // 
            this.toolItem_Cut.Name = "toolItem_Cut";
            this.toolItem_Cut.Size = new System.Drawing.Size(152, 22);
            this.toolItem_Cut.Text = "剪切(&C)";
            // 
            // toolItem_Copy
            // 
            this.toolItem_Copy.Name = "toolItem_Copy";
            this.toolItem_Copy.Size = new System.Drawing.Size(152, 22);
            this.toolItem_Copy.Text = "复制(&C)";
            // 
            // toolItem_Paster
            // 
            this.toolItem_Paster.Name = "toolItem_Paster";
            this.toolItem_Paster.Size = new System.Drawing.Size(152, 22);
            this.toolItem_Paster.Text = "粘贴(&P)";
            // 
            // toolItem_Del
            // 
            this.toolItem_Del.Name = "toolItem_Del";
            this.toolItem_Del.Size = new System.Drawing.Size(152, 22);
            this.toolItem_Del.Text = "删除(&D)";
            // 
            // toolItem_SelectAll
            // 
            this.toolItem_SelectAll.Name = "toolItem_SelectAll";
            this.toolItem_SelectAll.Size = new System.Drawing.Size(152, 22);
            this.toolItem_SelectAll.Text = "全选(&A)";
            // 
            // toolItem_Cancel
            // 
            this.toolItem_Cancel.Name = "toolItem_Cancel";
            this.toolItem_Cancel.Size = new System.Drawing.Size(152, 22);
            this.toolItem_Cancel.Text = "取消(&U)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // ctl_EditableMapCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ctl_EditableMapCanvas";
            this.contextMenu_SelectBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu_SelectBox;
        private System.Windows.Forms.ToolStripMenuItem toolItem_Cut;
        private System.Windows.Forms.ToolStripMenuItem toolItem_Copy;
        private System.Windows.Forms.ToolStripMenuItem toolItem_Paster;
        private System.Windows.Forms.ToolStripMenuItem toolItem_Del;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolItem_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem toolItem_Cancel;
    }
}
