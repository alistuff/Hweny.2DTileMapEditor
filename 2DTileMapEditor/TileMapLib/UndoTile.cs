using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapLib
{
    public class UndoTile
    {
        private Tile _tile;
        private int _x;
        private int _y;
        private int _layerId;

        public UndoTile() { }

        public UndoTile(Tile tile,int x,int y, int layerId)
        {
            _tile = tile;
            _x = x;
            _y = y;
            _layerId = layerId;
        }

        public Tile Tile
        {
            get { return _tile; }
            set { _tile = value; }
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int LayerId
        {
            get { return _layerId; }
            set { _layerId = value; }
        }
    }
}
