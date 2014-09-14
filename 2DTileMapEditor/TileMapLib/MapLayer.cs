using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapLib
{
    /// <summary>
    /// 图素层
    /// </summary>
    public class MapLayer : Layer<Tile>, IComparable<MapLayer>
    {
        private int _id;
        private string _name;
        private int _zindex;
        private bool _backgroundLayer;

        public MapLayer(int id, string name, int width, int height, int zindex,bool bg)
            : base(width, height)
        {
            _id = id;
            _name = name;
            _zindex = zindex;
            _backgroundLayer = bg;
            _visible = true;
        }

        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return this._name; }
            set { _name = value; }
        }

        public int ZIndex
        {
            get { return _zindex; }
            set { _zindex = value; }
        }

        public bool BackgroundLayer
        {
            get { return _backgroundLayer; }
            set { _backgroundLayer = value; }
        }

        public int CompareTo(MapLayer other)
        {
            int result = 0;
            if (_zindex > other._zindex)
                result = 1;
            else if (_zindex < other._zindex)
                result = -1;
            return result;
        }
    }
}
