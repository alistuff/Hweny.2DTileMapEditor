using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TileMapEditor.Test.Setting
{
    public partial class frm_CharSet : Form
    {
        public event EventHandler<CharSetEventArgs> Confirm;
        private int lastIndex = -1;

        public frm_CharSet()
        {
            InitializeComponent();

            LoadCharSets();
            lsbCharset.SelectedIndexChanged += new EventHandler(lsbCharset_SelectedIndexChanged);
            Application.Idle += new EventHandler(Application_Idle);
        }

        public void Initialize(string currentCharset)
        {
            lsbCharset.SelectedItem = currentCharset;
        }

        private void LoadCharSets()
        {
            try
            {
                string dirPath = Application.StartupPath + "\\" + ApplicationConsts.TILEMAP_GAME_CHARSET_DIR;
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                    return;
                }

                FileInfo[] files = new DirectoryInfo(dirPath).GetFiles("*.png");
                if (files.Length == 0)
                    return;

                lsbCharset.Items.Clear();
                foreach (FileInfo file in files)
                {
                    lsbCharset.Items.Add(file.Name);
                }
                lsbCharset.SelectedIndex = 0;
                lastIndex = 0;
                UpdateView();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void UpdateView()
        {
            try
            {
                string fullName = string.Format("{0}\\{1}\\{2}",
                    Application.StartupPath,
                    ApplicationConsts.TILEMAP_GAME_CHARSET_DIR,
                    lsbCharset.SelectedItem.ToString()
                );

                if (picCharSet.Image != null)
                {
                    picCharSet.Image.Dispose();
                    picCharSet.Image = null;
                }

                picCharSet.Image = Image.FromFile(fullName);
                lblCharSetInfo.Text = string.Format("width:{0} height:{1}", picCharSet.Width, picCharSet.Height);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void lsbCharset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbCharset.SelectedIndex == -1)
                return;
            if (lastIndex == lsbCharset.SelectedIndex)
                return;

            lastIndex = lsbCharset.SelectedIndex;
            UpdateView();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Confirm != null)
                Confirm(this, new CharSetEventArgs(lsbCharset.SelectedItem.ToString()));
            this.Close();
        }
       
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFilesPath_Click(object sender, EventArgs e)
        {
            string dirPath = Application.StartupPath + "\\" + ApplicationConsts.TILEMAP_GAME_CHARSET_DIR;
            System.Diagnostics.Process.Start(dirPath);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            btnConfirm.Enabled = lsbCharset.SelectedIndex != -1;
        }
    }

    public class CharSetEventArgs : EventArgs
    {
        public string FileName
        {
            get;
            private set;
        }
        public CharSetEventArgs(string fileName)
        {
            this.FileName = fileName;
        }
    }
}
