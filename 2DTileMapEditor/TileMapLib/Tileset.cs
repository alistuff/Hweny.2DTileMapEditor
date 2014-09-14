using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapLib
{
    /// <summary>
    /// 图块类
    /// </summary>
    public class Tileset : IComparable
    {
        private int _id;
        private string _name;
        private int _width;
        private int _height;
        private Bitmap _image;
        private IntPtr _pImage;
        private Color _transparentColor;

        private int _tileWidth;
        private int _tileHeight;

        public Tileset(int id, string name, Image image)
        {
            _id = id;
            _name = name;
            _image = new Bitmap(image);
            _width = _image.Width;
            _height = _image.Height;
            _pImage = _image.GetHbitmap(Color.Black);
            _transparentColor = Color.Empty;
        }

        public Tileset(int id, string name, Image image, Color transparentColor)
        {
            _id = id;
            _name = name;
            _image = new Bitmap(image);
            _width = _image.Width;
            _height = _image.Height;
            _pImage = _image.GetHbitmap(Color.Black);
            _transparentColor = transparentColor;
        }

        public Tileset(int id, string name, int tileWidth, int tileHeight, Image image)
        {
            _id = id;
            _name = name;
            _image = new Bitmap(image);
            _width = _image.Width;
            _height = _image.Height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _pImage = _image.GetHbitmap(Color.Black);
            _transparentColor = Color.Empty;
        }

        public Tileset(int id, string name, int tileWidth, int tileHeight, Image image, Color transparentColor)
        {
            _id = id;
            _name = name;
            _image = new Bitmap(image);
            _width = _image.Width;
            _height = _image.Height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _pImage = _image.GetHbitmap(Color.Black);
            _transparentColor = transparentColor;
        }

        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int TileWidth
        {
            get { return _tileWidth; }
            set { _tileWidth = value; }
        }

        public int TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; }
        }

        public Bitmap Image
        {
            get { return _image; }
        }

        public IntPtr PtrImage
        {
            get { return _pImage; }
        }

        public Color TransparentColor
        {
            get { return _transparentColor; }
            set { _transparentColor = value; }
        }

        public override string ToString()
        {
            return _name;
        }

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            int result = 0;
            if (this.ID > ((Tileset)obj).ID)
                result = 1;
            else if (this.ID < ((Tileset)obj).ID)
                result = -1;
            return result;
        }

        #endregion
    }
}
