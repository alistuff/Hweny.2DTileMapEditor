using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using Microsoft.Win32;
using System.Globalization;
using System.Security;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace TileMapDoc
{
    /// <summary>
    /// mru类，实现最近最多使用的工程
    /// </summary>
    public class MruManager
    {
        private const string REGISTRY_ENTRY_NAME = "file";
        private const string REGISTRY_MRU_KEY = "Mru";
        private Form _owner;
        private ToolStripMenuItem _menuItem_Mru;
        private ToolStripMenuItem _menuItem_Mru_Parent;
        private int _maxMruFileCount;
        private int _maxMruDisplayNameLength;
        private string _registryPath;
        private List<string> _mruFiles;

        public event MruFileOpenEventHandler MruFileOpen;

        public int MaxMruFileCount
        {
            get { return _maxMruFileCount; }
            set 
            {
                _maxMruFileCount = value;
                _maxMruFileCount = _maxMruFileCount < 1 ? 1 : _maxMruFileCount;
                if (_mruFiles.Count > _maxMruFileCount)
                {
                    _mruFiles.RemoveRange(_maxMruFileCount - 1, 
                        _mruFiles.Count - _maxMruFileCount);
                }
            }
        }

        public int MaxMruDisplayNameLength
        {
            get { return _maxMruDisplayNameLength; }
            set 
            {
                _maxMruDisplayNameLength = value;
                _maxMruDisplayNameLength =
                    _maxMruDisplayNameLength < 10 ? 10 : _maxMruDisplayNameLength;
            }
        }

        /// <summary>
        /// 压缩字符串，隐藏部分用...表示
        /// </summary>
        /// <param name="pszOut"></param>
        /// <param name="pszSrc"></param>
        /// <param name="cchMax"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern bool PathCompactPathEx(
            StringBuilder pszOut, //返回经过压缩的字符串
            string pszSrc,        //原始字符串
            uint cchMax,          //字符长度，包括结束字符
            int dwFlags);         //0

        public MruManager()
        {
            _mruFiles = new List<string>();
            _registryPath = "";
            _owner = null;
            _menuItem_Mru = null;
            _menuItem_Mru_Parent = null;
            _maxMruFileCount = 10;
            _maxMruDisplayNameLength = 50;
        }

        public void Initialize(Form owner, ToolStripMenuItem menuItemMru,
            ToolStripMenuItem menuItemMruParent, string registryPath)
        {
            _owner = owner;
            _menuItem_Mru = menuItemMru;
            _menuItem_Mru_Parent = menuItemMruParent;
            _registryPath = registryPath;

            if (_registryPath.EndsWith("\\"))
                _registryPath += REGISTRY_MRU_KEY;
            else
                _registryPath += "\\" + REGISTRY_MRU_KEY;

            _menuItem_Mru_Parent.DropDownOpening
                += new EventHandler(_menuItem_Mru_Parent_DropDownOpening);
            _owner.Closing
                += new System.ComponentModel.CancelEventHandler(_owner_Closing);

            LoadMruList();
        }

        public void Add(string file)
        {
            Remove(file);
            if (_mruFiles.Count == _maxMruFileCount)
                _mruFiles.RemoveAt(_maxMruFileCount - 1);
            _mruFiles.Insert(0, file);
        }

        public void Remove(string file)
        {
            for (int i = 0; i < _mruFiles.Count; i++)
            {
                if (_mruFiles[i] == file)
                {
                    _mruFiles.RemoveAt(i);
                    break;
                }
            }
        }

        private void LoadMruList()
        {
            _mruFiles.Clear();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(_registryPath);
            if (key != null)
            {
                for (int i = 0; i < _maxMruFileCount; i++)
                {
                    string keyName = string.Format(CultureInfo.InvariantCulture,
                        "{0}{1}", REGISTRY_ENTRY_NAME, i.ToString());
                    string file = (string)key.GetValue(keyName, "");
                    if (file.Length == 0) break;
                    _mruFiles.Add(file);
                }
            }
        }

        private void _menuItem_Mru_Parent_DropDownOpening(object sender, EventArgs e)
        {
            _menuItem_Mru.DropDownItems.Clear();
            if (_mruFiles.Count == 0)
            {
                _menuItem_Mru.Enabled = false;
                return;
            }

            _menuItem_Mru.Enabled = true;
            ToolStripMenuItem item = null;
            for (int i = 0; i < _mruFiles.Count; i++)
            {
                item = new ToolStripMenuItem();
                item.Text = GetCompactPath(_mruFiles[i], _maxMruDisplayNameLength);
                item.Tag = i;
                item.Click += new EventHandler(item_Click);
                _menuItem_Mru.DropDownItems.Add(item);
            }
        }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                string fileName = _mruFiles[(int)item.Tag];
                if (fileName.Length > 0)
                {
                    MruFileOpenEventArgs args = new MruFileOpenEventArgs(fileName);
                    OnMruFileOpen(args);
                }
            }
        }

        private void _owner_Closing(object sender, CancelEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(_registryPath);
            if (key != null)
            {
                for (int i = 0; i < _maxMruFileCount; i++)
                {
                    string keyName = string.Format(CultureInfo.InvariantCulture,
                        "{0}{1}", REGISTRY_ENTRY_NAME, i.ToString());
                    key.DeleteValue(keyName, false);
                }
                for (int i = 0; i < _mruFiles.Count; i++)
                {
                    string keyName = string.Format(CultureInfo.InvariantCulture,
                        "{0}{1}", REGISTRY_ENTRY_NAME, i.ToString());
                    key.SetValue(keyName, _mruFiles[i]);
                }
            }
        }

        private string GetCompactPath(string str, int len)
        {
            StringBuilder pszOut = new StringBuilder(len * 2 + 2);
            if (PathCompactPathEx(pszOut, str, (uint)len, 0))
            {
                return pszOut.ToString();
            }
            return str;
        }

        private void OnMruFileOpen(MruFileOpenEventArgs e)
        {
            MruFileOpenEventHandler temp = MruFileOpen;
            if (temp != null)
                temp(this, e);
        }
    }
}
