using System;
using TileMapLib;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public abstract class MapRenderer : IRenderer
    {
        protected EditableMap _map;
        protected Bitmap _blockIcon;

        protected Point _startPoint;
        protected Point _endPoint;
        protected Rectangle _selectedArea;
        protected Rectangle _realSelectedArea;
        protected Size _tileSize;

        protected MapRendererStyle _style;

        public virtual EditableMap Map
        {
            set { _map = value; Update(); }
        }

        public Bitmap BlockIcon
        {
            set { _blockIcon = value; Update(); }
        }

        public Point StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = adjustMouseCoordinate((Point)value); }
        }

        public Point EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = adjustMouseCoordinate((Point)value);
                _selectedArea = calculateSelectedRect();

                Rectangle oldArea = _realSelectedArea;
                oldArea.X = oldArea.X - _offsetX;
                oldArea.Y = oldArea.Y - _offsetY;
                oldArea.Inflate(2, 2);
                Update(oldArea);

                _realSelectedArea = calculateRealSelectedRect(_selectedArea);
                Update(_realSelectedArea);
            }
        }

        public Rectangle RealSelectedArea
        {
            get { return _realSelectedArea; }
            set
            {
                if (_realSelectedArea != Rectangle.Empty)
                {
                    Update(_realSelectedArea);
                }
                _realSelectedArea = value;
            }
        }

        public Size TileSize
        {
            get { return _tileSize; }
            set { _tileSize = value; Update(); }
        }

        public int RealWidth
        {
            get { return _tileSize.Width * _map.Width; }
        }

        public int RealHeight
        {
            get { return _tileSize.Height * _map.Height; }
        }

        public MapRendererStyle Style
        {
            get { return _style; }
            set { _style = value; Update(); }
        }

        protected MapRenderer()
            : base()
        {
            _showGrid = true;
            _backColor = Color.Black;
            _map = null;
            _blockIcon = new Bitmap(32, 32);

            _style = new MapRendererStyle();
        }

        protected MapRenderer(EditableMap map)
            : base()
        {
            _showGrid = true;
            _backColor = Color.Black;
            _map = map;
            _blockIcon = new Bitmap(32, 32);
            _style = new MapRendererStyle();
        }

        public override void Render(PaintEventArgs e)
        {
            if (_map != null)
            {
                DrawMap(e);
                DrawGrid(e);
                DrawBlock(e);
                DrawSelectedArea(e);
            }
            else
            {
                Graphics g = e.Graphics;
                g.Clear(_style.BackColor);
            }
        }

        protected virtual void DrawMap(PaintEventArgs e)
        {
            //
        }

        protected virtual void DrawGrid(PaintEventArgs e)
        {
            if (_showGrid)
            {
                Graphics g = e.Graphics;
                using (Pen pen = new Pen(_style.GridColor))
                {
                    for (int x = 0; x < RealWidth + _tileSize.Width; x += _tileSize.Width)
                        g.DrawLine(pen, x - _offsetX, 0, x - _offsetX, RealHeight);
                    for (int y = 0; y < RealHeight + _tileSize.Height; y += _tileSize.Height)
                        g.DrawLine(pen, 0, y - _offsetY, RealWidth, y - _offsetY);
                }
            }
        }

        protected virtual void DrawBlock(PaintEventArgs e)
        {
            if (_map.BlockLayer.Visible)
            {
                Graphics g = e.Graphics;
                for (int x = 0; x < _map.BlockLayer.Width; x++)
                {
                    for (int y = 0; y < _map.BlockLayer.Height; y++)
                    {
                        if (_map.BlockLayer[x, y])
                        {
                            g.DrawImage(_blockIcon, x * _tileSize.Width - _offsetX,
                                y *_tileSize.Height - _offsetY, _tileSize.Width, _tileSize.Height);
                        }
                    }
                }
            }
        }

        protected virtual void DrawSelectedArea(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (_realSelectedArea == Rectangle.Empty) return;

            Rectangle tempRect = new Rectangle(
                _realSelectedArea.X - _offsetX,
                _realSelectedArea.Y - _offsetY,
                _realSelectedArea.Width,
                _realSelectedArea.Height);
            
            if (Style.Solid)
            {
                using (Brush brush = new SolidBrush(
                    Color.FromArgb(_style.TransparentColor, _style.SelectedAreaColor)))
                {
                    g.FillRectangle(brush, tempRect);
                }
            }
            else
            {
                using (Pen pen = new Pen(_style.SelectedAreaColor, 2))
                {
                    g.DrawRectangle(pen,tempRect);
                }
            }
        }

        public virtual void Clear() { }

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

            left = left / _tileSize.Width * _tileSize.Width;
            top = top / _tileSize.Height * _tileSize.Height;
            right = ((right + _tileSize.Width) / _tileSize.Width) * _tileSize.Width;
            bottom = ((bottom +_tileSize.Height) / _tileSize.Height) * _tileSize.Height;

            return new Rectangle(left, top, right - left, bottom - top);
        }

        private Point adjustMouseCoordinate(Point pt)
        {
            int mouseX = pt.X;
            int mouseY = pt.Y;

            mouseX = Math.Max(0, Math.Min(pt.X + _offsetX, RealWidth - 1));
            mouseY = Math.Max(0, Math.Min(pt.Y + _offsetY, RealHeight - 1));

            return new Point(mouseX, mouseY);
        }

        #endregion
    }
}
