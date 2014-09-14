using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapRenderer;

namespace TileMapEditor.Controls
{
    public partial class ctl_ScrollCanvas : UserControl
    {
        public event ScrollCanvasEventHandler SrollCavasUpdate;
        private int _offsetX;
        private int _offsetY;
        private int _largeChangeX;
        private int _largeChangeY;
        private int _contentWidth;
        private int _contentHeight;

        public ctl_DoubleBufferCanvas Canvas
        {
            get { return _canvas; }
        }

        public ctl_ScrollCanvas()
        {
            InitializeComponent();

            _canvas.SizeChanged += new EventHandler(_canvas_SizeChanged);
            hScrollBar1.ValueChanged += new EventHandler(hScrollBar1_ValueChanged);
            vScrollBar1.ValueChanged += new EventHandler(vScrollBar1_ValueChanged);
            hScrollBar1.MouseEnter += new EventHandler(hScrollBar1_MouseEnter);
            vScrollBar1.MouseEnter += new EventHandler(vScrollBar1_MouseEnter);
        }

        public void SetRenderer(IRenderer renderer)
        {
            _canvas.SetRenderer(renderer);
        }

        public void SetScrollCanvasContext(int contentWidth, int contentHeight,
            int largeChangeX, int largeChangeY)
        {
            _contentWidth = contentWidth;
            _contentHeight = contentHeight;
            _largeChangeX = largeChangeX;
            _largeChangeY = largeChangeY;
            hScrollBar1.LargeChange = largeChangeX;
            vScrollBar1.LargeChange = largeChangeY;

            updateScrollCanvas();
        }

        public void SetOffset(int x, int y)
        {
            if (x < 0)
                x = 0;
            else if (x > hScrollBar1.Maximum)
                x = hScrollBar1.Maximum;
            if (y < 0)
                y = 0;
            else if (y > vScrollBar1.Maximum)
                y = vScrollBar1.Maximum;

            hScrollBar1.Value = x;
            vScrollBar1.Value = y;
        }

        public Size GetOffset()
        {
            return new Size(hScrollBar1.Value, vScrollBar1.Value);
        }

        private void vScrollBar1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void hScrollBar1_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void _canvas_SizeChanged(object sender, EventArgs e)
        {
            updateScrollCanvas();
        }

        private void vScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            _offsetY = vScrollBar1.Value;
            _canvas.SetOffset(_offsetX, _offsetY);
            onSrollCavasUpdate(new ScrollCanvasEventArgs(_canvas.ClientRectangle, _offsetX, _offsetY));
            Application.DoEvents();
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            _offsetX = hScrollBar1.Value;
            _canvas.SetOffset(_offsetX, _offsetY);
            onSrollCavasUpdate(new ScrollCanvasEventArgs(_canvas.ClientRectangle, _offsetX, _offsetY));
            Application.DoEvents();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (vScrollBar1.Enabled)
            {
                int newvalue = vScrollBar1.Value - e.Delta;
                if (newvalue < vScrollBar1.Minimum)
                    newvalue = vScrollBar1.Minimum;
                if (newvalue > vScrollBar1.Maximum)
                    newvalue = vScrollBar1.Maximum - vScrollBar1.LargeChange + 1;
                vScrollBar1.Value = newvalue;
                _canvas.SetOffset(_offsetX, vScrollBar1.Value);
                Application.DoEvents();
            }
        }

        private void updateScrollCanvas()
        {
            if (_contentWidth >= _canvas.Width)
            {
                hScrollBar1.Maximum = _contentWidth - _canvas.Width + _largeChangeX;
                hScrollBar1.Enabled = true;
            }
            else
            {
                _offsetX = 0;
                hScrollBar1.Enabled = false;
            }

            if (_contentHeight >= _canvas.Height)
            {
                vScrollBar1.Maximum = _contentHeight - _canvas.Height + _largeChangeY;
                vScrollBar1.Enabled = true;
            }
            else
            {
                _offsetY = 0;
                vScrollBar1.Enabled = false;
            }

            if (_offsetY == 0 || _offsetY + _canvas.Height < _contentHeight)
                _canvas.SetOffset(_offsetX, _offsetY);
            else
                _canvas.SetOffset(_offsetX, _offsetY - _largeChangeY + 1);

            onSrollCavasUpdate(new ScrollCanvasEventArgs(_canvas.ClientRectangle, _offsetX, _offsetY));
        }

        private void onSrollCavasUpdate(ScrollCanvasEventArgs e)
        {
            ScrollCanvasEventHandler temp = SrollCavasUpdate;
            if (temp != null)
                temp(this,e);
        }
    }
}
