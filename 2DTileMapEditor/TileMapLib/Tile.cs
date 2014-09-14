using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapLib
{
    /// <summary>
    /// 图素类
    /// </summary>
    public class Tile
    {
        private Tileset _tileset;
        private int _tilesetOffset;
        private bool _isEmpty;

        public Tileset Tileset
        {
            get { return _tileset; }
            set { _tileset = value; _isEmpty = false; }
        }

        public int OffsetID
        {
            get { return _tilesetOffset; }
            set { _tilesetOffset = value; }
        }

        public int OffsetX
        {
            get
            {
                return (OffsetID % (Tileset.Width / Tileset.TileWidth)) * Tileset.TileWidth;
            }
        }
        public int OffsetY
        {
            get
            {
                return (OffsetID / (Tileset.Width / Tileset.TileWidth)) * Tileset.TileHeight; 
            }
        }

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; }
        }

        public Tile()
        {
            Clear();
        }

        public Tile(Tileset tileset, int offset)
        {
            _tileset = tileset;
            _tilesetOffset = offset;
            _isEmpty = false;
        }

        public void Clear()
        {
            _tileset = null;
            _tilesetOffset = 0;
            _isEmpty = true;
        }

        public Tile Clone()
        {
            Tile tile = new Tile();
            tile.Tileset =_tileset;
            tile.OffsetID = _tilesetOffset;
            tile.IsEmpty = _isEmpty;
            return tile;
        }
    }
}
