using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Collections;

namespace TileMapLib
{
    /// <summary>
    /// 地图编辑模式
    /// </summary>
    public enum EditableMapMode
    {
        DrawMode,
        EditMode
    }

    /// <summary>
    /// 继承于Map类，提供一组方法用于访问和修改地图数据
    /// </summary>
    public class EditableMap : Map
    {
        private MapLayer _currentMapLayer;
        private Tile[,] _currentTiles;
        private EditableMapMode _editableMapMode;
        private bool _highLightMapLayer;
        private UndoGroup _undoGroup;

        public event MapChangedEventHandle MapChanged;
        public event MapTileResizeEventHandler MapTileResized;

        public MapLayer CurrentMapLayer
        {
            get { return _currentMapLayer; }
            set
            {
                if (_mapLayers.Contains(value))
                {
                    _currentMapLayer = value;
                }
            }
        }

        public bool HighLightMapLayer
        {
            get { return _highLightMapLayer; }
            set 
            { 
                _highLightMapLayer = value;
                OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
            }
        }

        public EditableMap()
            : base()
        {
            ClearUndoGroup();
        }

        public EditableMap(int id, string name, int width, int height, int tileWidth, int tileHeight)
        {
            Initialize(id, name, width, height, tileWidth, tileHeight);
        }

        private void Initialize(int id, string name, int width, int height,int tileWidth, int tileHeight)
        {
            _id = id;
            _name = name;
            _width = width;
            _height = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _currentTiles = new Tile[0, 0];
            _mapLayers = new List<MapLayer>();
            _blockLayer = new BlockLayer(width, height);
            AddMapLayer("Default", 0, true);

            ClearUndoGroup();
        }

        /// <summary>
        /// 设置地图模式
        /// </summary>
        public EditableMapMode MapMode
        {
            get { return _editableMapMode; }
            set
            {
                _editableMapMode = value;
                OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, RealWidth, RealHeight)));
            }
        }

        /// <summary>
        /// 设置地图名称
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 设置地图格子尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetTileSize(int width, int height)
        {
            _tileWidth = width;
            _tileHeight = height;

            OnMapTileResized(new MapTileResizeArgs(width,height));
            OnMapChanged(new MapChangedArgs(new Rectangle(0,0,_width,_height)));
        }

        /// <summary>
        /// 设置地图尺寸
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(int width, int height)
        {
            _width = width;
            _height = height;

            foreach (MapLayer layer in _mapLayers)
            {
                layer.Resize(width, height);
            }

            _blockLayer.Resize(width, height);
            //_eventLayer.Resize(width, height);
        }

        /// <summary>
        /// 添加新图层
        /// </summary>
        /// <param name="name"></param>
        /// <param name="zIndex"></param>
        /// <param name="background"></param>
        public void AddMapLayer(string name, int zIndex, bool background)
        {
            int id = 0;
            foreach (MapLayer layer in _mapLayers)
            {
                if (layer.ID <= id)
                    id++;
            }

            MapLayer newLayer = new MapLayer(id, name, _width, _height, zIndex, background);
            _mapLayers.Add(newLayer);
            _mapLayers.Sort();

            _currentMapLayer = newLayer;
            OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
        }

        /// <summary>
        /// 删除指定图层
        /// </summary>
        /// <param name="index"></param>
        public void DeleteMapLayer(int index)
        {
            if (index < 0 || index >= _mapLayers.Count)
                return;
            _mapLayers.RemoveAt(index);
            OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
        }

        /// <summary>
        /// 设置指定图层为当前可编辑图层
        /// </summary>
        /// <param name="index"></param>
        public void SetCurrentMapLayer(int index)
        {
            if (index < 0) return;
            if (index >= _mapLayers.Count) return;

            _currentMapLayer = _mapLayers[index];
           
            if (_highLightMapLayer)
            {
                OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
            }
        }

        /// <summary>
        /// 设置当前已选择的图块
        /// </summary>
        /// <param name="tiles"></param>
        public void SetSelectedTiles(Tile[,] tiles)
        {
            _currentTiles = tiles;
        }

        /// <summary>
        /// 获取图素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layerId"></param>
        /// <returns></returns>
        public Tile GetTile(int x, int y, int layerId)
        {
            MapLayer layer = CheckLayerExists(layerId);
            if (layer == null) return null;
            if (x < 0 || x > _width - 1) return null;
            if (y < 0 || y > _height - 1) return null;
            return layer[x, y];
        }
        
        /// <summary>
        /// 设置图素
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="layerId"></param>
        /// <returns></returns>
        public bool SetTile(Tile tile, int x, int y, int layerId)
        {
            MapLayer layer = CheckLayerExists(layerId);
            if (layer == null) return false;
            if (x < 0 || x > _width - 1) return false; 
            if (y < 0 || y > _height - 1) return false;

            layer[x, y] = tile;

            OnMapChanged(new MapChangedArgs(new Rectangle(x, y, 1, 1)));

            return true;
        }

        /// <summary>
        /// 根据图层ID判断是否存在该图层
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private MapLayer CheckLayerExists(int id)
        {
            foreach (MapLayer layer in _mapLayers)
            {
                if (layer.ID == id)
                    return layer;
            }
            return null;
        }

        /// <summary>
        /// 绘图模式下在指定地图格子位置设置地图图块,编辑模式下在指定位置设置障碍物
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetTiles(int x, int y)
        {
            if (_editableMapMode == EditableMapMode.DrawMode)
            {
                for (int i = 0; i < GetSelectedTilesWidth(); i++)
                {
                    for (int j = 0; j < GetSelectedTilesHeight(); j++)
                    {
                        int dx = x + i;
                        int dy = y + j;

                        if (dx < _width && dy < _height)                  
                        {
                            Tile tile = (Tile)_currentTiles[i, j].Clone();
                            AddUndoTile(dx, dy, true);
                            _currentMapLayer[dx, dy] = tile;
                        }
                    }
                }
                OnMapChanged(new MapChangedArgs(new Rectangle(x, y, 
                    GetSelectedTilesWidth(), GetSelectedTilesHeight())));
            }
            else if (_editableMapMode == EditableMapMode.EditMode)
            {
                _blockLayer[x, y] = true;
                OnMapChanged(new MapChangedArgs(new Rectangle(x, y, 1, 1)));
            }
        }

        /// <summary>
        /// 绘图模式下在指定地图矩形区域内设置图块信息
        /// </summary>
        /// <param name="rect"></param>
        public void SetTiles(Rectangle rect)
        {
            SetTiles(rect.X, rect.Y, rect.Width, rect.Height);
        }

        /// <summary>
        /// 绘图模式下在指定地图矩形区域内设置图块信息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetTiles(int x, int y, int width, int height)
        {
            if (_currentTiles == null)
                return;
            if (_editableMapMode != EditableMapMode.DrawMode)
                return;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int dx = x + i;
                    int dy = y + j;
                    if (dx < _width && dy < _height)
                    {
                        int offsetX = i % GetSelectedTilesWidth();
                        int offsetY = j % GetSelectedTilesHeight();
                        Tile tile = (Tile)_currentTiles[offsetX, offsetY].Clone();
                        AddUndoTile(dx, dy,true);
                        _currentMapLayer[dx, dy] = tile;
                    }
                }
            }

            OnMapChanged(new MapChangedArgs(new Rectangle(x, y, width, height)));
        }

        /// <summary>
        /// 置一个范围内的图素为空
        /// </summary>
        /// <param name="rect"></param>
        public void SetEmpty(Rectangle rect)
        {
            SetEmpty(rect.X,rect.Y,rect.Width,rect.Height);
        }

        public void SetEmpty(int x, int y, int width, int height)
        {
            if (_editableMapMode != EditableMapMode.DrawMode)
                return;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int dx = x + i;
                    int dy = y + j;
                    if (dx < _width && dy < _height)
                    {
                        AddUndoTile(dx, dy, true);
                        _currentMapLayer[dx, dy] = null;
                        _blockLayer[dx, dy] = false;
                    }
                }
            }

            OnMapChanged(new MapChangedArgs(new Rectangle(x, y, width, height)));
        }

        public bool IsTileEqual(Tile tile1, Tile tile2)
        {
            if (tile1 == null && tile2 == null) return true;
            if (tile1 == null && tile2 != null) return false;
            if (tile1 != null && tile2 == null) return false;
            if (tile1.Tileset.ID == tile2.Tileset.ID && tile1.OffsetID == tile2.OffsetID)
                return true;
            return false;
        }

        /// <summary>
        /// 填充图素
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void FillTiles(int x, int y)
        {
            if (_currentTiles == null)
                return;
            if (_editableMapMode != EditableMapMode.DrawMode)
                return;

            Tile srcTile = _currentTiles[0, 0];
            Tile destTile = _currentMapLayer[x, y];

            if (IsTileEqual(srcTile, destTile))
                return;

            AddUndoTile(x, y, false);

            Stack stack = new Stack();
            stack.Push(new Point(x, y));
            _currentMapLayer[x, y] = srcTile.Clone();
            while (stack.Count != 0)
            {
                Point pt = (Point)stack.Pop();
                if (pt.X > 0)
                {
                    if (IsTileEqual(destTile, _currentMapLayer[pt.X - 1, pt.Y]))
                    {
                        stack.Push(new Point(pt.X - 1, pt.Y));
                        AddUndoTile(pt.X - 1, pt.Y, false);
                        _currentMapLayer[pt.X - 1, pt.Y] = srcTile.Clone();
                    }
                }

                if (pt.X < _width - 1)
                {
                    if (IsTileEqual(destTile, _currentMapLayer[pt.X + 1, pt.Y]))
                    {
                        stack.Push(new Point(pt.X + 1, pt.Y));
                        AddUndoTile(pt.X + 1, pt.Y, false);
                        _currentMapLayer[pt.X + 1, pt.Y] = srcTile.Clone();
                    }
                }

                if (pt.Y > 0)
                {
                    if (IsTileEqual(destTile, _currentMapLayer[pt.X, pt.Y - 1]))
                    {
                        stack.Push(new Point(pt.X, pt.Y - 1));
                        AddUndoTile(pt.X, pt.Y - 1, false);
                        _currentMapLayer[pt.X, pt.Y - 1] = srcTile.Clone();
                    }
                }

                if(pt.Y < _height - 1)
                {
                    if (IsTileEqual(destTile, _currentMapLayer[pt.X, pt.Y + 1]))
                    {
                        stack.Push(new Point(pt.X, pt.Y + 1));
                        AddUndoTile(pt.X, pt.Y + 1, false);
                        _currentMapLayer[pt.X, pt.Y + 1] = srcTile.Clone();
                    }
                }
            }

            stack = null;
            OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
        }

        /// <summary>
        /// 置指定地图格子内容为空
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetEmpty(int x, int y)
        {
            if (_editableMapMode == EditableMapMode.DrawMode)
            {
                AddUndoTile(x, y, true);
                _currentMapLayer[x, y] = null;
            }
            _blockLayer[x, y] = false;
            OnMapChanged(new MapChangedArgs(new Rectangle(x, y, 1, 1)));
        }

        /// <summary>
        /// 设置指定图层的可见性
        /// </summary>
        /// <param name="index"></param>
        /// <param name="visible"></param>
        public void SetLayerVisible(int index, bool visible)
        {
            if (index < 0) return;
            if (index >= _mapLayers.Count) return;

            _mapLayers[index].Visible = visible;
            OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
        }

        /// <summary>
        /// 设置指定图层名称
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        public void SetLayerName(int index, string name)
        {
            if (index < 0) return;
            if (index >= _mapLayers.Count) return;

            _mapLayers[index].Name = name;
        }

        /// <summary>
        /// 设置指定图层是否为背景
        /// </summary>
        /// <param name="index"></param>
        /// <param name="background"></param>
        public void SetLayerBackground(int index, bool background)
        {
            if (index < 0) return;
            if (index >= _mapLayers.Count) return;
            _mapLayers[index].BackgroundLayer = background;
        }

        /// <summary>
        /// 设置障碍物层的可见性
        /// </summary>
        /// <param name="visible"></param>
        public void SetBlockVisible(bool visible)
        {
            _blockLayer.Visible = visible;
            OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
        }

        /// <summary>
        /// 交换两个图层的位置
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public void SwapMapLayer(int index1, int index2)
        {
            if (index1 < 0 || index2 < 0) return;
            if (index1 >= _mapLayers.Count || index2 >= _mapLayers.Count) return;
            int temp = _mapLayers[index1].ZIndex;
            _mapLayers[index1].ZIndex = _mapLayers[index2].ZIndex;
            _mapLayers[index2].ZIndex = temp;
            _mapLayers.Sort();
            _currentMapLayer = _mapLayers[index2];
            OnMapChanged(new MapChangedArgs(new Rectangle(0, 0, _width, _height)));
        }

        /// <summary>
        /// 获取已选择图块的宽度
        /// </summary>
        /// <returns></returns>
        public int GetSelectedTilesWidth()
        {
            return _currentTiles != null ? _currentTiles.GetLength(0) : 0;
        }

        /// <summary>
        /// 获取已选择图块的高度
        /// </summary>
        /// <returns></returns>
        public int GetSelectedTilesHeight()
        {
            return _currentTiles != null ? _currentTiles.GetLength(1) : 0;
        }

        /// <summary>
        /// 响应地图已改变事件
        /// </summary>
        /// <param name="e"></param>
        private void OnMapChanged(MapChangedArgs e)
        {
            MapChangedEventHandle temp = MapChanged;
            if (temp != null)
                temp(e);
        }

        private void OnMapTileResized(MapTileResizeArgs e)
        {
            MapTileResizeEventHandler temp = MapTileResized;
            if (temp != null)
                temp(e);
        }

        public void AddUndoTile(int x, int y, bool checkExits)
        {
            _undoGroup.AddToGroup(new UndoTile(_currentMapLayer[x, y], x, y, _currentMapLayer.ID), checkExits);
        }

        public void ClearUndoGroup()
        {
            _undoGroup = new UndoGroup();
        }

        public UndoGroup GetUndoGroup()
        {
            return _undoGroup;
        }
    }
}
