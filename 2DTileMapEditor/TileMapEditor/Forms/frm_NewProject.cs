using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace TileMapEditor.Forms
{
    public partial class frm_NewProject : Form
    {
        public event NewProjectEventHandler NewProject;

        public frm_NewProject()
        {
            InitializeComponent();

            SetupControls();
        }

        private void SetupControls()
        {
            txtTitle.PromptText = "请输入标题";
            txtFolder.PromptText = "请输入文件夹名";
            txtFilePath.PromptText = "请选择工程保存位置";

            txtTitle.Text = "Project";
            txtFolder.Text = "Project";
            txtFilePath.Enabled = false;

            btnConfirm.Click += new EventHandler(btnConfirm_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            btnBrowse.Click += new EventHandler(btnBrowse_Click);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!txtTitle.Dirty)
            {
                MessageBox.Show("请输入标题!");
                return;
            }
            if (!txtFolder.Dirty)
            {
                MessageBox.Show("请输入文件夹名!");
                return;
            }
            if (!txtFilePath.Dirty)
            {
                MessageBox.Show("请选择工程保存位置!");
                return;
            }

            string title = txtTitle.Text.Trim();
            string folder = txtFolder.Text.Trim();
            string savePath = txtFilePath.Text.Trim();
            string fullPath = savePath + "\\" + folder;
            try
            {
                if (Directory.Exists(fullPath))
                {
                    if (MessageBox.Show("此路径已存在该目录是否进行覆盖操作?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) !=
                        DialogResult.Yes)
                        return;
                    Directory.Delete(fullPath, true);
                    Directory.CreateDirectory(fullPath);
                }
            }
            catch
            {

            }

            NewProjectEventArgs args = new NewProjectEventArgs(title, folder, savePath);
            OnNewProject(args);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog saveDialog = new FolderBrowserDialog();

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = saveDialog.SelectedPath;
            }
        }

        private void OnNewProject(NewProjectEventArgs e)
        {
            NewProjectEventHandler temp = NewProject;
            if (temp != null)
                temp(e);
        }
    }
}
