using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapRenderer
{
    //public class GdiMapRenderer : MapRenderer
    //{
    //    private Bitmap _memoryBuffer;
    //    private Graphics _memG;

    //    public override EditableMap Map
    //    {
    //        set
    //        {
    //            if (_map != value)
    //            {
    //                _map = value;
    //                setMemoryBuffer();
    //            }
    //        }
    //    }

    //    public GdiMapRenderer()
    //        : base()
    //    {
    //    }

    //    public GdiMapRenderer(EditableMap map)
    //        : base(map)
    //    {
    //        setMemoryBuffer();
    //    }

    //    private void setMemoryBuffer()
    //    {
    //        Clear();
    //        if (_map != null)
    //        {
    //            _map.MapChanged += new MapChangedEventHandle(_map_MapChanged);
    //            _memoryBuffer = new Bitmap(_map.RealWidth ,_map.RealHeight);
    //            _memG = Graphics.FromImage(_memoryBuffer);

    //            renderMemoryBuffer();
    //        }
    //    }

    //    private void renderMemoryBuffer()
    //    {
    //        _memG.Clear(_backColor);
    //        paintMap(0,0,_map.Width,_map.Height);
    //    }

    //    private void paintMap(int left, int top, int width, int height)
    //    {
    //        int _tileSize = _map.TileSize;
    //        for (int i = 0; i < _map.MapLayers.Count; i++)
    //        {
    //            MapLayer layer = _map.MapLayers[i];
    //            if (layer.Visible)
    //            {
    //                for (int x = 0; x < width; x++)
    //                {
    //                    for (int y = 0; y < height; y++)
    //                    {
    //                        int dx = x + left;
    //                        int dy = y + top;

    //                        if (dx < _map.Width && dy < _map.Height)
    //                        {
    //                            Tile tile = layer[dx, dy];
    //                            if (tile != null && !tile.IsEmpty)
    //                            {
    //                                int posX = tile.Offset % (tile.Tileset.Width / _map.TileSize);
    //                                int posY = tile.Offset / (tile.Tileset.Width / _map.TileSize);
    //                                Rectangle dest = new Rectangle(dx * _map.TileSize, dy * _map.TileSize, 
    //                                    _map.TileSize, _map.TileSize);
    //                                Rectangle src = new Rectangle(posX * _map.TileSize, posY * _map.TileSize, 
    //                                    _map.TileSize, _map.TileSize);
    //                                _memG.FillRectangle(new SolidBrush(_backColor), dest);
    //                                _memG.DrawImage(tile.Tileset.Image, dest, src, GraphicsUnit.Pixel);
    //                            }
    //                            else
    //                            {
    //                                Rectangle dest = new Rectangle(dx * _map.TileSize, dy * _map.TileSize,
    //                                    _map.TileSize, _map.TileSize);
    //                                _memG.FillRectangle(new SolidBrush(_backColor),dest);
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    private void _map_MapChanged(MapChangedArgs e)
    //    {
    //        Rectangle region = e.Region;
    //        int _tileSize = _map.TileSize;
    //        paintMap(region.X, region.Y, region.Width, region.Height);

    //        Rectangle updateRect = region;
    //        updateRect.X = updateRect.X * _tileSize - _offsetX;
    //        updateRect.Y = updateRect.Y * _tileSize - _offsetY;
    //        updateRect.Width = updateRect.Width * _tileSize;
    //        updateRect.Height = updateRect.Height * _tileSize;

    //        Update(updateRect);
    //    }

    //    protected override void DrawMap(PaintEventArgs e)
    //    {
    //        base.DrawMap(e);
    //        if (_memoryBuffer != null)
    //        {
    //            Graphics g = e.Graphics;
    //            Rectangle dest = e.ClipRectangle;
    //            Rectangle src = new Rectangle(dest.X + _offsetX, dest.Y + _offsetY, dest.Width, dest.Height);
    //            g.DrawImage(_memoryBuffer, dest, src, GraphicsUnit.Pixel);
    //        }
    //    }

    //    public override void Clear()
    //    {
    //        base.Clear();

    //        if (_memoryBuffer != null)
    //        {
    //            _memoryBuffer.Dispose();
    //            _memoryBuffer = null;
    //            _memG.Dispose();
    //            _memG = null;
    //        }
    //    }

    //}
}
