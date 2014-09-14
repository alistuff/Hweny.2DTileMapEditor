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

namespace TileMapEditor.Controls
{
    public partial class MiniMapView : UserControl, IComponentObserver
    {
        public event MiniMapViewPortDragEventHandler ViewPortDrag;
        private MiniMapRenderer _renderer;
        private ctl_DoubleBufferCanvas _canvas;

        public Rectangle ViewPort
        {
            set { _renderer.ViewPort = value; }
        }

        public Point ViewPortPosition
        {
            set { _renderer.ViewPortPosition = value; }
        }

        public MiniMapView()
        {
            InitializeComponent();

            AutoScroll = false;
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.Fixed3D;

            _canvas = new ctl_DoubleBufferCanvas();
            _canvas.Dock = DockStyle.Fill;

            _renderer = new Win32GdiMiniMapRenderer();
            //_renderer = new GdiMiniMapRenderer();

            _canvas.SetRenderer(_renderer);

            this._canvas.SizeChanged += new EventHandler(_canvas_SizeChanged);
            this._canvas.MouseDown += new MouseEventHandler(_canvas_MouseDown);
            this._canvas.MouseMove += new MouseEventHandler(_canvas_MouseMove);
            this._canvas.MouseUp += new MouseEventHandler(_canvas_MouseUp);
            this.panel1.Controls.Add(_canvas);
        }

        public void Update(EditableMap mapData)
        {
            if (mapData == null)
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
            _renderer.Map = mapData;
        }

        private void _canvas_MouseUp(object sender, MouseEventArgs e)
        {
            _renderer.IsDrag = false;
            Cursor = Cursors.Default;
        }

        private void _canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_renderer.IsDrag)
            {
                _renderer.DragPosition = new Point(e.X, e.Y);
                OnViewPortDrag(new MiniMapViewPortDragEventArgs(_renderer.ViewPortPosition));
                Cursor = Cursors.SizeAll;
            }
        }

        private void _canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _renderer.IsDrag = true;
        }

        private void _canvas_SizeChanged(object sender, EventArgs e)
        {
            _renderer.MiniMapClientRect = _canvas.ClientRectangle;
        }

        private void OnViewPortDrag(MiniMapViewPortDragEventArgs e)
        {
            MiniMapViewPortDragEventHandler temp = ViewPortDrag;
            if (temp != null)
                temp(this, e);
        }
    }
}
