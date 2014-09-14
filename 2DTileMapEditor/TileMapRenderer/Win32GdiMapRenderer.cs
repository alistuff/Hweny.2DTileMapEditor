using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapRenderer
{
    public class Win32GdiMapRenderer : MapRenderer
    {
        public override EditableMap Map
        {
            set
            {
                if (_map != value)
                {
                    _map = value;
                    _map.MapTileResized += new MapTileResizeEventHandler(_map_MapTileResized);
                    _map.MapChanged += new MapChangedEventHandle(_map_MapChanged);
                }
            }
        }

        public Win32GdiMapRenderer()
            : base()
        {

        }

        public Win32GdiMapRenderer(EditableMap map)
            : base(map)
        {
            _map.MapTileResized += new MapTileResizeEventHandler(_map_MapTileResized);
            _map.MapChanged += new MapChangedEventHandle(_map_MapChanged);
        }

        private void _map_MapTileResized(MapTileResizeArgs e)
        {
            _tileSize = new Size(e.TileWidth,e.TileHeight);
        }

        private void _map_MapChanged(MapChangedArgs e)
        {
            Rectangle updateRect = e.Region;
            updateRect.X = updateRect.X * _tileSize.Width - _offsetX;
            updateRect.Y = updateRect.Y * _tileSize.Height - _offsetY;
            updateRect.Width = updateRect.Width * _tileSize.Width;
            updateRect.Height = updateRect.Height * _tileSize.Height;

            Update(updateRect);
        }

        protected override void DrawMap(PaintEventArgs e)
        {
            base.DrawMap(e);

            using (Brush brush = new SolidBrush(_style.BackColor))
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, RealWidth, RealHeight));

            IntPtr target = e.Graphics.GetHdc();
            IntPtr source = Win32Gdi.CreateCompatibleDC(target);
            for (int i = 0; i < _map.MapLayers.Count; i++)
            {
                MapLayer layer = _map.MapLayers[i];
                if (layer.Visible)
                {
                    for (int x = 0; x < _map.Width; x++)
                    {
                        for (int y = 0; y < _map.Height; y++)
                        {
                            int dx = x + e.ClipRectangle.Left / _tileSize.Width;
                            int dy = y + e.ClipRectangle.Top / _tileSize.Height;

                            if (dx < _map.Width && dy < _map.Height)
                            {
                                Tile tile = layer[dx, dy];
                                if (tile != null && !tile.IsEmpty)
                                {
                                    int posX = tile.OffsetID % (tile.Tileset.Width / tile.Tileset.TileWidth);
                                    int posY = tile.OffsetID / (tile.Tileset.Width / tile.Tileset.TileWidth);

                                    byte alpha = 255;
                                    if (_map.HighLightMapLayer)
                                    {
                                        if (layer.ID != _map.CurrentMapLayer.ID)
                                            alpha = 127;
                                    }

                                    Win32Gdi.SelectObject(source, tile.Tileset.PtrImage);
                                    Win32Gdi.AlphaBlend(
                                        target,
                                        dx * _tileSize.Width - _offsetX,
                                        dy * _tileSize.Height - _offsetY,
                                        _tileSize.Width,
                                        _tileSize.Height,
                                        source,
                                        posX * tile.Tileset.TileWidth,
                                        posY * tile.Tileset.TileHeight,
                                        tile.Tileset.TileWidth,
                                        tile.Tileset.TileHeight,
                                        new Win32Gdi.BLENDFUNCTION(Win32Gdi.AC_SRC_OVER, 0,
                                            alpha, Win32Gdi.AC_SRC_ALPHA));
                                }
                            }
                        }
                    }
                }
            }

            Win32Gdi.DeleteDC(source);
            e.Graphics.ReleaseHdc(target);
        }
    }
}
