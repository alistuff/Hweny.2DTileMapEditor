using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 剪贴板
    /// </summary>
    class TileMapClipboard
    {
        private EditableMap _map;
        private Rectangle _selectArea;
        private Tile[,] _clipboardData;
        private bool _isCut;

        public TileMapClipboard(EditableMap map)
        {
            _map = map;
            ClearClipboard();
        }

        /// <summary>
        /// 是否能剪切
        /// </summary>
        public bool CanCut
        {
            get { return _selectArea == Rectangle.Empty ? false : true; }
        }

        /// <summary>
        /// 是否能复制
        /// </summary>
        public bool CanCopy
        {
            get { return CanCut; }
        }

        /// <summary>
        /// 是否能粘贴
        /// </summary>
        public bool CanPaster
        {
            get
            {
                if (_clipboardData == null) return false;
                return true;
            }
        }

        /// <summary>
        /// 清空剪贴板
        /// </summary>
        public void ClearClipboard()
        {
            _selectArea = Rectangle.Empty;
            _clipboardData = null;
        }

        public Rectangle SelectArea
        {
            get { return _selectArea; }
            set { _selectArea = value; }
        }

        /// <summary>
        /// 剪切
        /// </summary>
        public void Cut()
        {
            if (!CanCut) return;
            _clipboardData = getSelectedTiles();

            _isCut = true;
        }

        /// <summary>
        /// 复制
        /// </summary>
        public void Copy()
        {
            if (!CanCopy) return;

            _clipboardData = getSelectedTiles();

            _isCut = false;
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Paster(int x, int y)
        {
            if (!CanPaster) return;

            for (int i = 0; i < _clipboardData.GetLength(0); i++)
            {
                for (int j = 0; j < _clipboardData.GetLength(1); j++)
                {
                    int dx = x + i;
                    int dy = y + j;
                    if (dx < _map.Width && dy < _map.Height)
                    {
                        _map.AddUndoTile(dx, dy, true);
                        _map.SetTile(_clipboardData[i, j], dx, dy, _map.CurrentMapLayer.ID);
                    }
                }
            }

            if (_isCut)
            {
                ClearClipboard();
            }
        }

        private Tile[,] getSelectedTiles()
        {
            Tile[,] tiles = null;
            if (_selectArea != Rectangle.Empty)
            {
                tiles = new Tile[_selectArea.Width, _selectArea.Height];
                for (int x = 0; x < _selectArea.Width; x++)
                {
                    for (int y = 0; y < _selectArea.Height; y++)
                    {
                        tiles[x, y] = _map.GetTile(_selectArea.X + x, _selectArea.Y + y, _map.CurrentMapLayer.ID);
                    }
                }
            }

            return tiles;
        }
    }
}
