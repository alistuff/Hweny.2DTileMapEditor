using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapLib;
using TileMapEditor.Lib;
using TileMapEditor.Forms;
using TileMapDoc;

namespace TileMapEditor.Controls
{
    public partial class MapSettingView : UserControl, IComponentObserver
    {
        private DocManager _docManager;
        private EditableMap _map;
        private ctl_EditMapLayer _editMapLayer;
        public event EventHandler ShowGridEvent;
        private ctl_EditHistory _editHistory;

        public bool ShowGrid
        {
            get { return cbShowGrid.Checked; }
            set { cbShowGrid.Checked = value; }
        }

        public DocManager DocManager
        {
            set { _docManager = value; }
        }

        public MapSettingView()
        {
            InitializeComponent();

            SetupControls();
        }

        public void Update(EditableMap mapData)
        {
            if (mapData == null)
            {
                this.Visible = false;
                return;
            }
            this.Visible = true;
            _map = mapData;
            _editMapLayer.Update(_map);
        }

        private void SetupControls()
        {
            Dock = DockStyle.Fill;
            _editMapLayer = new ctl_EditMapLayer();
            _editMapLayer.Dock = DockStyle.Fill;
            tabPageLayer.Controls.Add(_editMapLayer);

            _editHistory = new ctl_EditHistory();
            _editHistory.Dock = DockStyle.Fill;
            tabPageHistory.Controls.Add(_editHistory);

            _editMapLayer.Dirty
                += delegate(object sender, EventArgs e) { SetDirty(); };

            rbtnDrawMode.Checked = true;
            rbtnEditBlock.Checked = true;

            cbShowGrid.Checked = true;
            cbShowBlock.Checked = true;
            cbShowEvent.Checked = true;

            rbtnDrawMode.CheckedChanged += new EventHandler(rbtnDrawMode_CheckedChanged);
            rbtnEditMode.CheckedChanged += new EventHandler(rbtnEditMode_CheckedChanged);
            cbShowBlock.CheckedChanged += new EventHandler(cbShowBlock_CheckedChanged);
            cbShowGrid.Click += new EventHandler(cbShowGrid_Click);
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void SetDirty()
        {
            if (_docManager != null)
                _docManager.IsDirty = true;
        }

        private void cbShowGrid_Click(object sender, EventArgs e)
        {
            EventHandler temp = ShowGridEvent;
            if (temp != null)
                temp(sender, e);
        }

        private void cbShowBlock_CheckedChanged(object sender, EventArgs e)
        {
            _map.SetBlockVisible(cbShowBlock.Checked);
        }

        private void rbtnEditMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnEditMode.Checked)
            {
                _map.MapMode = EditableMapMode.EditMode;
                if (cbShowBlock.Checked)
                    _map.SetBlockVisible(true);
                else
                    _map.SetBlockVisible(false);
            }
        }

        private void rbtnDrawMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDrawMode.Checked)
            {
                _map.MapMode = EditableMapMode.DrawMode;
                _map.SetBlockVisible(false);
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (rbtnDrawMode.Checked)
            {
                cbShowBlock.Enabled = false;
             //   cbShowEvent.Enabled = false;
                rbtnEditBlock.Enabled = false;
              //  rbtnEditEvent.Enabled = false;
            }
            else
            {
                cbShowBlock.Enabled = true;
               // cbShowEvent.Enabled = true;
                rbtnEditBlock.Enabled = true;
              //  rbtnEditEvent.Enabled = true;
            }
        }
    }
}
