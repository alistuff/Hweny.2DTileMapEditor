using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public class GdiTilesetRenderer : TileSetRenderer
    {
        private Bitmap _memoryBuffer;
        private Graphics _memG;

        public override Tileset Tileset
        {
            set { _tileset = value; setMemoryBuffer(); Update(); }
        }

        public GdiTilesetRenderer()
            : base()
        {

        }

        public GdiTilesetRenderer(Tileset tileset)
            : base(tileset)
        {
            setMemoryBuffer();
        }

        private void setMemoryBuffer()
        {
            Clear();
            if (_tileset != null)
            {
                _memoryBuffer = new Bitmap(_tileset.Width, _tileset.Height);
                _memG = Graphics.FromImage(_memoryBuffer);

                renderMemoryBuffer();
            }
        }

        private void renderMemoryBuffer()
        {
            _memG.DrawImage(_tileset.Image, 0, 0, _tileset.Width, _tileset.Height);
        }

        protected override void DrawTileset(PaintEventArgs e)
        {
            base.DrawTileset(e);

            Graphics g = e.Graphics;
            int dx = e.ClipRectangle.X;
            int dy = e.ClipRectangle.Y;
            if (_memoryBuffer != null)
            {
                Rectangle dest = e.ClipRectangle;
                Rectangle src = new Rectangle(dest.X + _offsetX, dest.Y + _offsetY, dest.Width, dest.Height);

                g.DrawImage(_memoryBuffer, dest, src, GraphicsUnit.Pixel);
            }
        }

        public override void Clear()
        {
            base.Clear();

            if (_memoryBuffer != null)
            {
                _memoryBuffer.Dispose();
                _memoryBuffer = null;
                _memG.Dispose();
                _memG = null;
            }
        }
    }
}
