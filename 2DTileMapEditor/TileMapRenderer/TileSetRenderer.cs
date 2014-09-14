using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TileMapLib;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public abstract class TileSetRenderer : IRenderer
    {
        protected Point _startPoint;
        protected Point _endPoint;
        protected Rectangle _selectedArea;
        protected Rectangle _realSelectedArea;
        protected bool _isSelected;
        protected Tileset _tileset;

        protected TileSetRendererStyle _style;

        public Point StartPoint
        {
            set
            {
                _startPoint = adjustMouseCoordinate((Point)value); 
                Update();
            }
        }

        public Point EndPoint
        {
            set
            {
                _endPoint = adjustMouseCoordinate((Point)value);
                _selectedArea = calculateSelectedRect();
                _realSelectedArea = calculateRealSelectedRect(_selectedArea);
                Update();
            }
        }

        public bool IsSelected
        {
            set { _isSelected = value; Update(); }
        }

        public virtual Tileset Tileset
        {
            set { _tileset = value; Update(); }
        }

        public Rectangle RealSelectedArea
        {
            get { return _realSelectedArea; }
            set {  _realSelectedArea = value; Update(); }
        }

        public TileSetRendererStyle Style
        {
            get { return _style; }
            set { _style = value; Update(); }
        }

        protected TileSetRenderer()
            : base()
        {
            _startPoint = Point.Empty;
            _endPoint = Point.Empty;
            _selectedArea = Rectangle.Empty;
            _realSelectedArea = Rectangle.Empty;
            _isSelected = false;
            _tileset = null;

            _style = new TileSetRendererStyle();
        }

        protected TileSetRenderer(Tileset tileset)
            : base()
        {
            _startPoint = Point.Empty;
            _endPoint = Point.Empty;
            _selectedArea = Rectangle.Empty;
            _realSelectedArea = Rectangle.Empty;
            _isSelected = false;
            _tileset = tileset;

            _style = new TileSetRendererStyle();
        }

        public override void Render(PaintEventArgs e)
        {
            if (_tileset != null)
            {
                DrawTileset(e);
                DrawGrid(e);
                DrawSelectedArea(e);
            }
            else
            {
                Graphics g = e.Graphics;
                g.Clear(_backColor);
            }
        }

        protected virtual void DrawTileset(PaintEventArgs e)
        {
        }

        protected virtual void DrawGrid(PaintEventArgs e)
        {
            if (_style.ShowGrid)
            {
                Graphics g = e.Graphics;
                using (Pen pen = new Pen(_style.GridColor))
                {
                    for (int x = 0; x < _tileset.Width + _tileset.TileWidth; x += _tileset.TileWidth)
                        g.DrawLine(pen, x - _offsetX, 0, x - _offsetX, _tileset.Height);
                    for (int y = 0; y < _tileset.Height + _tileset.TileHeight; y += _tileset.TileHeight)
                        g.DrawLine(pen, 0, y - _offsetY, _tileset.Width, y - _offsetY);
                }
            }
        }

        protected virtual void DrawSelectedArea(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle tempRect = _realSelectedArea;

            if (tempRect.Left <= 0)
                tempRect.X = tempRect.X + 1;
            if (tempRect.Right >= _tileset.Width - 1)
                tempRect.Width = tempRect.Width - 1;
            if (tempRect.Top <= 0)
                tempRect.Y = tempRect.Y + 1;
            if (tempRect.Bottom >= _tileset.Height - 1)
                tempRect.Height = tempRect.Height - 1;

            tempRect = new Rectangle(tempRect.X - _offsetX, tempRect.Y - OffsetY,
                    tempRect.Width, tempRect.Height);

            if (_style.Solid2)
            {
                using (Brush brush = new SolidBrush(
                    Color.FromArgb(_style.TransparentColor2, _style.SelectedAreaColor2)))
                {
                    g.FillRectangle(brush, tempRect);
                }
            }
            else
            {
                using (Pen pen = new Pen(_style.SelectedAreaColor2, 2))
                {
                    g.DrawRectangle(pen, tempRect);
                }
            }

            //

            tempRect = _selectedArea;           
            if (!_isSelected)
                tempRect = Rectangle.Empty;
            else
                tempRect.Inflate(-1, -1);

            tempRect = new Rectangle(tempRect.X - _offsetX, tempRect.Y - OffsetY,
                tempRect.Width, tempRect.Height);

            if (_style.Solid1)
            {
                using (Brush brush = new SolidBrush(
               Color.FromArgb(_style.TransparentColor1, _style.SelectedAreaColor1)))
                {
                    g.FillRectangle(brush, tempRect);
                }
            }
            else
            {
                using (Pen pen = new Pen(_style.SelectedAreaColor1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, tempRect);
                }
            }
            _selectedArea = Rectangle.Empty;
        }

        public virtual void Clear()
        {

        }

        #region private functions

        private Rectangle calculateSelectedRect()
        {
            int startX;
            int startY;
            int width;
            int height;

            if (_startPoint.X < _endPoint.X)
            {
                startX = _startPoint.X;
                width = _endPoint.X - _startPoint.X;
            }
            else
            {
                startX = _endPoint.X;
                width = _startPoint.X - _endPoint.X;
            }

            if (_startPoint.Y < _endPoint.Y)
            {
                startY = _startPoint.Y;
                height = _endPoint.Y - _startPoint.Y;
            }
            else
            {
                startY = _endPoint.Y;
                height = _startPoint.Y - _endPoint.Y;
            }

            return new Rectangle(startX, startY, width, height);
        }

        private Rectangle calculateRealSelectedRect(Rectangle rect)
        {
            int left = rect.Left;
            int top = rect.Top;
            int right = rect.Right;
            int bottom = rect.Bottom;

            int tileWidth = _tileset.TileWidth;
            int tileHeight = _tileset.TileHeight;

            left = left / tileWidth * tileWidth;
            top = top / tileHeight * tileHeight;
            right = ((right + tileWidth) / tileWidth) * tileWidth;
            bottom = ((bottom + tileHeight) / tileHeight) * tileHeight;

            return new Rectangle(left, top, right - left, bottom - top);
        }

        private Point adjustMouseCoordinate(Point pt)
        {
            int mouseX = pt.X;
            int mouseY = pt.Y;

            mouseX = Math.Max(0, Math.Min(pt.X + _offsetX, _tileset.Width - 1));
            mouseY = Math.Max(0, Math.Min(pt.Y + _offsetY, _tileset.Height - 1));

            return new Point(mouseX, mouseY);
        }

        #endregion
    }
}
