using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileMapEditor;

namespace TileMapEditor.Forms
{
    public partial class frm_NewTileset : Form
    {
        public event NewTilesetEventHandler NewTileset;

        public frm_NewTileset()
        {
            InitializeComponent();

            SetupControls();
        }

        private void SetupControls()
        {
            txtName.PromptText = "请输入图块名称";
            txtFileName.PromptText = "png\\bmp\\jpeg";
            txtFileName.Enabled = false;
            btnColorDialog.BackColor = Color.FromArgb(255, 0, 255);
            lblColor.Text = getColor(btnColorDialog.BackColor);

            txtTileWidth.Minimum = 1;
            txtTileHeight.Minimum = 1;

            txtTileWidth.Value = 32m;
            txtTileHeight.Value = 32m;

            btnBrowse.Click += new EventHandler(btnBrowse_Click);
            btnColorDialog.Click += new EventHandler(btnColorDialog_Click);
            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!txtName.Dirty)
            {
                MessageBox.Show("请输入图块名称!");
                return;
            }
            if (!txtFileName.Dirty)
            {
                MessageBox.Show("请选择图块文件!");
                return;
            }
            if (txtName.Dirty && txtFileName.Dirty)
            {
                string name = txtName.Text.Trim();
                string filePath = txtFileName.Text.Trim();
                int tileWidth = Convert.ToInt32(txtTileWidth.Value);
                int tileHeight = Convert.ToInt32(txtTileHeight.Value);
                Color transparentColor = Color.Empty;
                if (cbTransparentColor.Checked)
                    transparentColor = btnColorDialog.BackColor;
                NewTilesetEventArgs args = new NewTilesetEventArgs(name, filePath,
                    tileWidth, tileHeight, transparentColor);
                onNewTileset(args);
                txtName.Text = "";
                this.Close();
            }
        }

        void btnColorDialog_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnColorDialog.BackColor = colorDialog.Color;
                lblColor.Text = getColor(btnColorDialog.BackColor);
            }
        }

        void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "PNG (*.PNG)|*.PNG|BMP (*.BMP)|*.BMP|JPEG (*.JPG;*.JPEG)|*.JPG;*.JPEG";
            openDialog.RestoreDirectory = true;
            if (openDialog.ShowDialog() != DialogResult.OK)
                return;
            txtFileName.Text = openDialog.FileName;
            SetTileSizeMaxValue(openDialog.FileName);
            if (!txtName.Dirty)
            {
                txtName.Text = new System.IO.FileInfo(openDialog.FileName).Name;
            }
            openDialog.Dispose();
            openDialog = null;
        }

        void onNewTileset(NewTilesetEventArgs e)
        {
            NewTilesetEventHandler temp = NewTileset;
            if (temp != null)
                temp(e);
        }

        string getColor(Color color)
        {
            return string.Format("[A={0} R={1} G={2} B={3}]", color.A, color.R, color.G, color.B);
        }

        void SetTileSizeMaxValue(string fileName)
        {
            Image image = Image.FromFile(fileName);
            if (image != null)
            {
                txtTileWidth.Maximum = image.Width;
                txtTileHeight.Maximum = image.Height;
                image.Dispose();
                image = null;
            }
        }
    }
}
