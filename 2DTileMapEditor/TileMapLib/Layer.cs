using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapLib
{
    public abstract class Layer<T>
    {
        protected int _width;
        protected int _height;
        protected bool _visible;
        protected T[,] _t;

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public T this[int x, int y]
        {
            get { return _t[x, y]; }
            set { _t[x, y] = value; }
        }

        public T[,] Elements
        {
            get { return _t; }
        }

        public Layer(int width, int height)
        {
            _width = width;
            _height = height;
            _visible = true;
            _t = new T[width, height];
        }

        public virtual void Resize(int width, int height)
        {
            if (width == _width && height == _height)
                return;

            T[,] newT = new T[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x < _width && y < _height)
                    {
                        newT[x, y] = _t[x, y];
                    }
                }
            }
            _t = null;
            _t = newT;

            _width = width;
            _height = height;
        }
    }
}
