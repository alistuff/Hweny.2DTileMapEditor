using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;

namespace TileMapRenderer
{
    public class Win32GdiMiniMapRenderer : MiniMapRenderer
    {
        public override EditableMap Map
        {
            set
            {
                if (_map != value)
                {
                    _map = value;
                    _map.MapChanged += new MapChangedEventHandle(_map_MapChanged);
                }
            }
        }

        public Win32GdiMiniMapRenderer()
            : base()
        {

        }

        public Win32GdiMiniMapRenderer(EditableMap map, Rectangle viewPort)
            : base(map, viewPort)
        {
            _map.MapChanged+=new MapChangedEventHandle(_map_MapChanged);
        }

        void _map_MapChanged(MapChangedArgs e)
        {
            _miniTileWidth = _map.TileWidth;
            _miniTileHeight = _map.TileHeight;
            while (_miniTileWidth * _map.Width > _miniMapClientRect.Width ||
                _miniTileHeight * _map.Height > _miniMapClientRect.Height)
            {
                _miniTileWidth = _miniTileWidth >> 1;
                _miniTileHeight = _miniTileHeight >> 1;
            }
            _miniTileWidth = _miniTileWidth == 0 ? 1 : _miniTileWidth;
            _miniTileHeight = _miniTileHeight == 0 ? 1 : _miniTileHeight;
            Update();
        }

        protected override void DrawMap(System.Windows.Forms.PaintEventArgs e)
        {
            base.DrawMap(e);
            e.Graphics.Clear(Color.Black);

            IntPtr target = e.Graphics.GetHdc();
            IntPtr source = Win32Gdi.CreateCompatibleDC(target);

            for (int i = 0; i < _map.MapLayers.Count; i++)
            {
                MapLayer layer = _map.MapLayers[i];
                if (layer.Visible)
                {
                    for (int x = e.ClipRectangle.X; x < e.ClipRectangle.Width; x++)
                    {
                        if (x >= _map.Width)
                        {
                            break;
                        }
                        for (int y = e.ClipRectangle.Y; y < e.ClipRectangle.Height; y++)
                        {
                            if (y >= _map.Height) break ;
                            Tile tile = layer[x, y];
                            if (tile != null && !tile.IsEmpty)
                            {
                                int posX = tile.OffsetID % (tile.Tileset.Width / tile.Tileset.TileWidth);
                                int posY = tile.OffsetID / (tile.Tileset.Width / tile.Tileset.TileHeight);
                                Win32Gdi.SelectObject(source, tile.Tileset.PtrImage);
                                Win32Gdi.AlphaBlend(
                                        target,
                                        x*_miniTileWidth,
                                        y*_miniTileHeight,
                                        _miniTileWidth,
                                        _miniTileHeight,
                                        source,
                                        posX * tile.Tileset.TileWidth,
                                        posY * tile.Tileset.TileHeight,
                                        tile.Tileset.TileWidth,
                                        tile.Tileset.TileHeight,
                                      new Win32Gdi.BLENDFUNCTION
                                          (Win32Gdi.AC_SRC_OVER, 0, 255,
                                          Win32Gdi.AC_SRC_ALPHA));
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
