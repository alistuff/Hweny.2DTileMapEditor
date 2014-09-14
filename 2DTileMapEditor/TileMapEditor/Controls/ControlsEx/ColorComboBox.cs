using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TileMapEditor.Controls.ControlsEx
{
    public partial class ColorComboBox : UserControl
    {
        public ColorComboBox()
        {
            InitializeComponent();

            PopulateItems();
            cbbColor.DrawMode = DrawMode.OwnerDrawFixed;
            cbbColor.DrawItem += new DrawItemEventHandler(cbbColor_DrawItem);
            cbbColor.SelectedIndexChanged += new EventHandler(cbbColor_SelectedIndexChanged);
            cbbColor.SelectedIndex = 0;
        }

        void cbbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.BackColor = SelectedItem;
        }

        public new string Text
        {
            get { return cbbColor.Text; }
            set { cbbColor.Text = value; }
        }

        public Color SelectedItem
        {
            get
            {
                return (Color)cbbColor.SelectedItem;
            }
            set { cbbColor.SelectedItem = value; }
        }

        public int SelectedIndex
        {
            get { return cbbColor.SelectedIndex; }
            set { cbbColor.SelectedIndex = value; }
        }

        private void PopulateItems()
        {
            string[] colors = Enum.GetNames(typeof(KnownColor));
            cbbColor.Items.Clear();
            foreach (string c in colors)
            {
                Color color = Color.FromName(c);
                if (!color.IsSystemColor && color != Color.Transparent)
                {
                    cbbColor.Items.Add(color);
                }
            }
        }

        private void cbbColor_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Color color = (Color)cbbColor.Items[e.Index];
            Point pt = new Point(e.Bounds.X, e.Bounds.Y);

            Brush brush = new SolidBrush(SystemColors.Window);
            g.FillRectangle(brush, e.Bounds);

            Color textColor = SystemColors.ControlText;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                brush = new SolidBrush(SystemColors.Highlight);
                g.FillRectangle(brush, e.Bounds);
                textColor = SystemColors.HighlightText;
            }

            Pen pen = new Pen(Color.Black);
            g.DrawRectangle(pen, pt.X + 1, pt.Y + 1, 18, 12);
            brush = new SolidBrush(color);
            g.FillRectangle(brush, pt.X + 2, pt.Y + 2, 17, 11);

            brush = new SolidBrush(textColor);
            g.DrawString(color.Name, Font, brush, pt.X + 20, pt.Y + 2);

            brush.Dispose();
            brush = null;
            pen.Dispose();
            pen = null;
        }
    }
}
