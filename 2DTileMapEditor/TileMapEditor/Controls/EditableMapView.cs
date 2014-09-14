using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapLib;
using TileMapRenderer;
using TileMapEditor.Lib;
using TileMapEditor.Forms;
using TileMapDoc;
using System.Collections;

namespace TileMapEditor.Controls
{
    public enum ToolStyle
    {
        Pointer,
        SelectBox,
        Pen,
        Fill,
        Rectangle,
        Eraser,
        None
    }

    public partial class EditableMapView : UserControl, IComponentObserver
    {
        public event ScrollCanvasEventHandler EditorScrollChanged;

        private DocManager _docManager;
        private EditableMap _currentMap;
        private MapRenderer _renderer;
        private ctl_ScrollCanvas _scrollPanel;
        private ToolStyle _toolStyle;
        private Tool[] _tools;
        private CommandManager _commandManager;
        private TileMapClipboard _clipboard;
        private Point _mousePt;

        public bool ShowGrid
        {
            get { return _renderer.ShowGrid; }
            set { _renderer.ShowGrid = value; }
        }

        public MapRendererStyle Style
        {
            get { return _renderer.Style; }
            set 
            { 
                _renderer.Style = value;
                Bitmap icon = (Bitmap)TileMapEditor.Properties.Resources.ResourceManager.GetObject(Style.BlockType);
                if (icon != null)
                    _renderer.BlockIcon = icon;
                else
                    _renderer.BlockIcon = TileMapEditor.Properties.Resources.ico_block;
            }
        }

        public Rectangle RealSelectedArea
        {
            get { return _renderer.RealSelectedArea; }
            set { _renderer.RealSelectedArea = value; }
        }

        public ToolStyle ToolStyle
        {
            get { return _toolStyle; }
            set { _toolStyle = value; ActiveClipboard(); RealSelectedArea = Rectangle.Empty; }
        }

        public bool HighLightMapLayer
        {
            get { return _currentMap.HighLightMapLayer; }
            set { _currentMap.HighLightMapLayer = value; }
        }

        public EditableMapMode MapMode
        {
            get { return _currentMap.MapMode; }
        }

        public DocManager DocManager
        {
            set { _docManager = value; }
        }

        public EditableMapView()
        {
            InitializeComponent();

            this.SuspendLayout();
            Dock = DockStyle.Fill;
            BackColor = SystemColors.ControlDarkDark;
            ContextMenuStrip = contextMenu_SelectBox;

            _scrollPanel = new ctl_ScrollCanvas();
            _scrollPanel.Dock = DockStyle.Fill;

            // _renderer = new GdiMapRenderer();
            _renderer = new Win32GdiMapRenderer();
            _scrollPanel.SetRenderer(_renderer);

            this.ShowGrid = true;

            _scrollPanel.Canvas.MouseDown += new MouseEventHandler(Canvas_MouseDown);
            _scrollPanel.Canvas.MouseMove += new MouseEventHandler(Canvas_MouseMove);
            _scrollPanel.Canvas.MouseUp += new MouseEventHandler(Canvas_MouseUp);
            _scrollPanel.SrollCavasUpdate += new ScrollCanvasEventHandler(_scrollPanel_SrollCavasUpdate);

            this.Controls.Add(_scrollPanel);
            this.ResumeLayout();

            _toolStyle = ToolStyle.Pointer;

            _tools = new Tool[(int)ToolStyle.None];
            _tools[0] = new ToolPointer();
            _tools[1] = new ToolSelectBox();
            _tools[2] = new ToolPen();
            _tools[3] = new ToolFill();
            _tools[4] = new ToolRectangle();
            _tools[5] = new ToolEraser();

            toolItem_Cut.Click += new EventHandler(toolItem_Cut_Click);
            toolItem_Copy.Click += new EventHandler(toolItem_Copy_Click);
            toolItem_Paster.Click += new EventHandler(toolItem_Paster_Click);
            toolItem_Del.Click += new EventHandler(toolItem_Del_Click);
            toolItem_SelectAll.Click += new EventHandler(toolItem_SelectAll_Click);
            toolItem_Cancel.Click += new EventHandler(toolItem_Cancel_Click);
            Application.Idle += new EventHandler(Application_Idle);
        }

        public void Update(EditableMap mapData)
        {
            if (mapData == null)
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
            _currentMap = mapData;
            _scrollPanel.SetScrollCanvasContext(
                _currentMap.RealWidth, _currentMap.RealHeight, _currentMap.TileWidth, _currentMap.TileHeight);
            _scrollPanel.SetOffset(0,0);
            _renderer.Map = _currentMap;
            _renderer.RealSelectedArea = Rectangle.Empty;
            _renderer.TileSize = new Size(_currentMap.TileWidth, _currentMap.TileHeight);

            _commandManager = CommandManager.CreateInstance();
            _commandManager.ClearHistory();
            _clipboard = new TileMapClipboard(_currentMap);
        }

        public void Clear()
        {
            _currentMap = null;
            _renderer.Clear();
            _renderer = null;
        }

        private Point AdjustMouseCoordinate(Point pt)
        {
            int mouseX = pt.X;
            int mouseY = pt.Y;

            mouseX = Math.Max(0, Math.Min(pt.X + _renderer.OffsetX, _renderer.RealWidth - 1));
            mouseY = Math.Max(0, Math.Min(pt.Y + _renderer.OffsetY, _renderer.RealHeight - 1));

            return new Point(mouseX / _renderer.TileSize.Width, mouseY / _renderer.TileSize.Height);
        }

        public void SetRendererMouseStartPoint(int x, int y)
        {
            if (_renderer != null)
                _renderer.StartPoint = new Point(x, y);
        }

        public void SetRendererMouseEndPoint(int x, int y)
        {
            if (_renderer != null)
                _renderer.EndPoint = new Point(x, y);
        }

        public Size GetSelectedTilesSize()
        {
            if (_currentMap != null)
            {
                int width, height;
                width = _currentMap.GetSelectedTilesWidth();
                height = _currentMap.GetSelectedTilesHeight();
                width = width == 0 ? 1 : width;
                height = height == 0 ? 1 : height;
                //
                return new Size((width - 1) * _renderer.TileSize.Width, (height - 1) * _renderer.TileSize.Height);
            }
            return Size.Empty;
        }

        public void SetTiles(int x, int y)
        {
            Point pt = AdjustMouseCoordinate(new Point(x, y));
            _currentMap.SetTiles(pt.X, pt.Y);
        }

        public void SetTiles(Rectangle rect)
        {
            _currentMap.SetTiles(new Rectangle(
                rect.X / _renderer.TileSize.Width,
                rect.Y / _renderer.TileSize.Height,
                rect.Width / _renderer.TileSize.Width,
                rect.Height / _renderer.TileSize.Height));
        }

        public void SetEmpty(Rectangle rect)
        {
            _currentMap.SetEmpty(new Rectangle(
                rect.X / _renderer.TileSize.Width,
                rect.Y / _renderer.TileSize.Height,
                rect.Width / _renderer.TileSize.Width,
                rect.Height / _renderer.TileSize.Height));
        }

        public void SetTileEmpty(int x, int y)
        {
            Point pt = AdjustMouseCoordinate(new Point(x, y));
            _currentMap.SetEmpty(pt.X, pt.Y);
        }

        public void FillTiles(int x, int y)
        {
            Point pt = AdjustMouseCoordinate(new Point(x, y));
            _currentMap.FillTiles(pt.X, pt.Y);
        }

        public void SetZoomIn()
        {
            int tileWidth = _renderer.TileSize.Width << 1;
            int tileHeight = _renderer.TileSize.Height << 1;
            if (tileWidth / _currentMap.TileWidth == 16)
                return;

            _renderer.TileSize = new Size(tileWidth, tileHeight);
            AdjustScrollCanvas();
        }

        public void SetZoomOut()
        {
            int tileWidth = _renderer.TileSize.Width >> 1;
            int tileHeight = _renderer.TileSize.Height >> 1;

            tileWidth = tileWidth == 0 ? 1 : tileWidth;
            tileHeight = tileHeight == 0 ? 1 : tileHeight;

            _renderer.TileSize = new Size(tileWidth, tileHeight);
            AdjustScrollCanvas();
        }

        public void SetZoom(int rate, bool zoomIn)
        {
            int tileWidth = _currentMap.TileWidth;
            int tileHeight = _currentMap.TileHeight;
            if (zoomIn)
            {
                tileWidth = tileWidth << rate;
                tileHeight = tileHeight << rate;
            }
            else
            {
                tileWidth = tileWidth >> rate;
                tileHeight = tileHeight >> rate;
                tileWidth = tileWidth == 0 ? 1 : tileWidth;
                tileHeight = tileHeight == 0 ? 1 : tileHeight;
            }
            _renderer.TileSize = new Size(tileWidth, tileHeight);
            AdjustScrollCanvas();
        }

        public void SetDefaultSize()
        {
            _renderer.TileSize = new Size(_currentMap.TileWidth, _currentMap.TileHeight);
            AdjustScrollCanvas();
        }

        public void SetDirty()
        {
            _docManager.IsDirty = true;
        }

        private void AdjustScrollCanvas()
        {
            _scrollPanel.SetScrollCanvasContext(_renderer.RealWidth, _renderer.RealHeight,
                _renderer.TileSize.Width, _renderer.TileSize.Height);
            _renderer.RealSelectedArea = Rectangle.Empty;
        }

        public void SetScrollOffset(int offsetX, int offsetY)
        {
            _scrollPanel.SetOffset(offsetX, offsetY);
        }

        public void AddCommandToHistory(string cmdName)
        {
            UndoGroup undoGroup = _currentMap.GetUndoGroup();
            if (undoGroup.Count == 0) return;

            CommandSetTile command = new CommandSetTile(cmdName, _currentMap);
            command.SaveChange(undoGroup);
            _commandManager.AddCommand(command);
            _currentMap.ClearUndoGroup();
        }

        public bool CanUndo
        {
            get
            {
                if (_commandManager != null)
                {
                    return _commandManager.CanUndo;
                }
                return false;
            }
        }

        public bool CanRedo
        {
            get
            {
                if (_commandManager != null)
                {
                    return _commandManager.CanRedo;
                }
                return false;
            }
        }

        public void ActiveClipboard()
        {
            Rectangle rect = new Rectangle(
           RealSelectedArea.X / _renderer.TileSize.Width,
           RealSelectedArea.Y / _renderer.TileSize.Height,
           RealSelectedArea.Width / _renderer.TileSize.Width,
           RealSelectedArea.Height / _renderer.TileSize.Height);

            _clipboard.SelectArea = rect;
        }

        public bool CanCut
        {
            get { return _clipboard.CanCut; }
        }

        public bool CanCopy
        {
            get { return _clipboard.CanCopy; }
        }

        public bool CanPaster
        {
            get { return _clipboard.CanPaster; }
        }

        public void Cut()
        {
            if (_toolStyle != ToolStyle.SelectBox) return;

            _clipboard.Cut();

            SetEmpty(RealSelectedArea);

            AddCommandToHistory("剪切");

            RealSelectedArea = Rectangle.Empty;

            SetDirty();
        }

        public void Copy()
        {
            if (_toolStyle != ToolStyle.SelectBox) return;

            _clipboard.Copy();

            RealSelectedArea = Rectangle.Empty;
        }

        public void Paster()
        {
            if (RealSelectedArea == Rectangle.Empty)
            {
                _clipboard.Paster(0,0);
            }
            else
            {
                Point pt = new Point(RealSelectedArea.X/_renderer.TileSize.Width,
                    RealSelectedArea.Y/_renderer.TileSize.Height);
                _clipboard.Paster(pt.X, pt.Y);
            }

            AddCommandToHistory("粘贴");

            RealSelectedArea = Rectangle.Empty;

            SetDirty();
        }

        public void Del()
        {
            SetEmpty(RealSelectedArea);

            AddCommandToHistory("删除");

            RealSelectedArea = Rectangle.Empty;

            SetDirty();
        }

        public void OutputMap()
        {
            frm_OutputPictureMap output = new frm_OutputPictureMap();
            output.Map = _currentMap;
            output.BackColor = Style.BackColor;
            output.ShowDialog();
        }

        #region Events

        private void _scrollPanel_SrollCavasUpdate(object sender, ScrollCanvasEventArgs e)
        {
            if (_currentMap != null && EditorScrollChanged != null)
            {
                EditorScrollChanged(sender, e);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_currentMap != null)
            {
                Point pt = AdjustMouseCoordinate(new Point(e.X, e.Y));
                if (_currentMap.MapMode == EditableMapMode.DrawMode)
                {
                    _tools[(int)_toolStyle].OnMouseMove(this, e);

                    _mousePt = pt;
                }
                else
                {
                    _tools[(int)ToolStyle.Pen].OnMouseDown(this, e);
                }
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (_currentMap != null)
            {
                Point pt = AdjustMouseCoordinate(new Point(e.X, e.Y));

                if (_currentMap.MapMode == EditableMapMode.DrawMode)
                    _tools[(int)_toolStyle].OnMouseDown(this, e);
                else
                    _tools[(int)ToolStyle.Pen].OnMouseDown(this, e);
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_currentMap != null)
            {
                if (_currentMap.MapMode == EditableMapMode.DrawMode)
                    _tools[(int)_toolStyle].OnMouseUp(this, e);
                else
                    _tools[(int)ToolStyle.Pen].OnMouseDown(this, e);
            }
        }

        private void toolItem_SelectAll_Click(object sender, EventArgs e)
        {
            SetRendererMouseStartPoint(0,0);
            SetRendererMouseEndPoint(_renderer.RealWidth,_renderer.RealHeight);
        }

        private void toolItem_Del_Click(object sender, EventArgs e)
        {
            Del();
        }

        private void toolItem_Copy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolItem_Cut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolItem_Cancel_Click(object sender, EventArgs e)
        {
            RealSelectedArea = Rectangle.Empty;
        }

        private void toolItem_Paster_Click(object sender, EventArgs e)
        {
            Paster();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (_currentMap != null)
            {
                if (_toolStyle == ToolStyle.SelectBox)
                {
                    ContextMenuStrip = contextMenu_SelectBox;
                    toolItem_Cut.Enabled = CanCut;
                    toolItem_Copy.Enabled = CanCopy;
                    toolItem_Paster.Enabled = CanPaster;
                    toolItem_Del.Enabled = CanCut;
                }
                else
                {
                    this.ContextMenuStrip = null;
                }
            }
        }

       #endregion
    }
}