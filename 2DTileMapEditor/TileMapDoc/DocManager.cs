using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;
using System.Globalization;
using System.Security;
using System.Security.AccessControl;

namespace TileMapDoc
{
    /// <summary>
    /// 文档管理类，负责处理文档新建、打开、保存逻辑
    /// </summary>
    public class DocManager
    {
        public event LoadFileEventHandler LoadFile;
        public event SaveFileEventHandler SaveFile;
        public event OpenFileEventHandler OpenFile;
        public event EventHandler NewFile;
        public event EventHandler DocumentChanged;

        private Form _owner;
        //文档路径
        private string _filePath;
        //文档所在目录名
        private string _fileDirectoryName;
        //新文档名
        private string _newDocName;
        //文档扩展名
        private string _docExtension;
        private string _fileDlgFilter;
        private string _fileDlgInitDirectory;
        private bool _isDirty;
        private bool _hasDocument;

        public enum SaveType { Save, SaveAs }

        public DocManager(Form owner)
        {
            _owner = owner;
            _filePath = "";
            _fileDlgFilter = "";
            _fileDlgInitDirectory = "";
            _isDirty = false;
            _hasDocument = false;
        }

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string FileDirectoryName
        {
            get { return _fileDirectoryName; }
            set { _fileDirectoryName = value; }
        }

        public string NewDocName
        {
            get { return _newDocName; }
            set { _newDocName = value; }
        }

        public string DocExtension
        {
            get { return _docExtension; }
            set { _docExtension = value; }
        }

        public string FileDlgFilter
        {
            get { return _fileDlgFilter; }
            set { _fileDlgFilter = value; }
        }

        public string FileDlgInitDirectory
        {
            get { return _fileDlgInitDirectory; }
            set { _fileDlgInitDirectory = value; }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public bool HasDocument
        {
            get { return _hasDocument; }
        }

        /// <summary>
        /// 新建文档
        /// </summary>
        /// <returns></returns>
        public bool NewDocument()
        {
            if (!CloseDocument())
                return false;

            OnNewFile(new EventArgs());

            _isDirty = false;
            _hasDocument = true;
            return true;
        }

        /// <summary>
        /// 关闭文档
        /// </summary>
        /// <returns></returns>
        public bool CloseDocument()
        {
            if (!_isDirty)
                return true;
            bool ret = false;
            DialogResult result = MessageBox.Show("将更改保存到当前工程目录下吗?", "提示",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                ret = true;
            else if (result == DialogResult.Cancel)
                ret = false;
            else if (result == DialogResult.Yes)
                ret = SaveDocument(SaveType.Save);
            return ret;
        }

        /// <summary>
        /// 打开文档
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool OpenDocument(string fileName)
        {
            if (!CloseDocument())
                return false;

            if (string.IsNullOrEmpty(fileName))
            {
                OpenFileDialog openDlg = new OpenFileDialog();
                openDlg.Filter = _fileDlgFilter;
                openDlg.InitialDirectory = _fileDlgInitDirectory;

                if (openDlg.ShowDialog(_owner) != DialogResult.OK)
                    return false;
                fileName = openDlg.FileName;
                _fileDlgInitDirectory = new FileInfo(fileName).DirectoryName;
            }
            try
            {
                _filePath = fileName;
                using (Stream fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
                {
                    IFormatter bf = new BinaryFormatter();
                    FileSerializationEventArgs e = new FileSerializationEventArgs(bf, fs, _filePath);
                    LoadFile(this, e);
                    if (!e.IsSucceeded)
                    {
                        OpenFile(this, new OpenFileEventArgs(_filePath, false));
                        return false;
                    }
                    OnDocumentChanged(new EventArgs());
                    _hasDocument = true;
                }
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                OpenFile(this, new OpenFileEventArgs(_filePath, false));
                return false;
            }
            _isDirty = false;
            OpenFile(this, new OpenFileEventArgs(_filePath, true));
            return true;
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool SaveDocument(SaveType type)
        {
            if (type == SaveType.SaveAs || string.IsNullOrEmpty(_filePath))
            {
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                if (!string.IsNullOrEmpty(_filePath))
                {
                    folderDlg.SelectedPath = Path.GetDirectoryName(_filePath);
                }
                if (folderDlg.ShowDialog() != DialogResult.OK)
                    return false;
                _fileDlgInitDirectory = folderDlg.SelectedPath;
                _filePath = _fileDlgInitDirectory + "//" + _fileDirectoryName + "//" +
                    string.Format("{0}.{1}", _newDocName, _docExtension);
            }
            try
            {
                string dirPath = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                using (Stream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
                {
                    IFormatter bf = new BinaryFormatter();
                    FileSerializationEventArgs e = new FileSerializationEventArgs(bf, fs, _filePath);
                    SaveFile(this, e);
                    if (!e.IsSucceeded)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            _isDirty = false;
            return true;
        }

        private void OnLoadFile(FileSerializationEventArgs e)
        {
            LoadFileEventHandler temp = LoadFile;
            if (temp != null)
                temp(this, e);
        }

        private void OnSaveFile(FileSerializationEventArgs e)
        {
            SaveFileEventHandler temp = SaveFile;
            if (temp != null)
                temp(this, e);
        }

        private void OnOpenFile(OpenFileEventArgs e)
        {
            OpenFileEventHandler temp = OpenFile;
            if (temp != null)
                temp(this, e);
        }

        private void OnNewFile(EventArgs e)
        {
            EventHandler temp = NewFile;
            if (temp != null)
                temp(this, e);
        }

        private void OnDocumentChanged(EventArgs e)
        {
            EventHandler temp = DocumentChanged;
            if (temp != null)
                temp(this, e);
        }

        /// <summary>
        /// 注册文件类型到注册表中
        /// </summary>
        /// <param name="info"></param>
        public void RegisterFileType(FileTypeInfo info)
        {
            try
            {
                string ext = string.Format(CultureInfo.InvariantCulture, "{0}",
                    info.Extension);
                
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(ext))
                {
                    key.SetValue(null, info.ProgramId);
                }
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(info.ProgramId))
                {
                    key.SetValue(null, info.FileDisplayName);
                }
                //版本信息
                string version = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}",
                    info.ProgramId, "CurVer");
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(version))
                {
                    key.SetValue(null, info.Version);
                }

                string icon = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}",
                    info.ProgramId, "DefaultIcon");
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(icon))
                {
                    key.SetValue(null, Application.ExecutablePath + ",0");
                }

                string cmdKey = string.Format(CultureInfo.InvariantCulture,
                    "{0}\\{1}\\{2}\\{3}", info.ProgramId, "shell", "open", "command");
                using (RegistryKey key = Registry.ClassesRoot.CreateSubKey(cmdKey))
                {
                    key.SetValue(null, Application.ExecutablePath + " \"%1\"");
                }
            }
            catch (ArgumentNullException e)
            {
                RegisterExceptionHandler(e.Message);
            }
            catch (SecurityException e)
            {
                RegisterExceptionHandler(e.Message);
            }
            catch (ArgumentException e)
            {
                RegisterExceptionHandler(e.Message);
            }
            catch (ObjectDisposedException e)
            {
                RegisterExceptionHandler(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                RegisterExceptionHandler(e.Message);
            }
            catch (IOException e)
            {
                RegisterExceptionHandler(e.Message);
            }
        }

        private void RegisterExceptionHandler(string message)
        {
            MessageBox.Show("注册文件类型失败:" + message);
        }
    }
}
