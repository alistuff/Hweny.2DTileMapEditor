using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapRenderer
{
    [Serializable]
    public class MapRendererStyle
    {
        private Color _gridColor;
        private Color _selectedAreaColor;
        private Color _backColor;
        private bool _solid;
        private byte _transparentColor;
        private string _blockType;

        public MapRendererStyle()
        {
            _gridColor = Color.LightGray;
            _selectedAreaColor = Color.LightGreen;
            _backColor = Color.Black;
            _solid = true;
            _transparentColor = 128;
            _blockType = "ico_block";
        }

        public Color GridColor
        {
            get { return _gridColor; }
            set { _gridColor = value; }
        }

        public Color SelectedAreaColor
        {
            get { return _selectedAreaColor; }
            set { _selectedAreaColor = value; }
        }

        public Color BackColor
        {
            get { return _backColor; }
            set { _backColor = value; }
        }

        public bool Solid
        {
            get { return _solid; }
            set { _solid = value; }
        }

        public byte TransparentColor
        {
            get { return _transparentColor; }
            set { _transparentColor = value; }
        }

        public string BlockType
        {
            get { return _blockType; }
            set { _blockType = value; }
        }
    }
}
