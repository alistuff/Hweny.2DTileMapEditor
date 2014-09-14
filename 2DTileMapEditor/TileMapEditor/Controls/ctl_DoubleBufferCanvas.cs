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
    public partial class ctl_DoubleBufferCanvas : UserControl
    {
        private IRenderer _renderer;

        public ctl_DoubleBufferCanvas()
        {
            InitializeComponent();

            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }

        public void SetRenderer(IRenderer renderer)
        {
            _renderer = renderer;
            _renderer.RendererUpdate += new RendererEventHandler(_renderer_RendererUpdate);
        }

        void _renderer_RendererUpdate(RendererArgs e)
        {
            if (e.ClientRegion == Rectangle.Empty)
                this.Invalidate();
            else
                this.Invalidate(e.ClientRegion);
        }

        public void SetOffset(int x, int y)
        {
            _renderer.SetOffset(x,y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_renderer != null)
            {
                _renderer.Render(e);
            }
        }
    }
}
