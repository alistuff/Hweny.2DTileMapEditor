using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TileMapLib;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public abstract class MiniMapRenderer : IRenderer
    {
        protected bool _isDrag;
        protected Point _viewPortPosition;
        protected Point _dragPosition;
        protected Rectangle _viewPort;
        protected Rectangle _miniMapClientRect;
        protected EditableMap _map;
        protected int _miniTileWidth=1;
        protected int _miniTileHeight=1;

        public bool IsDrag
        {
            get { return _isDrag; }
            set { _isDrag = value; }
        }

        public Point ViewPortPosition
        {
            get { return _viewPortPosition; }
            set { _viewPortPosition = value; Update(); }
        }

        public Point DragPosition
        {
            get { return _dragPosition; }
            set { _dragPosition = value; Update(); }
        }

        public Rectangle ViewPort
        {
            get { return _viewPort; }
            set { _viewPort = value; Update(); }
        }

        public virtual Rectangle MiniMapClientRect
        {
            set 
            { 
                _miniMapClientRect = value;
                Update(); 
            }
        }

        public virtual EditableMap Map
        {
            set { _map = value; Update(); }
        }

        protected MiniMapRenderer()
            : base()
        {
            _viewPort = Rectangle.Empty;
            _miniMapClientRect = Rectangle.Empty;
            _map = null;
        }

        protected MiniMapRenderer(EditableMap map, Rectangle viewPort)
            : base()
        {
            _map = map;
            _miniMapClientRect = Rectangle.Empty;
            _viewPort = viewPort;
        }

        public override void Render(PaintEventArgs e)
        {
            if (_map != null)
            {
                DrawMap(e);
                DrawViewPort(e);
            }
        }

        protected virtual void DrawMap(PaintEventArgs e) { }
        protected virtual void DrawViewPort(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (_viewPort != Rectangle.Empty)
            {
                int width = _viewPort.Width / _map.TileWidth * _miniTileWidth;
                int height = _viewPort.Height / _map.TileHeight * _miniTileHeight;
                float x = 0f;
                float y = 0f;
                if (!_isDrag)
                {
                    x = (float)Math.Floor((_viewPortPosition.X) / ((float)_viewPort.Width / width));
                    y = (float)Math.Floor((_viewPortPosition.Y) / ((float)_viewPort.Height / height));
                }
                else
                {
                    x = _dragPosition.X - width / 2;
                    y = _dragPosition.Y - height / 2;
                    if (x <= 0)
                        x = 0;
                    if (y <= 0)
                        y = 0;
                    if (x + width >= _miniTileWidth * _map.Width)
                        x = _miniTileWidth * _map.Width - width;
                    if (y + height >= _miniTileHeight * _map.Height)
                        y = _miniTileHeight * _map.Height - height;

                    _viewPortPosition.X = Convert.ToInt32(x * (_viewPort.Width / width));
                    _viewPortPosition.Y = Convert.ToInt32(y * (_viewPort.Height / height));

                }
                using (Pen pen = new Pen(Color.FromArgb(255, 255, 255), 2))
                    g.DrawRectangle(pen, x, y, width, height);
            }
        }

        protected virtual void Clear() { }

        protected int getMiniGridWidth()
        {
            int result = 0;
            if (_miniMapClientRect.Width > _map.RealWidth)
                result = _map.TileWidth;
            else
                result = _miniMapClientRect.Width / _map.Width;
            return result;
        }
        protected int getMiniGridHeight()
        {
            int result = 0;
            if (_miniMapClientRect.Height > _map.RealHeight)
                result = _map.TileHeight;
            else
                result = _miniMapClientRect.Height / _map.Height;
            return result;
        }
    }
}
