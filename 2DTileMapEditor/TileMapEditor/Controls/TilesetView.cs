using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapRenderer;
using TileMapLib;
using System.IO;
using TileMapEditor.Lib;
using TileMapEditor.Forms;
using TileMapDoc;

namespace TileMapEditor.Controls
{
    public partial class TilesetView : UserControl, IComponentObserver
    {
        #region fields
        private DocManager _docManager;
        private EditableMap _map;
        private Tileset _currentTileset;
        private List<Tileset> _tilesets;
        private TileSetRenderer _renderer;
        private ctl_ScrollCanvas _canvas;

        #endregion

        #region Properties

        public Color BackColour
        {
            get { return _renderer.BackColor; }
            set { _renderer.BackColor = value; }
        }

        public TileSetRendererStyle Style
        {
            get { return _renderer.Style; }
            set { _renderer.Style = value; }
        }

        public DocManager DocManager
        {
            set { _docManager = value; }
        }

        #endregion

        public TilesetView()
        {
            InitializeComponent();

            _currentTileset = null;
            _tilesets = new List<Tileset>();

            this.SuspendLayout();
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.None;

            _canvas = new ctl_ScrollCanvas();
            _canvas.Dock = DockStyle.Fill;
            _canvas.Canvas.ContextMenuStrip = contextMenuStrip1;

            _renderer = new GdiTilesetRenderer();

            _canvas.SetRenderer(_renderer);

            cbbTileset.SelectedIndexChanged += new EventHandler(cbbTileset_SelectedIndexChanged);
            _canvas.Canvas.MouseDown += new MouseEventHandler(_canvas_MouseDown);
            _canvas.Canvas.MouseMove += new MouseEventHandler(_canvas_MouseMove);
            _canvas.Canvas.MouseUp += new MouseEventHandler(_canvas_MouseUp);

            Application.Idle += new EventHandler(Application_Idle);

            this.tabPage1.Controls.Add(_canvas);
            this.cbbTileset.Items.Add("AddTileset...");
            this.ResumeLayout();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (_tilesets.Count > 0)
            {
                contextMenu_Del.Enabled = true;
                contextMenu_DelAll.Enabled = true;
            }
            else
            {
                contextMenu_Del.Enabled = false;
                contextMenu_DelAll.Enabled = false;
            }
        }

        public void Update(EditableMap mapData)
        {
            if (mapData != null)
            {
                _map = mapData;
                SetTilesets(_map.Tilesets);
                setSelectedTiles(null);
                this.Visible = true;
            }
            else
            {
                _map = null;
                setCurrentTileset(null);
                this.Visible = false;
            }
        }

        public void Clear()
        {
            _currentTileset = null;
            _tilesets.Clear();
            _tilesets = null;
            _renderer.Clear();
            _renderer = null;
            _canvas.Dispose();
            _canvas = null;
        }

        #region private functions

        private void SetTilesets(List<Tileset> tilesets)
        {
            _tilesets = tilesets;
            cbbTileset.SuspendLayout();
            cbbTileset.Items.Clear();
            cbbTileset.Text = "";
            foreach (Tileset tileset in _tilesets)
                cbbTileset.Items.Add(tileset);
            if (_tilesets.Count > 0)
                setCurrentTileset(_tilesets[0]);
            else
                setCurrentTileset(null);
            cbbTileset.Items.Add("AddTileset...");
            cbbTileset.ResumeLayout();
        }

        private void loadTilesetDialog()
        {
            try
            {
                Forms.frm_NewTileset newTileset = new TileMapEditor.Forms.frm_NewTileset();
                newTileset.NewTileset += delegate(NewTilesetEventArgs e)
                {
                    string filePath = e.FilePath;
                    Bitmap image = new Bitmap(filePath);
                    if (image != null)
                    {
                        Bitmap png = Utilities.BitmapToPng32Argb(image, e.TransparentColor);
                        if (png != null)
                        {
                            int id = 0;
                            foreach (Tileset ts in _tilesets)
                            {
                                if (ts.ID <= id)
                                    id++;
                            }
                            Tileset tileset = new Tileset(id, e.Name, e.TileWidth, e.TileHeight,
                                (Image)png.Clone(), e.TransparentColor);
                            addTileset(tileset);
                            SetTilesets(_tilesets);
                            setCurrentTileset(_tilesets[_tilesets.Count - 1]);
                            png.Dispose();
                            png = null;
                        }
                        image.Dispose();
                        image = null;
                    }
                };
                newTileset.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + ",无效的图块文件!");
            }
        }

        private void setCurrentTileset(Tileset tileset)
        {
            _currentTileset = tileset;
            _renderer.Tileset = _currentTileset;
            _renderer.RealSelectedArea = Rectangle.Empty;

            if (tileset != null)
            {
                cbbTileset.SelectedItem = tileset;
                _canvas.SetScrollCanvasContext(_currentTileset.Width, _currentTileset.Height, 
                    _currentTileset.TileWidth, _currentTileset.TileHeight);
                _canvas.SetOffset(0, 0);
            }
            else
            {
                _canvas.SetScrollCanvasContext(0, 0, 0, 0);
                _canvas.SetOffset(0, 0);
            }
        }

        private void addTileset(Tileset tileset)
        {
            if (tileset == null) return;
            foreach (Tileset ts in _tilesets)
            {
                if (ts.Name.Equals(tileset.Name))
                    return;
            }

            _tilesets.Add(tileset);
            _tilesets.Sort();
            _docManager.IsDirty = true;
        }

        private int getOffset(int x, int y)
        {
            int dx = x / _currentTileset.TileWidth;
            int dy = y / _currentTileset.TileHeight;
            int width = _currentTileset.Width / _currentTileset.TileWidth;

            return dy * width + dx;
        }

        private void updateStatusMessage(Point pt)
        {
            status_Pos.Text = string.Format("({0},{1})  ({2},{3})", 
                pt.X / _currentTileset.TileWidth, pt.Y / _currentTileset.TileHeight, pt.X, pt.Y);
            status_offset.Text = string.Format("Offset:{0}", getOffset(pt.X, pt.Y));
        }

        private Point adjustMouseCoordinate(Point pt)
        {
            int mouseX = pt.X;
            int mouseY = pt.Y;

            mouseX = Math.Max(0, Math.Min(pt.X + _renderer.OffsetX, _currentTileset.Width - 1));
            mouseY = Math.Max(0, Math.Min(pt.Y + _renderer.OffsetY, _currentTileset.Height - 1));

            return new Point(mouseX, mouseY);
        }

        private Tile[,] getSelectedTiles()
        {
            Tile[,] tiles = null;
            //当前存在图块
            if (_currentTileset != null)
            {
                //当前已选择图素
                Rectangle realSelectedArea = _renderer.RealSelectedArea;
                if (realSelectedArea != Rectangle.Empty)
                {
                    int width = (int)Math.Ceiling((double)realSelectedArea.Width / _currentTileset.TileWidth);
                    int height = (int)Math.Ceiling((double)realSelectedArea.Height / _currentTileset.TileHeight);

                    tiles = new Tile[width, height];
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            int posX = realSelectedArea.Left + (x * _currentTileset.TileWidth);
                            int posY = realSelectedArea.Top + (y * _currentTileset.TileHeight);

                            tiles[x, y] = new Tile(_currentTileset, getOffset(posX, posY));
                        }
                    }
                }
            }

            return tiles;
        }

        private void setSelectedTiles(Tile[,] tiles)
        {
            _map.SetSelectedTiles(tiles);
        }

        #endregion

        #region event

        void cbbTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTileset.SelectedIndex == -1) return;
            if (cbbTileset.SelectedItem.GetType() == typeof(string))
                loadTilesetDialog();
            else
                setCurrentTileset((Tileset)cbbTileset.SelectedItem);
            setSelectedTiles(null);
        }

        void _canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (_currentTileset != null &&
               e.Button == MouseButtons.Left)
            {
                _renderer.StartPoint = new Point(e.X, e.Y);
                _renderer.IsSelected = true;
            }
        }

        void _canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_currentTileset != null)
            {
                updateStatusMessage(adjustMouseCoordinate(new Point(e.X, e.Y)));
                if (e.Button == MouseButtons.Left)
                {
                    _renderer.EndPoint = new Point(e.X, e.Y);
                }
            }
        }

        void _canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_currentTileset != null &&
                e.Button == MouseButtons.Left)
            {
                _renderer.IsSelected = false;
                _renderer.EndPoint = new Point(e.X, e.Y);

                setSelectedTiles(getSelectedTiles());
            }
        }

        private void contextMenu_AddTileset_Click(object sender, EventArgs e)
        {
            loadTilesetDialog();
        }

        private void contextMenu_Del_Click(object sender, EventArgs e)
        {
            if (_currentTileset != null)
            {
                DialogResult result = MessageBox.Show("确定删除图块吗?", "提示",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    _tilesets.Remove(_currentTileset);
                    SetTilesets(_tilesets);

                    _docManager.IsDirty = true;
                }
            }
        }

        private void contextMenu_DelAll_Click(object sender, EventArgs e)
        {
            if (_currentTileset != null)
            {
                DialogResult result = MessageBox.Show("确定清空所有图块吗?", "提示",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    _tilesets.Clear();
                    SetTilesets(_tilesets);

                    _docManager.IsDirty = true;
                }
            }
        }

        #endregion
    }
}