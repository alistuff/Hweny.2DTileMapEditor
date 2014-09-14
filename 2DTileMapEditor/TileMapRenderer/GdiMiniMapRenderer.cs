using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;

namespace TileMapRenderer
{
    public class GdiMiniMapRenderer:MiniMapRenderer
    {
        //private Bitmap _memoryBuffer;
        //private Graphics _memG;

        //public override Rectangle MiniMapClientRect
        //{
        //    set
        //    {
        //        _miniMapClientRect = value; setMemoryBuffer(); Update();
        //    }
        //}

        //public override EditableMap Map
        //{
        //    set
        //    {
        //        _map = value;
        //        setMemoryBuffer();
        //    }
        //}

        //public GdiMiniMapRenderer()
        //    : base()
        //{

        //}

        //public GdiMiniMapRenderer(EditableMap map, Rectangle viewPort)
        //    : base(map, viewPort)
        //{
        //    setMemoryBuffer();
        //}

        //private void setMemoryBuffer()
        //{
        //    Clear();
        //    if (_map != null&&_miniMapClientRect!=Rectangle.Empty)
        //    {
        //        _map.MapChanged += new MapChangedEventHandle(_map_MapChanged);
        //        _memoryBuffer = new Bitmap(_miniMapClientRect.Width, _miniMapClientRect.Height);
        //        _memG = Graphics.FromImage(_memoryBuffer);
        //        rendererMemoryBuffer();
        //    }
        //}

        //void _map_MapChanged(MapChangedArgs e)
        //{
        //    paintMap(e.Region.X,e.Region.Y,e.Region.Width,e.Region.Height);
        //    Update();
        //}

        //private void rendererMemoryBuffer()
        //{
        //    _memG.Clear(Color.Black);
        //    paintMap(0,0,_map.Width,_map.Height);
        //}

        //private void paintMap(int left, int top, int width, int height)
        //{
        //    for (int i = 0; i < _map.MapLayers.Count; i++)
        //    {
        //        MapLayer layer = _map.MapLayers[i];
        //        if (layer.Visible)
        //        {
        //            for (int x = 0; x < width; x++)
        //            {
        //                for (int y = 0; y < height; y++)
        //                {
        //                    int offsetX = x + left;
        //                    int offsetY = y + top;
        //                    if (offsetX < _map.Width && offsetY < _map.Height)
        //                    {
        //                        Tile tile = layer[offsetX, offsetY];
        //                        RectangleF dest = new RectangleF
        //                            (
        //                                offsetX * getMiniGridWidth() + 1, 
        //                                offsetY * getMiniGridHeight() + 1, 
        //                                getMiniGridWidth() - 1, 
        //                                getMiniGridHeight() - 1
        //                            );
        //                        if (tile != null && !tile.IsEmpty)
        //                        {
        //                            int posX = tile.Offset % (tile.Tileset.Width / _map.TileSize);
        //                            int posY = tile.Offset / (tile.Tileset.Width / _map.TileSize);
        //                            Color color = tile.Tileset.Image.GetPixel(
        //                                posX * _map.TileSize + (_map.TileSize >> 1), posY * _map.TileSize + (_map.TileSize >> 1));
        //                            _memG.FillRectangle(new SolidBrush(color), dest);
        //                        }
        //                        else
        //                            _memG.FillRectangle(new SolidBrush(Color.Black), dest);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //protected override void DrawMap(System.Windows.Forms.PaintEventArgs e)
        //{
        //    base.DrawMap(e);
        //    if (_memoryBuffer != null)
        //        e.Graphics.DrawImage(_memoryBuffer, e.ClipRectangle);
        //}

        //protected override void Clear()
        //{
        //    if (_memoryBuffer != null)
        //    {
        //        _memoryBuffer.Dispose();
        //        _memoryBuffer = null;
        //        _memG.Dispose();
        //        _memG = null;
        //    }
        //}
    }
}
