using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TileMapLib;
using TileMapRenderer;
using System.Drawing.Imaging;

namespace TileMapEditor.Forms
{
    public partial class frm_OutputPictureMap : Form
    {
        private EditableMap _currentMap;
        private Color _backColor;

        public frm_OutputPictureMap()
        {
            InitializeComponent();

            SetupControl();
        }

        public EditableMap Map
        {
            set
            {
                _currentMap = value;
                txtWidth.Value = _currentMap.RealWidth;
                txtHeight.Value = _currentMap.RealHeight;
            }
        }

        public new Color BackColor
        {
            set { _backColor = value; }
        }

        private void SetupControl()
        {
            txtOutputPath.PromptText = "请选择图片输出位置";
            txtOutputPath.Enabled = false;
            cbbFormat.SelectedIndex = 0;
            btnBrowse.Click += new EventHandler(btnBrowse_Click);
            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private ImageFormat GetFormat()
        {
            ImageFormat format = ImageFormat.Png;
            switch (cbbFormat.Text)
            {
                case "BMP":
                    format = ImageFormat.Bmp;
                    break;
                case "JPEG":
                    format = ImageFormat.Jpeg;
                    break;
                default:
                    format = ImageFormat.Png;
                    break;
            }
            return format;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() != DialogResult.OK)
                return;
            txtOutputPath.Text = folderDlg.SelectedPath;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!txtOutputPath.Dirty)
            {
                MessageBox.Show("请选择图片输出位置!");
                return;
            }

            string fileName = string.Format("{0}\\{1}.{2}",
                txtOutputPath.Text, _currentMap.Name, cbbFormat.Text.ToLower());

            int width = Convert.ToInt32(txtWidth.Value);
            int height = Convert.ToInt32(txtHeight.Value);

            OutputMap(fileName, width, height, GetFormat());

            MessageBox.Show("导出成功!");

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void OutputMap(string fileName, int width, int height, ImageFormat format)
        {
            Bitmap memoryBuffer = new Bitmap(_currentMap.RealWidth, _currentMap.RealHeight);
            Graphics memG = Graphics.FromImage(memoryBuffer);

            if (format != ImageFormat.Png)
            {
                memG.Clear(_backColor);
            }

            IntPtr target = memG.GetHdc();
            IntPtr source = Win32Gdi.CreateCompatibleDC(target);

            for (int i = 0; i < _currentMap.MapLayers.Count; i++)
            {
                MapLayer layer = _currentMap.MapLayers[i];
                for (int x = 0; x < _currentMap.Width; x++)
                {
                    for (int y = 0; y < _currentMap.Height; y++)
                    {
                        Tile tile = layer[x, y];
                        if (tile != null && !tile.IsEmpty)
                        {
                            int posX = tile.OffsetID % (tile.Tileset.Width / tile.Tileset.TileWidth);
                            int posY = tile.OffsetID / (tile.Tileset.Width / tile.Tileset.TileWidth);

                            Rectangle dest = new Rectangle(x * _currentMap.TileWidth, y * _currentMap.TileHeight,
                                _currentMap.TileWidth, _currentMap.TileHeight);
                            Rectangle src = new Rectangle(posX * tile.Tileset.TileWidth,
                                posY * tile.Tileset.TileHeight, tile.Tileset.TileWidth, tile.Tileset.TileHeight);

                            Win32Gdi.SelectObject(source, tile.Tileset.PtrImage);
                            Win32Gdi.AlphaBlend(target, dest, source, src,
                                       new Win32Gdi.BLENDFUNCTION(Win32Gdi.AC_SRC_OVER, 0,
                                           255, Win32Gdi.AC_SRC_ALPHA));
                        }
                    }
                }
            }

            Win32Gdi.DeleteDC(source);
            memG.ReleaseHdc(target);

            Bitmap newBitmap = new Bitmap(memoryBuffer, width, height);
            newBitmap.Save(fileName, format);
            memG.Dispose();
            memG = null;
            memoryBuffer.Dispose();
            memoryBuffer = null;
            newBitmap.Dispose();
            newBitmap = null;
        }
    }
}
