using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapLib
{
    public delegate void MapChangedEventHandle(MapChangedArgs e);
    public class MapChangedArgs : EventArgs
    {
        private Rectangle _region;
        public Rectangle Region
        {
            get { return _region; }
        }

        public MapChangedArgs(Rectangle region)
        {
            _region = region;
        }
    }

    public delegate void MapTileResizeEventHandler(MapTileResizeArgs e);
    public class MapTileResizeArgs : EventArgs
    {
        private int _tileWidth;
        private int _tileHeight;
        public int TileWidth
        {
            get { return _tileWidth; }
        }
        public int TileHeight
        {
            get { return _tileHeight; }
        }
        public MapTileResizeArgs(int width, int height)
        {
            _tileWidth = width;
            _tileHeight = height;
        }
    }
}
