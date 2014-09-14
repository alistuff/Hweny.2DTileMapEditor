using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public class Win32GdiTilesetRenderer : TileSetRenderer
    {
        public Win32GdiTilesetRenderer()
            : base()
        {

        }

        public Win32GdiTilesetRenderer(Tileset tileset)
            : base(tileset)
        {

        }

        protected override void DrawTileset(PaintEventArgs e)
        {
            base.DrawTileset(e);
        }
    }
}
