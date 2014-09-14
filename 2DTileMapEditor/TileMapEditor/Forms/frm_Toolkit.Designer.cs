namespace TileMapEditor.Forms
{
    partial class frm_Toolkit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Toolkit));
            this.toolItem_Pointer = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Pen = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Rectangle = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Fill = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Eraser = new System.Windows.Forms.ToolStripButton();
            this.toolItem_ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolItem_ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Grid = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolItem_SelectedBox = new System.Windows.Forms.ToolStripButton();
            this.toolItem_Highlight = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolItem_Pointer
            // 
            this.toolItem_Pointer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Pointer.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Pointer.Image")));
            this.toolItem_Pointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Pointer.Name = "toolItem_Pointer";
            this.toolItem_Pointer.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Pointer.Text = "指针";
            this.toolItem_Pointer.ToolTipText = "指针";
            this.toolItem_Pointer.Click += new System.EventHandler(this.toolItem_Pointer_Click);
            // 
            // toolItem_Pen
            // 
            this.toolItem_Pen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Pen.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Pen.Image")));
            this.toolItem_Pen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Pen.Name = "toolItem_Pen";
            this.toolItem_Pen.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Pen.Text = "铅笔";
            this.toolItem_Pen.ToolTipText = "铅笔";
            this.toolItem_Pen.Click += new System.EventHandler(this.toolItem_Pen_Click);
            // 
            // toolItem_Rectangle
            // 
            this.toolItem_Rectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Rectangle.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Rectangle.Image")));
            this.toolItem_Rectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Rectangle.Name = "toolItem_Rectangle";
            this.toolItem_Rectangle.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Rectangle.Text = "矩形";
            this.toolItem_Rectangle.ToolTipText = "矩形";
            this.toolItem_Rectangle.Click += new System.EventHandler(this.toolItem_Rectangle_Click);
            // 
            // toolItem_Fill
            // 
            this.toolItem_Fill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Fill.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Fill.Image")));
            this.toolItem_Fill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Fill.Name = "toolItem_Fill";
            this.toolItem_Fill.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Fill.Text = "填充";
            this.toolItem_Fill.ToolTipText = "填充";
            this.toolItem_Fill.Click += new System.EventHandler(this.toolItem_Fill_Click);
            // 
            // toolItem_Eraser
            // 
            this.toolItem_Eraser.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Eraser.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Eraser.Image")));
            this.toolItem_Eraser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Eraser.Name = "toolItem_Eraser";
            this.toolItem_Eraser.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Eraser.Text = "橡皮擦";
            this.toolItem_Eraser.ToolTipText = "橡皮擦";
            this.toolItem_Eraser.Click += new System.EventHandler(this.toolItem_Eraser_Click);
            // 
            // toolItem_ZoomIn
            // 
            this.toolItem_ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_ZoomIn.Image")));
            this.toolItem_ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_ZoomIn.Name = "toolItem_ZoomIn";
            this.toolItem_ZoomIn.Size = new System.Drawing.Size(28, 28);
            this.toolItem_ZoomIn.Text = "放大";
            this.toolItem_ZoomIn.ToolTipText = "放大";
            this.toolItem_ZoomIn.Click += new System.EventHandler(this.toolItem_ZoomIn_Click);
            // 
            // toolItem_ZoomOut
            // 
            this.toolItem_ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_ZoomOut.Image")));
            this.toolItem_ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_ZoomOut.Name = "toolItem_ZoomOut";
            this.toolItem_ZoomOut.Size = new System.Drawing.Size(28, 28);
            this.toolItem_ZoomOut.Text = "缩小";
            this.toolItem_ZoomOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolItem_ZoomOut.ToolTipText = "缩小";
            this.toolItem_ZoomOut.Click += new System.EventHandler(this.toolItem_ZoomOut_Click);
            // 
            // toolItem_Grid
            // 
            this.toolItem_Grid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Grid.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Grid.Image")));
            this.toolItem_Grid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Grid.Name = "toolItem_Grid";
            this.toolItem_Grid.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Grid.Text = "网格";
            this.toolItem_Grid.ToolTipText = "网格";
            this.toolItem_Grid.Click += new System.EventHandler(this.toolItem_Grid_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolItem_Pointer,
            this.toolStripButton2,
            this.toolItem_SelectedBox,
            this.toolItem_Pen,
            this.toolItem_Rectangle,
            this.toolItem_Fill,
            this.toolItem_Eraser,
            this.toolItem_Highlight,
            this.toolItem_ZoomIn,
            this.toolItem_ZoomOut,
            this.toolItem_Grid});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(8, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(65, 308);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.AutoToolTip = false;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 4);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolItem_SelectedBox
            // 
            this.toolItem_SelectedBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_SelectedBox.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_SelectedBox.Image")));
            this.toolItem_SelectedBox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_SelectedBox.Name = "toolItem_SelectedBox";
            this.toolItem_SelectedBox.Size = new System.Drawing.Size(28, 28);
            this.toolItem_SelectedBox.Text = "toolStripButton1";
            this.toolItem_SelectedBox.ToolTipText = "选择框";
            this.toolItem_SelectedBox.Click += new System.EventHandler(this.toolItem_SelectedBox_Click);
            // 
            // toolItem_Highlight
            // 
            this.toolItem_Highlight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolItem_Highlight.Image = ((System.Drawing.Image)(resources.GetObject("toolItem_Highlight.Image")));
            this.toolItem_Highlight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolItem_Highlight.Name = "toolItem_Highlight";
            this.toolItem_Highlight.Size = new System.Drawing.Size(28, 28);
            this.toolItem_Highlight.Text = "高亮";
            this.toolItem_Highlight.ToolTipText = "高亮";
            this.toolItem_Highlight.Click += new System.EventHandler(this.toolItem_Highlight_Click);
            // 
            // frm_Toolkit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(73, 308);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_Toolkit";
            this.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "工具箱";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripButton toolItem_Pointer;
        private System.Windows.Forms.ToolStripButton toolItem_Pen;
        private System.Windows.Forms.ToolStripButton toolItem_Rectangle;
        private System.Windows.Forms.ToolStripButton toolItem_Fill;
        private System.Windows.Forms.ToolStripButton toolItem_Eraser;
        private System.Windows.Forms.ToolStripButton toolItem_ZoomIn;
        private System.Windows.Forms.ToolStripButton toolItem_ZoomOut;
        private System.Windows.Forms.ToolStripButton toolItem_Grid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolItem_Highlight;
        private System.Windows.Forms.ToolStripButton toolItem_SelectedBox;
        private System.Windows.Forms.ToolStripButton toolStripButton2;

    }
}