using System;
using System.Drawing;

namespace TileMapEditor
{
    //自定义用户事件参数

    public delegate void NewTilesetEventHandler(NewTilesetEventArgs e);
    public class NewTilesetEventArgs : EventArgs
    {
        private string _name;
        private string _filePath;
        private Color _transparentColor;
        private int _tileWidth;
        private int _tileHeight;

        public string Name
        {
            get { return _name; }
        }
        public string FilePath
        {
            get { return _filePath; }
        }
        public Color TransparentColor
        {
            get { return _transparentColor; }
        }
        public int TileWidth
        {
            get { return _tileWidth; }
        }
        public int TileHeight
        {
            get { return _tileHeight; }
        }

        public NewTilesetEventArgs(string name, string filePath, int tileWidth,int tileHeight,
            Color transparentColor)
        {
            _name = name;
            _filePath = filePath;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
            _transparentColor = transparentColor;
        }
    }

    public delegate void NewProjectEventHandler(NewProjectEventArgs e);
    public class NewProjectEventArgs : EventArgs
    {
        private string _projectTitle;
        private string _projectSaveFolder;
        private string _projectSavePath;
        public string ProjectTitle
        {
            get { return _projectTitle; }
        }
        public string ProjectSaveFolder
        {
            get { return _projectSaveFolder; }
        }
        public string ProjectSavePath
        {
            get { return _projectSavePath; }
        }
        public NewProjectEventArgs(string title, string folder, string savePath)
        {
            _projectTitle = title;
            _projectSaveFolder = folder;
            _projectSavePath = savePath;
        }
    }

    public delegate void NewMapEventHandler(NewMapEventArgs e);
    public class NewMapEventArgs:EventArgs
    {
        private string _mapName;
        private int _mapWidth;
        private int _mapHeight;
        private int _tileWidth;
        private int _tileHeight;
        public string MapName
        {
            get { return _mapName; }
        }
        public int MapWidth
        {
            get { return _mapWidth; }
        }
        public int MapHeight
        {
            get { return _mapHeight; }
        }
        public int TileWidth
        {
            get { return _tileWidth; }
        }
        public int TileHeight
        {
            get { return _tileHeight; }
        }
        public NewMapEventArgs(string name, int width, int height, int tileWidth, int tileHeight)
        {
            _mapName = name;
            _mapWidth = width;
            _mapHeight = height;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }
    }

    public delegate void MiniMapViewPortDragEventHandler(object sender, MiniMapViewPortDragEventArgs e);
    public class MiniMapViewPortDragEventArgs : EventArgs
    {
        private Point _viewPortPosition;
        public Point ViewPortPosition
        {
            get { return _viewPortPosition; }
        }
        public MiniMapViewPortDragEventArgs(Point position)
        {
            _viewPortPosition = position;
        }
    }

    public delegate void ScrollCanvasEventHandler(object sender, ScrollCanvasEventArgs e);
    public class ScrollCanvasEventArgs : EventArgs
    {
        private Rectangle _viewPort;
        private int _offsetX;
        private int _offsetY;
        public Rectangle ViewPort
        {
            get { return _viewPort; }
        }
        public int OffsetX
        {
            get { return _offsetX; }
        }
        public int OffsetY
        {
            get { return _offsetY; }
        }
        public ScrollCanvasEventArgs(Rectangle viewPort, int offsetX, int offsetY)
        {
            _viewPort = viewPort;
            _offsetX = offsetX;
            _offsetY = offsetY;
        }
        public ScrollCanvasEventArgs(Rectangle viewPort, Point offset)
        {
            _viewPort = viewPort;
            _offsetX = offset.X;
            _offsetY = offset.Y;
        }
        public override string ToString()
        {
            return ViewPort.ToString() + "  " + _offsetX + "," + _offsetY;
        }
    }

    public delegate void UndoEventHandler(object sender,UndoEvents e);
    public class UndoEvents : EventArgs
    {
        private int _undoIndex;
        public int UndoIndex
        {
            get { return _undoIndex; }
        }
        public UndoEvents(int undoIndex)
        {
            _undoIndex = undoIndex;
        }
    }
}
