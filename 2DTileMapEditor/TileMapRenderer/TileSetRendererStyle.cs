using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapRenderer
{
    [Serializable]
    public class TileSetRendererStyle
    {
        private Color _gridColor;
        private Color _selectedAreaColor1;
        private Color _selectedAreaColor2;
        private bool _solid1;
        private bool _solid2;
        private byte _transparentColor1;
        private byte _transparentColor2;
        private bool _showGrid;

        public TileSetRendererStyle()
        {
            _gridColor = Color.DarkRed;
            _selectedAreaColor1 = Color.White;
            _selectedAreaColor2 = Color.Red;
            _solid1 = false;
            _solid2 = true;
            _showGrid = true;
            _transparentColor1 = 255;
            _transparentColor2 = 128;
        }

        public Color GridColor
        {
            get { return _gridColor; }
            set { _gridColor = value; }
        }

        public Color SelectedAreaColor1
        {
            get { return _selectedAreaColor1; }
            set { _selectedAreaColor1 = value; }
        }

        public Color SelectedAreaColor2
        {
            get { return _selectedAreaColor2; }
            set { _selectedAreaColor2 = value; }
        }

        public bool Solid1
        {
            get { return _solid1; }
            set { _solid1 = value; }
        }

        public bool Solid2
        {
            get { return _solid2; }
            set { _solid2 = value; }
        }

        public bool ShowGrid
        {
            get { return _showGrid; }
            set { _showGrid = value; }
        }

        public byte TransparentColor1
        {
            get { return _transparentColor1; }
            set { _transparentColor1 = value; }
        }

        public byte TransparentColor2
        {
            get { return _transparentColor2; }
            set { _transparentColor2 = value; }
        }
    }
}
