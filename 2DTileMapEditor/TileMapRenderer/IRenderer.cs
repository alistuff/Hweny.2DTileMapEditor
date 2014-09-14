using System;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public abstract class IRenderer
    {
        protected Color _backColor;
        protected bool _showGrid;
        protected int _offsetX;
        protected int _offsetY;

        public event RendererEventHandler RendererUpdate;

        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; Update(); }
        }

        public bool ShowGrid
        {
            get { return _showGrid; }
            set { _showGrid = value; Update(); }
        }

        public int OffsetX
        {
            get { return _offsetX; }
            set { _offsetX = value; Update(); }
        }

        public int OffsetY
        {
            get { return _offsetY; }
            set { _offsetY = value; Update(); }
        }

        protected IRenderer()
        {
            _backColor = Color.Empty;
            _showGrid = false;
            _offsetX = 0;
            _offsetY = 0;
        }

        public void SetOffset(int x, int y)
        {
            _offsetX = x;
            _offsetY = y;
            Update(); 
        }

        public void SetOffset(Point pt)
        {
            _offsetX = pt.X;
            _offsetY = pt.Y;
            Update(); 
        }

        public void SetBackColor(byte r, byte g, byte b)
        {
            _backColor = Color.FromArgb(r, g, b);
            Update(); 
        }

        public abstract void Render(PaintEventArgs e);

        protected void OnRendererUpdate(RendererArgs e)
        {
            RendererEventHandler temp = RendererUpdate;
            if (temp != null)
                temp(e);
        }

        protected void Update()
        {
            Update(Rectangle.Empty);
        }

        protected void Update(Rectangle region)
        {
            RendererArgs e = new RendererArgs(region);
            OnRendererUpdate(e);
        }
    }
}
