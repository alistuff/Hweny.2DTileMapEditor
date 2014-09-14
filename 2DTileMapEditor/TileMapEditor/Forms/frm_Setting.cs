using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TileMapEditor.Lib;

namespace TileMapEditor.Forms
{
    public partial class frm_Setting : Form
    {
        public event EventHandler UpdateSetting;

        public frm_Setting()
        {
            InitializeComponent();

            SetupControl();
            PopulateData();
            LoadSetting();
        }

        private void SetupControl()
        {
            btnMruDefault.Click += new EventHandler(btnMruDefault_Click);
            btnTileSetDefault.Click += new EventHandler(btnTileSetDefault_Click);
            btnTileMapDefault.Click += new EventHandler(btnTileMapDefault_Click);
            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void PopulateData()
        {
            cbbMruFileCount.Items.Clear();
            for (int i = 1; i <= 20; i++)
                cbbMruFileCount.Items.Add(i.ToString());
            cbbMruFileCount.SelectedIndex = 0;

            cbbMruFileCharsLength.Items.Clear();
            for (int i = 50; i <= 100; i++)
                cbbMruFileCharsLength.Items.Add(i.ToString());
            cbbMruFileCharsLength.SelectedIndex = 0;

            cbbUndoLevel.Items.Clear();
            for (int i = 1; i <= 50; i++)
                cbbUndoLevel.Items.Add(i.ToString());
            cbbUndoLevel.SelectedIndex = 0;

            cbbBlockType.Items.Clear();
            cbbBlockType.Items.Add("ico_block");
            cbbBlockType.Items.Add("ico_block2");
            cbbBlockType.SelectedIndexChanged += new EventHandler(cbbBlockType_SelectedIndexChanged);
        }

        private void LoadSetting()
        {
            Setting setting = Setting.CreateInstance();

            cbbMruFileCount.Text = setting.MruFileCount.ToString();
            cbbMruFileCharsLength.Text = setting.MruFileCharsLength.ToString();
            cbbUndoLevel.Text = setting.UndoLevel.ToString();

            cbbTileSetGridColor.SelectedItem = setting.TileSetStyle.GridColor;
            cbbTileSetSelectedAreaColor.SelectedItem = setting.TileSetStyle.SelectedAreaColor1;
            ckbTileSetSolid1.Checked = setting.TileSetStyle.Solid1;
            txtTileSetTransparentColor1.Value = setting.TileSetStyle.TransparentColor1;
            cbbTileSetRealSelectedAreaColor.SelectedItem = setting.TileSetStyle.SelectedAreaColor2;
            ckbTileSetSolid2.Checked = setting.TileSetStyle.Solid2;
            txtTileSetTransparentColor2.Value = setting.TileSetStyle.TransparentColor2;
            ckbTileSetShowGrid.Checked = !setting.TileSetStyle.ShowGrid;

            cbbTileMapGridColor.SelectedItem = setting.TileMapStyle.GridColor;
            cbbTileMapSelectedAreaColor.SelectedItem = setting.TileMapStyle.SelectedAreaColor;
            cbbTileMapBackColor.SelectedItem = setting.TileMapStyle.BackColor;
            ckbTileMapSolid.Checked = setting.TileMapStyle.Solid;
            txtTileMapTransparentColor.Value = setting.TileMapStyle.TransparentColor;
            cbbBlockType.Text = setting.TileMapStyle.BlockType;
        }

        private void SaveSetting()
        {
            Setting setting = Setting.CreateInstance();

            setting.MruFileCount = int.Parse(cbbMruFileCount.Text);
            setting.MruFileCharsLength = int.Parse(cbbMruFileCharsLength.Text);
            setting.UndoLevel = int.Parse(cbbUndoLevel.Text);

            setting.TileSetStyle.GridColor = cbbTileSetGridColor.SelectedItem;
            setting.TileSetStyle.SelectedAreaColor1 = cbbTileSetSelectedAreaColor.SelectedItem;
            setting.TileSetStyle.Solid1 = ckbTileSetSolid1.Checked;
            setting.TileSetStyle.TransparentColor1 = (byte)txtTileSetTransparentColor1.Value;
            setting.TileSetStyle.SelectedAreaColor2 = cbbTileSetRealSelectedAreaColor.SelectedItem;
            setting.TileSetStyle.Solid2 = ckbTileSetSolid2.Checked;
            setting.TileSetStyle.TransparentColor2= (byte)txtTileSetTransparentColor2.Value;
            setting.TileSetStyle.ShowGrid = !ckbTileSetShowGrid.Checked;

            setting.TileMapStyle.GridColor = cbbTileMapGridColor.SelectedItem;
            setting.TileMapStyle.SelectedAreaColor = cbbTileMapSelectedAreaColor.SelectedItem;
            setting.TileMapStyle.BackColor = cbbTileMapBackColor.SelectedItem;
            setting.TileMapStyle.Solid = ckbTileMapSolid.Checked;
            setting.TileMapStyle.TransparentColor = (byte)txtTileMapTransparentColor.Value;
            setting.TileMapStyle.BlockType = cbbBlockType.Text;           
        }

        #region events

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveSetting();
            if (UpdateSetting != null)
                UpdateSetting(sender, e);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMruDefault_Click(object sender, EventArgs e)
        {
            cbbMruFileCount.Text ="10";
            cbbMruFileCharsLength.Text = "50";
            cbbUndoLevel.Text = "20";
        }

        private void btnTileSetDefault_Click(object sender, EventArgs e)
        {
            cbbTileSetGridColor.SelectedItem = Color.DarkRed;

            cbbTileSetSelectedAreaColor.SelectedItem = Color.White;
            ckbTileSetSolid1.Checked = false;
            txtTileSetTransparentColor1.Value = 255;

            cbbTileSetRealSelectedAreaColor.SelectedItem = Color.Red;
            ckbTileSetSolid2.Checked = true;
            txtTileSetTransparentColor2.Value = 128;
        }

        private void btnTileMapDefault_Click(object sender, EventArgs e)
        {
            cbbTileMapGridColor.SelectedItem = Color.LightGray;
            cbbTileMapBackColor.SelectedItem = Color.Black;

            cbbTileMapSelectedAreaColor.SelectedItem = Color.LightGreen;
            ckbTileMapSolid.Checked = true;
            txtTileMapTransparentColor.Value = 128;

            cbbBlockType.Text = "ico_block";
            picBlockType.Image = (Image)TileMapEditor.Properties.Resources.ResourceManager.GetObject("ico_block");
        }

        private void cbbBlockType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbBlockType.SelectedIndex == -1) return;
            picBlockType.Image = (Image)TileMapEditor.Properties.Resources.ResourceManager.GetObject(cbbBlockType.Text);
        }

        #endregion
    }
}
