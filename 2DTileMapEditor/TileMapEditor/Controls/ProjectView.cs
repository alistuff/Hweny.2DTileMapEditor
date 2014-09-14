using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapLib;
using TileMapEditor.Forms;
using TileMapEditor.Controls.ControlsEx;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using TileMapEditor.Lib;
using TileMapDoc;

namespace TileMapEditor.Controls
{
    public partial class ProjectView : UserControl, IComponentSubject
    {
        private DocManager _docManager;
        private string _projectName;
        private DraggableTree _mapTree;
        private List<EditableMap> _maps;
        private EditableMap _currentMap;
        private List<IComponentObserver> _observers;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        public DraggableTree MapTreeData
        {
            get { return _mapTree; }
        }

        public DocManager DocManager
        {
            set { _docManager = value; }
        }

        public Map CurrentMap
        {
            get
            {
                return _currentMap;
            }
        }

        public ProjectView()
        {
            InitializeComponent();

            _maps = new List<EditableMap>();
            _currentMap = null;
            _observers = new List<IComponentObserver>();
            SetupControls();
        }

        public void Clear()
        {
            _maps.Clear();
            _currentMap = null;

            _mapTree.Nodes.Clear();
            AddNode(0, _projectName, "", true);

            NewMap("MAP001", 20, 20, 32, 32);
        }

        /// <summary>
        /// 加载地图列表
        /// </summary>
        /// <param name="fileName"></param>
        public void LoadMapList(string fileName)
        {
            try
            {
                _maps.Clear();
                foreach (TreeNode node in _mapTree.Nodes)
                {
                    if (node.Name != "")
                    {
                        string fileFullName = string.Format("{0}\\{1}{2}", fileName, node.Name, 
                            ApplicationConsts.TILEMAP_DATA_FILE_Extension);
                        AddMap(fileFullName);
                    }
                    LoadSubMapList(fileName, node);
                }

                if (_maps.Count > 0)
                {
                    if (_mapTree.SelectedNode != null && _mapTree.SelectedNode.Parent != null)
                    {
                        _currentMap = GetMapByID(Convert.ToInt32(_mapTree.SelectedNode.Tag));
                    }
                }
                Notification();
            }
            catch (Exception e) { throw e; }
        }

        /// <summary>
        /// 保存地图列表
        /// </summary>
        /// <param name="fileName"></param>
        public void SaveMapList(string fileName)
        {
            if (_maps.Count == 0) return;
            foreach (EditableMap map in _maps)
            {
                map.Save(string.Format("{0}\\{1}{2}", fileName,
                    "Map" + map.ID.ToString("000"),
                    ApplicationConsts.TILEMAP_DATA_FILE_Extension));
            }
        }

        /// <summary>
        /// 导出图块
        /// </summary>
        /// <param name="fileName"></param>
        public void OutputTilesetFile(string fileName)
        {
            if (_maps.Count == 0) return;
            foreach (EditableMap map in _maps)
            {
                map.SaveTileset(fileName);
            }
        }

        private void SetupControls()
        {
            this.Dock = DockStyle.Fill;

            _mapTree = new DraggableTree();
            _mapTree.Dock = DockStyle.Fill;
            _mapTree.BackColor = SystemColors.Control;
            _mapTree.Indent = _mapTree.Indent + 5;
            _mapTree.ImageList = imageList1;
            _mapTree.AfterSelect += new TreeViewEventHandler(_mapTree_AfterSelect);
            _mapTree.DragDropEvent += new EventHandler(_mapTree_DragDropEvent);

            contextMenu_NewMap.Click += new EventHandler(contextMenu_NewMap_Click);
            contextMenu_EditMap.Click += new EventHandler(contextMenu_EditMap_Click);
            contextMenu_Del.Click += new EventHandler(contextMenu_Del_Click);
            Application.Idle += new EventHandler(Application_Idle);

            this.Controls.Add(_mapTree);
        }

        /// <summary>
        /// 新建地图
        /// </summary>
        /// <param name="name">地图名</param>
        /// <param name="width">地图宽度</param>
        /// <param name="height">地图高度</param>
        /// <param name="tileWidth">地图区块宽度</param>
        /// <param name="tileHeight">地图区块高度</param>
        private void NewMap(string name, int width, int height, int tileWidth, int tileHeight)
        {
            if (_mapTree.SelectedNode != null)
            {
                int id = 1;
                foreach (EditableMap map in _maps)
                {
                    if (map.ID <= id)
                        id++;
                }
                EditableMap newMap = new EditableMap(id, name, width, height, tileWidth, tileHeight);
                _maps.Add(newMap);
                _maps.Sort();
                _currentMap = newMap;

                AddNode(id, name, "MAP" + id.ToString("000"), false);
                Notification();
            }
        }

        private void AddNode(int id, string text, string name, bool root)
        {
            int imageIndex = 0;
            int selectedNodeImageIndex = 0;
            if (!root)
            {
                imageIndex = 1;
                selectedNodeImageIndex = 2;
            }
            TreeNode newNode = new TreeNode(text, imageIndex, selectedNodeImageIndex);
            newNode.Name = name;
            newNode.Tag = id;
            newNode.ContextMenuStrip = contextMenuStrip1;
            if (!root)
            {
                _mapTree.SelectedNode.Nodes.Add(newNode);
                _mapTree.SelectedNode.Expand();
            }
            else
                _mapTree.Nodes.Add(newNode);
            _mapTree.SelectedNode = newNode;
        }

        private void AddMap(string fileName)
        {
            EditableMap newMap = new EditableMap();
            if (File.Exists(fileName))
            {
                newMap.Load(fileName, RequestTileset);
                _maps.Add(newMap);
            }
        }

        private Image RequestTileset(string fileName)
        {
            string fullName = Path.GetDirectoryName(_docManager.FilePath) + "\\" +
                ApplicationConsts.TILEMAP_GRAPHICS_TILESET_DIR + "\\" + fileName + ".png";
            if (File.Exists(fullName))
            {
                Stream fs = new FileStream(fullName, FileMode.Open, FileAccess.Read);
                Bitmap image = null;
                if (fs != null)
                {
                    image = new Bitmap(fs);
                }
                fs.Close();
                fs.Dispose();
                fs = null;
                return image;
            }
            throw new FileNotFoundException(fullName);
        }

        private void LoadSubMapList(string fileName, TreeNode node)
        {
            node.ContextMenuStrip = contextMenuStrip1;
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Name != "")
                {
                    string fileFullName = string.Format("{0}\\{1}{2}", fileName, childNode.Name, 
                        ApplicationConsts.TILEMAP_DATA_FILE_Extension);
                    AddMap(fileFullName);
                }
                LoadSubMapList(fileName, childNode);
            }
        }

        private void CommandNewMap()
        {
            frm_NewMap frm_NewMap = new frm_NewMap();
            frm_NewMap.Initialize(delegate()
            {
                int id = 1;
                if (_maps.Count > 0)
                    id = _maps[_maps.Count - 1].ID + 1;
                string newMapName = "MAP" + id.ToString("000");
                NewMapEventArgs e = new NewMapEventArgs(newMapName, 20, 20, 32, 32);
                return e;
            }, true);
            frm_NewMap.MapChanged += delegate(NewMapEventArgs e)
            {
                NewMap(e.MapName, e.MapWidth, e.MapHeight, e.TileWidth, e.TileHeight);
                _docManager.IsDirty = true;
            };
            frm_NewMap.ShowDialog();
        }

        private void CommandEditMap()
        {
            if (_currentMap == null) return;
            frm_NewMap newMap = new frm_NewMap();
            newMap.Initialize(delegate()
            {
                NewMapEventArgs e = new NewMapEventArgs(
                    _mapTree.SelectedNode.Text, _currentMap.Width,
                    _currentMap.Height, _currentMap.TileWidth, _currentMap.TileHeight);
                return e;
            }, false);
            newMap.MapChanged += delegate(NewMapEventArgs e)
            {
                if (_mapTree.SelectedNode != null)
                {
                    _mapTree.SelectedNode.Text = e.MapName;
                    _currentMap.SetName(e.MapName);
                    _currentMap.SetSize(e.MapWidth, e.MapHeight);
                    _currentMap.SetTileSize(e.TileWidth, e.TileHeight);
                    Notification();
                    _docManager.IsDirty = true;
                }
            };
            newMap.ShowDialog();
        }

        private void CommandRemoveMap()
        {
            if (_mapTree.SelectedNode.Parent == null)
                return;
            if (DialogResult.Yes ==
                MessageBox.Show("此操作将删除当前地图文件以及子地图文件，是否确认删除?",
                "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                RemoveSubMap(_mapTree.SelectedNode);
                _mapTree.SelectedNode.Remove();
                _docManager.IsDirty = true;
            }
        }

        private void RemoveSubMap(TreeNode subNode)
        {
            if (subNode == null) return;
            _maps.Remove(GetMapByID(Convert.ToInt32(subNode.Tag)));
            foreach (TreeNode node in subNode.Nodes)
            {
                RemoveSubMap(node);
            }
        }

        private EditableMap GetMapByID(int id)
        {
            for (int i = 0; i < _maps.Count; i++)
            {
                if (id == _maps[i].ID)
                    return _maps[i];
            }
            return null;
        }

        #region Events

        private void _mapTree_DragDropEvent(object sender, EventArgs e)
        {
            _docManager.IsDirty = true;
        }

        private void _mapTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
                _currentMap = null;
            else
                _currentMap = GetMapByID(Convert.ToInt32(e.Node.Tag));
            Notification();
        }

        private void contextMenu_NewMap_Click(object sender, EventArgs e)
        {
            CommandNewMap();
        }

        private void contextMenu_EditMap_Click(object sender, EventArgs e)
        {
            CommandEditMap();
        }

        private void contextMenu_Del_Click(object sender, EventArgs e)
        {
            CommandRemoveMap();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (_mapTree.SelectedNode != null)
            {
                if (_mapTree.SelectedNode.Parent == null)
                {
                    contextMenu_EditMap.Enabled = false;
                    contextMenu_Del.Enabled = false;
                }
                else
                {
                    contextMenu_EditMap.Enabled = true;
                    contextMenu_Del.Enabled = true;
                }
            }
        }

        #endregion

        #region IProjectSubject 成员

        public void RegisterObserver(IComponentObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IComponentObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void Notification()
        {
            foreach (IComponentObserver observer in _observers)
                observer.Update(_currentMap);
        }

        #endregion
    }
}
