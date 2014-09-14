using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapLib
{
    public delegate void TileSelectedChangedEventHandle(TileSelectedChangedArgs e);
    public class TileSelectedChangedArgs : EventArgs
    {
        private Tile[,] _tiles;

        public Tile[,] SelectedTiles
        {
            get { return _tiles; }
        }

        public TileSelectedChangedArgs(Tile[,] tiles)
        {
            _tiles = tiles;
        }
    }
}
