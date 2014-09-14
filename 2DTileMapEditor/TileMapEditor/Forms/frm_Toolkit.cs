using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TileMapEditor.Forms
{
    public partial class frm_Toolkit : Form
    {
        public event EventHandler Toolkit_Pointer;
        public event EventHandler Toolkit_SelectedBox;
        public event EventHandler Toolkit_Pen;
        public event EventHandler Toolkit_Rectangle;
        public event EventHandler Toolkit_Fill;
        public event EventHandler Toolkit_Eraser;
        public event EventHandler Toolkit_ZoomIn;
        public event EventHandler Toolkit_ZoomOut;
        public event EventHandler Toolkit_Grid;
        public event EventHandler Toolkit_Highlihgt;
        public event EventHandler Toolkit_UnActivate;

        public bool Pointer
        {
            set { toolItem_Pointer.Checked = value; }
        }
        public bool SelectedBox
        {
            set { toolItem_SelectedBox.Checked = value; }
        }
        public bool Pen
        {
            set { toolItem_Pen.Checked = value; }
        }
        public bool Rectangle
        {
            set { toolItem_Rectangle.Checked = value; }
        }
        public bool Fill
        {
            set { toolItem_Fill.Checked = value; }
        }
        public bool Eraser
        {
            set { toolItem_Eraser.Checked = value; }
        }
        public bool ZoomIn
        {
            set { toolItem_ZoomIn.Checked = value; }
        }
        public bool ZoomOut
        {
            set { toolItem_ZoomOut.Checked = value; }
        }
        public bool Grid
        {
            set { toolItem_Grid.Checked = value; }
        }
        public bool Highlight
        {
            set { toolItem_Highlight.Checked = value; }
        }

        public frm_Toolkit()
        {
            InitializeComponent();

            this.toolStrip1.MouseEnter += new EventHandler(toolStrip1_MouseEnter);
            this.toolStrip1.MouseLeave += new EventHandler(toolStrip1_MouseLeave);
            FormClosing += new FormClosingEventHandler(frm_Toolkit_FormClosing);
        }

        #region Toolkit Events

        private void toolItem_Pointer_Click(object sender, EventArgs e)
        {
            if (Toolkit_Pointer != null)
                Toolkit_Pointer(sender, e);
        }

        private void toolItem_SelectedBox_Click(object sender, EventArgs e)
        {
            if (Toolkit_SelectedBox != null)
                Toolkit_SelectedBox(sender,e);
        }

        private void toolItem_Pen_Click(object sender, EventArgs e)
        {
            if (Toolkit_Pen != null)
                Toolkit_Pen(sender,e);
        }

        private void toolItem_Rectangle_Click(object sender, EventArgs e)
        {
            if (Toolkit_Rectangle != null)
                Toolkit_Rectangle(sender,e);
        }

        private void toolItem_Fill_Click(object sender, EventArgs e)
        {
            if (Toolkit_Fill != null)
                Toolkit_Fill(sender,e);
        }

        private void toolItem_Eraser_Click(object sender, EventArgs e)
        {
            if (Toolkit_Eraser != null)
                Toolkit_Eraser(sender,e);
        }

        private void toolItem_ZoomIn_Click(object sender, EventArgs e)
        {
            if (Toolkit_ZoomIn != null)
                Toolkit_ZoomIn(sender,e);
        }

        private void toolItem_ZoomOut_Click(object sender, EventArgs e)
        {
            if (Toolkit_ZoomOut != null)
                Toolkit_ZoomOut(sender,e);
        }

        private void toolItem_Grid_Click(object sender, EventArgs e)
        {
            if (Toolkit_Grid != null)
                Toolkit_Grid(sender,e);
        }

        private void toolItem_Highlight_Click(object sender, EventArgs e)
        {
            if (Toolkit_Highlihgt != null)
                Toolkit_Highlihgt(sender,e);
        }

        private void toolStrip1_MouseEnter(object sender, EventArgs e)
        {
            this.Activate();
        }

        private void toolStrip1_MouseLeave(object sender, EventArgs e)
        {
            if (Toolkit_UnActivate != null)
                Toolkit_UnActivate(sender, e);
        }

        private void frm_Toolkit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        #endregion
    }
}
