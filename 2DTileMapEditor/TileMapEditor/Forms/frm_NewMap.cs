using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TileMapEditor.Forms
{
    public partial class frm_NewMap : Form
    {
        public event NewMapEventHandler MapChanged;
        public delegate NewMapEventArgs RequestMapInfo();

        public frm_NewMap()
        {
            InitializeComponent();

            SetupControls();
        }

        public void Initialize(RequestMapInfo request, bool newFlag)
        {
            NewMapEventArgs e = request();
            if (e != null)
            {
                txtMapName.Text = e.MapName;
                txtMapWidth.Value = (decimal)e.MapWidth;
                txtMapHeight.Value = (decimal)e.MapHeight;
                txtTileWidth.Value = (decimal)e.TileWidth;
                txtTileHeight.Value = (decimal)e.TileHeight;

                this.Text = newFlag ? "新建地图 - " + e.MapName : "编辑地图 - " + e.MapName;
            }
        }

        private void SetupControls()
        {
            txtMapName.PromptText = "请输入地图名称";
            txtMapWidth.Minimum = 1;
            txtMapWidth.Maximum = 128;
            txtMapHeight.Minimum = 1;
            txtMapHeight.Maximum = 128;
            txtTileWidth.Minimum = 1;
            txtTileWidth.Maximum = 4096;
            txtTileHeight.Minimum = 1;
            txtTileHeight.Maximum = 4096;

            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!txtMapName.Dirty)
            {
                MessageBox.Show("请输入地图名称!");
                return;
            }

            string name = txtMapName.Text.Trim();
            int width = (int)txtMapWidth.Value;
            int height = (int)txtMapHeight.Value;
            int tileWidth = (int)txtTileWidth.Value;
            int tileHeight = (int)txtTileHeight.Value;

            NewMapEventArgs args = new NewMapEventArgs(name,width,height,tileWidth,tileHeight);
            OnNewMap(args);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnNewMap(NewMapEventArgs e)
        {
            NewMapEventHandler temp = MapChanged;
            if (temp != null)
                temp(e);
        }
    }
}
