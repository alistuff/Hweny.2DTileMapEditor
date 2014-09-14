/* 
 * ///////////////////////////////////////////////////////////////////// 
 * Filename: Add License
 * Author  : [LI-Games.ALi][alistuff@163.com] 
 * Date    : 2014/7/14    
 * Resume  : 基于Tile的2D地图编辑器，支持创建多图层地图及编辑障碍物
 *  
 * ///////////////////////////////////////////////////////////////////// 
 * Modifiy History 
 *  
 * Date    :
 * Resume  :
 *  
 */

// github：https://github.com/alistuff/Hweny.2DTileMapEditor
// e-mail：alistuff@163.com 

//The MIT License (MIT)
//
//Copyright (c) 2014 alistuff
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TileMapEditor.Controls;
using TileMapEditor.Lib;
using TileMapDoc;
using System.Runtime.Serialization;
using System.IO;
using TileMapLib;
using System.Runtime.Serialization.Formatters.Binary;

namespace TileMapEditor.Forms
{
    public partial class frm_MainForm : Form
    {
        #region 控件对象

        //容器控件对象
        private SplitContainer _container1;
        private SplitContainer _container2;
        private SplitContainer _container3;
        private SplitContainer _container4;

        //地图编辑器用户控件对象
        private ProjectView _component_projectPanel;
        private TilesetView _component_tilesetPanel;
        private EditableMapView _component_mapPanel;
        private MiniMapView _component_miniMapPanel;
        private MapSettingView _component_mapSettingPanel;

        #endregion

        #region 工程文件管理对象

        private string _argument = "";

        private DocManager _docManager;
        private MruManager _mruManager;
        private DocDragDropManager _docDragDrop;

        private frm_Toolkit _toolKit;

        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }

        #endregion

        #region 主窗体

        public frm_MainForm()
        {
            InitializeComponent();
            Load += new EventHandler(frm_MainForm_Load);
        }

        private void frm_MainForm_Load(object sender, EventArgs e)
        {
            //初始化工程文件管理对象
            InitializeDocManager();
            //初始化控件
            SetupControls();
            //加载配置
            LoadSetting();
            //文件关联
            ShellOpen();
        }

        private void ShellOpen()
        {
            if (_argument.Length > 0)
                OpenDocument(_argument);
        }

        private void frm_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_docManager.CloseDocument())
            {
                e.Cancel = true;
            }
            else
            {
                if (_toolKit != null)
                {
                    _toolKit.Close();
                }
                Setting.CreateInstance().Save();
            }
        }

        #endregion

        #region 初始化配置信息

        private void LoadSetting()
        {
            Setting setting = Setting.CreateInstance();

            _mruManager.MaxMruFileCount = setting.MruFileCount;
            _mruManager.MaxMruDisplayNameLength = setting.MruFileCharsLength;
            _component_tilesetPanel.Style = setting.TileSetStyle;
            _component_mapPanel.Style = setting.TileMapStyle;

            CommandManager commandManager = CommandManager.CreateInstance();
            commandManager.SetCapacity(setting.UndoLevel);
        }

        #endregion

        #region 初始化控件对象

        private void SetupControls()
        {
            #region SuspendLayout

            _container1 = new SplitContainer();
            _container2 = new SplitContainer();
            _container3 = new SplitContainer();
            _container4 = new SplitContainer();

            _container1.Panel1.SuspendLayout();
            _container1.Panel2.SuspendLayout();
            _container1.SuspendLayout();
            _container2.SuspendLayout();
            _container3.Panel2.SuspendLayout();
            _container3.SuspendLayout();
            _container4.SuspendLayout();
            this.componentPanel.SuspendLayout();

            #endregion

            #region container1

            _container1.Name = "container1";
            _container1.BorderStyle = BorderStyle.FixedSingle;
            _container1.Dock = DockStyle.Fill;
            _container1.FixedPanel = FixedPanel.Panel1;
            _container1.Size = new Size(990, 531);
            _container1.SplitterDistance = 289;
            _container1.Panel1.Controls.Add(_container2);
            _container1.Panel2.Controls.Add(_container3);
            _container1.BackColor = SystemColors.Control;
            _container1.Visible = false;

            #endregion

            #region container2

            _container2.Name = "container2";
            _container2.BorderStyle = BorderStyle.FixedSingle;
            _container2.Dock = DockStyle.Fill;
            _container2.Orientation = Orientation.Horizontal;
            _container2.FixedPanel = FixedPanel.Panel2;
            _container2.Size = new Size(289, 531);
            _container2.SplitterDistance = 385;
            _container2.BackColor = SystemColors.Control;
            _container2.Panel1.BackColor = SystemColors.ControlDarkDark;

            #endregion

            #region container3

            _container3.Name = "container3";
            _container3.BorderStyle = BorderStyle.FixedSingle;
            _container3.Dock = DockStyle.Fill;
            _container3.FixedPanel = FixedPanel.Panel2;
            _container3.Size = new Size(697, 531);
            _container3.SplitterDistance = 450;
            _container3.IsSplitterFixed = true;
            _container3.Panel2.Controls.Add(_container4);
            _container3.BackColor = SystemColors.Control;
            _container3.Panel1.BackColor = SystemColors.ControlDarkDark;

            #endregion

            #region container4

            _container4.Name = "container4";
            _container4.BorderStyle = BorderStyle.FixedSingle;
            _container4.Dock = DockStyle.Fill;
            _container4.Orientation = Orientation.Horizontal;
            _container4.FixedPanel = FixedPanel.Panel1;
            _container4.Size = new Size(243, 531);
            _container4.SplitterDistance = 180;
            _container4.IsSplitterFixed = true;
            _container4.BackColor = SystemColors.Control;
            _container4.Panel1.BackColor = SystemColors.ControlDarkDark;
            _container4.Panel2.BackColor = SystemColors.ControlDarkDark;

            #endregion

            #region ResumeLayout

            this.SetupComponents();
            this.componentPanel.Controls.Add(_container1);

            _container1.Panel1.ResumeLayout(false);
            _container1.Panel2.ResumeLayout(false);
            _container1.ResumeLayout(false);
            _container2.ResumeLayout(false);
            _container3.Panel2.ResumeLayout(false);
            _container3.ResumeLayout(false);
            _container4.ResumeLayout(false);
            this.componentPanel.ResumeLayout(false);
            this.componentPanel.PerformLayout();

            #endregion

            #region MenuItem Events

            EventHandler menuEventHandler = null;
            //文件
            menuEventHandler = delegate(object sender, EventArgs e) { CommandNew(); };
            menuItem_NewProject.Click += menuEventHandler;
            toolItem_NewProject.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandOpen(); };
            menuItem_OpenProject.Click += menuEventHandler;
            toolItem_OpenProject.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandSave(); };
            menuItem_SaveProject.Click += menuEventHandler;
            toolItem_SaveProject.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandSaveAs(); };
            menuItem_SaveAsProject.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandExit(); };
            menuItem_Exit.Click += menuEventHandler;

            //编辑
            menuEventHandler = delegate(object sender, EventArgs e) { CommandUndo(); };
            menuItem_Undo.Click += menuEventHandler;
            toolItem_Undo.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandRedo(); };
            menuItem_Redo.Click += menuEventHandler;
            toolItem_Redo.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandUndoAll(); };
            menuItem_UndoAll.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandRedoAll(); };
            menuItem_RedoAll.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandCut(); };
            menuItem_Cut.Click += menuEventHandler;
            toolItem_Cut.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandCopy(); };
            menuItem_Copy.Click += menuEventHandler;
            toolItem_Copy.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandPaster(); };
            menuItem_Paster.Click += menuEventHandler;
            toolItem_Paster.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandDel(); };
            menuItem_Del.Click += menuEventHandler;
            toolItem_Del.Click += menuEventHandler;

            //

            //视图
            menuEventHandler = delegate(object sender, EventArgs e) { CommandShowToolbar(); };
            menuItem_ShowToolbar.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandShowStatusbar(); };
            menuItem_ShowStatusbar.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandLeftCollapsed(); };
            menuItem_LeftCollapsed.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandRightCollapsed(); };
            menuItem_RightCollapsed.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandShowGrid(); };
            menuItem_ShowGrid.Click += menuEventHandler;
            toolItem_ShowGrid.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandToolkit(); };
            menuItem_Toolkit.Click += menuEventHandler;

            //图层
            menuEventHandler = delegate(object sender, EventArgs e) { CommandHighlight(); };
            menuItem_HighLight.Click += menuEventHandler;
            toolItem_HighLight.Click += menuEventHandler;

            //绘图
            menuEventHandler = delegate(object sender, EventArgs e) { CommandPointer(); };
            menuItem_Pointer.Click += menuEventHandler;
            toolItem_Pointer.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandSelectBox(); };
            menuItem_SelectBox.Click += menuEventHandler;
            toolItem_SelectBox.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandPen(); };
            menuItem_Pen.Click += menuEventHandler;
            toolItem_Pen.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandRectangle(); };
            menuItem_Rectangle.Click += menuEventHandler;
            toolItem_Rectangle.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandFill(); };
            menuItem_Fill.Click += menuEventHandler;
            toolItem_Fill.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandEraser(); };
            menuItem_Eraser.Click += menuEventHandler;
            toolItem_Eraser.Click += menuEventHandler;

            //比例
            menuEventHandler = delegate(object sender, EventArgs e) { CommandDefaultSize(); };
            menuItem_DefaultSize.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandZoomIn(); };
            menuItem_ZoomIn.Click += menuEventHandler;
            toolItem_ZoomIn.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandZoomOut(); };
            menuItem_ZoomOut.Click += menuEventHandler;
            toolItem_ZoomOut.Click += menuEventHandler;

            menuItem_ZoomH2.Click += delegate(object sender, EventArgs e) { CommandZoom(1, false); };
            menuItem_ZoomH4.Click += delegate(object sender, EventArgs e) { CommandZoom(2, false); };
            menuItem_ZoomH8.Click += delegate(object sender, EventArgs e) { CommandZoom(3, false); };
            menuItem_ZoomT2.Click += delegate(object sender, EventArgs e) { CommandZoom(1, true); };
            menuItem_ZoomT4.Click += delegate(object sender, EventArgs e) { CommandZoom(2, true); };
            menuItem_ZoomT8.Click += delegate(object sender, EventArgs e) { CommandZoom(3, true); };

            //数据
            menuEventHandler = delegate(object sender, EventArgs e) { CommandOutputMap(); };
            menuItem_OutputBitmapMap.Click += menuEventHandler;

            //工具
            menuEventHandler = delegate(object sender, EventArgs e) { CommandSetting(); };
            menuItem_Setting.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandAbout(); };
            menuItem_About.Click += menuEventHandler;
            toolItem_About.Click += menuEventHandler;

            //测试
            menuEventHandler = delegate(object sender, EventArgs e) { CommandTestSetting(); };
            menuItem_TestSetting.Click += menuEventHandler;

            menuEventHandler = delegate(object sender, EventArgs e) { CommandTest(); };
            menuItem_GameTest.Click += menuEventHandler;
            toolItem_Test.Click += menuEventHandler;

            #endregion

            #region Application Events

            FormClosing += new FormClosingEventHandler(frm_MainForm_FormClosing);
            Application.Idle
                += delegate(object sender, EventArgs e) { ApplicationIdleEHandler(); };

            #endregion
        }

        private void SetupComponents()
        {
            _component_projectPanel = new ProjectView();
            _component_tilesetPanel = new TilesetView();
            _component_mapPanel = new EditableMapView();
            _component_miniMapPanel = new MiniMapView();
            _component_mapSettingPanel = new MapSettingView();

            _component_projectPanel.DocManager = _docManager;
            _component_tilesetPanel.DocManager = _docManager;
            _component_mapPanel.DocManager = _docManager;
            _component_mapSettingPanel.DocManager = _docManager;

            _container2.Panel2.Controls.Add(_component_projectPanel);
            _container2.Panel1.Controls.Add(_component_tilesetPanel);
            _container3.Panel1.Controls.Add(_component_mapPanel);
            _container4.Panel1.Controls.Add(_component_miniMapPanel);
            _container4.Panel2.Controls.Add(_component_mapSettingPanel);

            _component_projectPanel.RegisterObserver(_component_tilesetPanel);
            _component_projectPanel.RegisterObserver(_component_mapPanel);
            _component_projectPanel.RegisterObserver(_component_miniMapPanel);
            _component_projectPanel.RegisterObserver(_component_mapSettingPanel);

            _component_mapPanel.EditorScrollChanged
                += delegate(object sender, ScrollCanvasEventArgs e)
                {
                    _component_miniMapPanel.ViewPort = e.ViewPort;
                    _component_miniMapPanel.ViewPortPosition = new Point(e.OffsetX, e.OffsetY);
                };
            _component_miniMapPanel.ViewPortDrag
                += delegate(object sender, MiniMapViewPortDragEventArgs e)
            {
                _component_mapPanel.SetScrollOffset(e.ViewPortPosition.X, e.ViewPortPosition.Y);
            };
            _component_mapSettingPanel.ShowGridEvent
                += delegate(object sender, EventArgs e) { CommandShowGrid(); };
        }

        private void ApplicationIdleEHandler()
        {
            if (!_docManager.HasDocument)
            {
                //文件
                menuItem_SaveProject.Enabled = false;
                menuItem_SaveAsProject.Enabled = false;
                menuItem_LeftCollapsed.Enabled = false;
                menuItem_RightCollapsed.Enabled = false;
                menuItem_ShowGrid.Enabled = false;
                menuItem_Toolkit.Enabled = false;

                //编辑
                menuItem_Undo.Enabled = false;
                menuItem_Redo.Enabled = false;
                menuItem_UndoAll.Enabled = false;
                menuItem_RedoAll.Enabled = false;
                menuItem_Cut.Enabled = false;
                menuItem_Copy.Enabled = false;
                menuItem_Paster.Enabled = false;
                menuItem_Del.Enabled = false;



                toolItem_Undo.Enabled = false;
                toolItem_Redo.Enabled = false;
                toolItem_Cut.Enabled = false;
                toolItem_Copy.Enabled = false;
                toolItem_Paster.Enabled = false;
                toolItem_Del.Enabled = false;

                //图层
                menuItem_HighLight.Enabled = false;
                toolItem_HighLight.Enabled = false;

                //绘图
                menuItem_Pointer.Enabled = false;
                menuItem_SelectBox.Enabled = false;
                menuItem_Pen.Enabled = false;
                menuItem_Rectangle.Enabled = false;
                menuItem_Fill.Enabled = false;
                menuItem_Eraser.Enabled = false;

                menuItem_DefaultSize.Enabled = false;
                menuItem_ZoomIn.Enabled = false;
                menuItem_ZoomOut.Enabled = false;
                menuItem_ZoomRate.Enabled = false;

                toolItem_ZoomIn.Enabled = false;
                toolItem_ZoomOut.Enabled = false;

                toolItem_SaveProject.Enabled = false;
                toolItem_ShowGrid.Enabled = false;

                toolItem_Pointer.Enabled = false;
                toolItem_SelectBox.Enabled = false;
                toolItem_Pen.Enabled = false;
                toolItem_Rectangle.Enabled = false;
                toolItem_Fill.Enabled = false;
                toolItem_Eraser.Enabled = false;

                //数据
                menuItem_OutputBitmapMap.Enabled = false;
                menuItem_Setting.Enabled = false;

                //测试
                menuItem_TestSetting.Enabled = false;
                menuItem_GameTest.Enabled = false;
                toolItem_Test.Enabled = false;
            }
            else
            {
                menuItem_SaveProject.Enabled = true;
                menuItem_SaveAsProject.Enabled = true;
                menuItem_LeftCollapsed.Enabled = true;
                menuItem_RightCollapsed.Enabled = true;
                menuItem_ShowGrid.Enabled = true;
                menuItem_Toolkit.Enabled = true;

                //编辑
                menuItem_Undo.Enabled = _component_mapPanel.CanUndo;
                menuItem_Redo.Enabled = _component_mapPanel.CanRedo;
                menuItem_UndoAll.Enabled = menuItem_Undo.Enabled;
                menuItem_RedoAll.Enabled = menuItem_Redo.Enabled;
                menuItem_Cut.Enabled = (_component_mapPanel.CanCut && _component_mapPanel.ToolStyle ==
                    ToolStyle.SelectBox);
                menuItem_Copy.Enabled = menuItem_Cut.Enabled;

                menuItem_Paster.Enabled = _component_mapPanel.CanPaster;
                menuItem_Del.Enabled = menuItem_Cut.Enabled;

                toolItem_Undo.Enabled = menuItem_Undo.Enabled;
                toolItem_Redo.Enabled = menuItem_Redo.Enabled;
                toolItem_Cut.Enabled = menuItem_Cut.Enabled;
                toolItem_Copy.Enabled = menuItem_Copy.Enabled;
                toolItem_Paster.Enabled = menuItem_Paster.Enabled;
                toolItem_Del.Enabled = menuItem_Cut.Enabled;

                menuItem_HighLight.Enabled = true;
                toolItem_HighLight.Enabled = true;

                menuItem_Pointer.Enabled = (_component_mapPanel.MapMode == EditableMapMode.DrawMode);
                menuItem_SelectBox.Enabled = menuItem_Pointer.Enabled;
                menuItem_Pen.Enabled = menuItem_Pointer.Enabled;
                menuItem_Rectangle.Enabled = menuItem_Pointer.Enabled;
                menuItem_Fill.Enabled = menuItem_Pointer.Enabled;
                menuItem_Eraser.Enabled = menuItem_Pointer.Enabled;

                //比例
                menuItem_DefaultSize.Enabled = true;
                menuItem_ZoomIn.Enabled = true;
                menuItem_ZoomOut.Enabled = true;
                menuItem_ZoomRate.Enabled = true;

                toolItem_ZoomIn.Enabled = true;
                toolItem_ZoomOut.Enabled = true;

                toolItem_SaveProject.Enabled = true;
                toolItem_ShowGrid.Enabled = true;

                toolItem_Pointer.Enabled = menuItem_Pointer.Enabled;
                toolItem_SelectBox.Enabled = menuItem_SelectBox.Enabled;
                toolItem_Pen.Enabled = menuItem_Pen.Enabled;
                toolItem_Rectangle.Enabled = menuItem_Rectangle.Enabled;
                toolItem_Fill.Enabled = menuItem_Fill.Enabled;
                toolItem_Eraser.Enabled = menuItem_Eraser.Enabled;

                menuItem_LeftCollapsed.Checked = !_container1.Panel1Collapsed;
                menuItem_RightCollapsed.Checked = !_container3.Panel2Collapsed;
                menuItem_ShowGrid.Checked = _component_mapPanel.ShowGrid;
                toolItem_ShowGrid.Checked = _component_mapPanel.ShowGrid;
                _component_mapSettingPanel.ShowGrid = _component_mapPanel.ShowGrid;

                menuItem_HighLight.Checked = _component_mapPanel.HighLightMapLayer;
                toolItem_HighLight.Checked = menuItem_HighLight.Checked;

                menuItem_Pointer.Checked = (_component_mapPanel.ToolStyle == ToolStyle.Pointer);
                toolItem_Pointer.Checked = menuItem_Pointer.Checked;

                menuItem_SelectBox.Checked = (_component_mapPanel.ToolStyle == ToolStyle.SelectBox);
                toolItem_SelectBox.Checked = menuItem_SelectBox.Checked;

                menuItem_Pen.Checked = (_component_mapPanel.ToolStyle == ToolStyle.Pen);
                toolItem_Pen.Checked = menuItem_Pen.Checked;

                menuItem_Rectangle.Checked = (_component_mapPanel.ToolStyle == ToolStyle.Rectangle);
                toolItem_Rectangle.Checked = menuItem_Rectangle.Checked;

                menuItem_Fill.Checked = (_component_mapPanel.ToolStyle == ToolStyle.Fill);
                toolItem_Fill.Checked = menuItem_Fill.Checked;

                menuItem_Eraser.Checked = (_component_mapPanel.ToolStyle == ToolStyle.Eraser);
                toolItem_Eraser.Checked = menuItem_Eraser.Checked;

                //
                if (_toolKit != null)
                {
                    _toolKit.Pointer = menuItem_Pointer.Checked;
                    _toolKit.SelectedBox = menuItem_SelectBox.Checked;
                    _toolKit.Pen = menuItem_Pen.Checked;
                    _toolKit.Rectangle = menuItem_Rectangle.Checked;
                    _toolKit.Fill = menuItem_Fill.Checked;
                    _toolKit.Eraser = menuItem_Eraser.Checked;

                    _toolKit.ZoomIn = menuItem_ZoomIn.Checked;
                    _toolKit.ZoomOut = menuItem_ZoomOut.Checked;

                    _toolKit.Grid = menuItem_ShowGrid.Checked;
                    _toolKit.Highlight = menuItem_HighLight.Checked;
                }

                //数据
                menuItem_OutputBitmapMap.Enabled = true;
                menuItem_Setting.Enabled = true;

                //测试
                menuItem_TestSetting.Enabled = true;
                menuItem_GameTest.Enabled = true;
                toolItem_Test.Enabled = true;
            }
            menuItem_ShowToolbar.Checked = tileMap_Tool.Visible;
            menuItem_ShowStatusbar.Checked = tileMap_Status.Visible;
        }

        #endregion

        #region 初始化工程文件管理对象

        private void InitializeDocManager()
        {
            _docManager = new DocManager(this);
            _docManager.FilePath = "";
            _docManager.FileDirectoryName = ApplicationConsts.TILEMAP_PROJECT_DIR;
            _docManager.NewDocName = ApplicationConsts.TILEMAP_PROJECT_NAME;
            _docManager.DocExtension = ApplicationConsts.TILEMAP_PROJECT_EXTENSION;
            _docManager.FileDlgFilter = ApplicationConsts.TILEMAP_PROJECT_FILE_FILTER;
            _docManager.FileDlgInitDirectory = "";

            FileTypeInfo fileType = new FileTypeInfo();
            fileType.Extension = ApplicationConsts.TILEMAP_FILETYPE_EXTENSION;
            fileType.ProgramId = ApplicationConsts.TILEMAP_FILETYPE_PROGRAMID;
            fileType.Version = ApplicationConsts.TILEMAP_VERSION;
            fileType.FileDisplayName = ApplicationConsts.TILEMAP_FILETYPE_DISPLAYNAME;

            _docManager.RegisterFileType(fileType);

            _docManager.IsDirty = false;

            _docManager.NewFile += NewDocumentHandler;
            _docManager.OpenFile += OpenDocumentHandler;
            _docManager.LoadFile += LoadDocumentHandler;
            _docManager.SaveFile += SaveDocumentHandler;
            _docManager.DocumentChanged
                += delegate(object sender, EventArgs e) { };

            //mru
            _mruManager = new MruManager();
            _mruManager.Initialize(this, menuItem_MruList, menu_File,
                ApplicationConsts.TILEMAP_SOFTWARE_REGISTRY_PATH);
            _mruManager.MruFileOpen += MruFileOpenHandler;

            //dragdrop
            _docDragDrop = new DocDragDropManager(this);
            _docDragDrop.FileDragDrop += FileDragDrop;
        }

        private void NewDocumentHandler(object sender, EventArgs e)
        {
            try
            {
                _component_projectPanel.Clear();
                _container1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenDocument(string fileName)
        {
            _docManager.OpenDocument(fileName);
        }

        private void OpenDocumentHandler(object sender, OpenFileEventArgs e)
        {
            if (e.IsSucceeded)
            {
                _container1.Visible = true;
                _mruManager.Add(e.FileName);
            }
            else
            {
                // _container1.Visible = false;
                _mruManager.Remove(e.FileName);
            }

            CommandStatusMessage();
        }

        private void LoadDocumentHandler(object sender, FileSerializationEventArgs e)
        {
            try
            {
                TreeViewData treeViewData = (TreeViewData)e.Formatter.Deserialize(e.FileSerializationStream);
                treeViewData.PopulateTree(_component_projectPanel.MapTreeData);

                string filePath = Path.GetDirectoryName(_docManager.FilePath) + "\\" +
                    ApplicationConsts.TILEMAP_DATA_MAP_DIR;
                if (Directory.Exists(filePath))
                {
                    _component_projectPanel.LoadMapList(filePath);
                }
                e.IsSucceeded = true;
            }
            catch (SerializationException ex)
            {
                e.IsSucceeded = false;
                MessageBox.Show(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                e.IsSucceeded = false;
                MessageBox.Show("Load Map Failed:" + ex.Message + " file not found!");
            }
        }

        private void SaveDocumentHandler(object sender, FileSerializationEventArgs e)
        {
            if (_component_projectPanel.MapTreeData == null) return;
            try
            {
                TreeViewData treeViewData = new TreeViewData(_component_projectPanel.MapTreeData);
                e.Formatter.Serialize(e.FileSerializationStream, treeViewData);

                string filePath = Path.GetDirectoryName(_docManager.FilePath) + "\\" +
                    ApplicationConsts.TILEMAP_DATA_MAP_DIR;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    Directory.Delete(filePath, true);
                    Directory.CreateDirectory(filePath);
                }
                _component_projectPanel.SaveMapList(filePath);

                filePath = Path.GetDirectoryName(_docManager.FilePath) + "\\" +
                    ApplicationConsts.TILEMAP_GRAPHICS_TILESET_DIR;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                else
                {
                    Directory.Delete(filePath, true);
                    Directory.CreateDirectory(filePath);
                }
                _component_projectPanel.OutputTilesetFile(filePath);
                e.IsSucceeded = true;
                return;
            }
            catch (SerializationException ex)
            {
                MessageBox.Show("Serialization failed:{0}", ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("LoadMap failed:{0}", ex.Message);
            }
            e.IsSucceeded = false;
        }

        private void MruFileOpenHandler(object sender, MruFileOpenEventArgs e)
        {
            OpenDocument(e.FileName);
        }

        private void FileDragDrop(object sender, FileDragDropEventArgs e)
        {
            if (e.FileName.Length > 0)
            {
                FileInfo file = new FileInfo(e.FileName);
                if (file.Extension == ApplicationConsts.TILEMAP_PROJECT_EXTENSION)
                {
                    OpenDocument(e.FileName);
                }
            }
        }

        #endregion

        #region 菜单命令

        private void CommandNew()
        {
            try
            {
                frm_NewProject frm_newProject = new frm_NewProject();
                frm_newProject.NewProject += delegate(NewProjectEventArgs e)
                {
                    _component_projectPanel.ProjectName = e.ProjectTitle;

                    _docManager.NewDocument();
                    _docManager.NewDocName = e.ProjectTitle;
                    _docManager.FileDirectoryName = e.ProjectSaveFolder;
                    _docManager.FilePath = string.Format("{0}\\{1}\\{2}{3}", e.ProjectSavePath,
                        _docManager.FileDirectoryName,
                        _docManager.NewDocName,
                        _docManager.DocExtension);

                    CommandStatusMessage();
                };
                frm_newProject.ShowDialog();
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("new failed:{0}", e.Message);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("new failed:{0}", e.Message);
            }
        }

        private void CommandOpen()
        {
            OpenDocument("");
        }

        private void CommandSave()
        {
            _docManager.SaveDocument(DocManager.SaveType.Save);
        }

        private void CommandSaveAs()
        {
            _docManager.SaveDocument(DocManager.SaveType.SaveAs);
        }

        private void CommandExit()
        {
            this.Close();
        }

        private void CommandUndo()
        {
            CommandManager commandManager = CommandManager.CreateInstance();
            commandManager.Undo();
            // _component_mapPanel.Undo();
        }

        private void CommandRedo()
        {
            CommandManager commandManager = CommandManager.CreateInstance();
            commandManager.Redo();
            //_component_mapPanel.Redo();
        }

        private void CommandUndoAll()
        {
            CommandManager commandManager = CommandManager.CreateInstance();
            commandManager.UndoAll();
            // _component_mapPanel.UndoAll();
        }

        private void CommandRedoAll()
        {
            CommandManager commandManager = CommandManager.CreateInstance();
            commandManager.RedoAll();
            // _component_mapPanel.RedoAll();
        }

        private void CommandCut()
        {
            _component_mapPanel.Cut();
        }

        private void CommandCopy()
        {
            _component_mapPanel.Copy();
        }

        private void CommandPaster()
        {
            _component_mapPanel.Paster();
        }

        private void CommandDel()
        {
            _component_mapPanel.Del();
        }

        private void CommandShowToolbar()
        {
            tileMap_Tool.Visible = !tileMap_Tool.Visible;
        }

        private void CommandShowStatusbar()
        {
            tileMap_Status.Visible = !tileMap_Status.Visible;
        }

        private void CommandLeftCollapsed()
        {
            _container1.Panel1Collapsed = !_container1.Panel1Collapsed;
        }

        private void CommandRightCollapsed()
        {
            _container3.Panel2Collapsed = !_container3.Panel2Collapsed;
        }

        private void CommandShowGrid()
        {
            _component_mapPanel.ShowGrid = !_component_mapPanel.ShowGrid;
        }

        private void CommandToolkit()
        {
            if (_toolKit == null || _toolKit.IsDisposed)
            {
                _toolKit = new frm_Toolkit();
                _toolKit.Toolkit_Pointer += delegate(object sender, EventArgs e) { CommandPointer(); };
                _toolKit.Toolkit_SelectedBox += delegate(object sender, EventArgs e) { CommandSelectBox(); };
                _toolKit.Toolkit_Pen += delegate(object sender, EventArgs e) { CommandPen(); };
                _toolKit.Toolkit_Rectangle += delegate(object sender, EventArgs e) { CommandRectangle(); };
                _toolKit.Toolkit_Fill += delegate(object sender, EventArgs e) { CommandFill(); };
                _toolKit.Toolkit_Eraser += delegate(object sender, EventArgs e) { CommandEraser(); };
                _toolKit.Toolkit_ZoomIn += delegate(object sender, EventArgs e) { CommandZoomIn(); };
                _toolKit.Toolkit_ZoomOut += delegate(object sender, EventArgs e) { CommandZoomOut(); };
                _toolKit.Toolkit_Grid += delegate(object sender, EventArgs e) { CommandShowGrid(); };
                _toolKit.Toolkit_Highlihgt += delegate(object sender, EventArgs e) { CommandHighlight(); };
                _toolKit.Toolkit_UnActivate += delegate(object sender, EventArgs e) { CommandActivate(); };

                if (WindowState == FormWindowState.Normal)
                    _toolKit.Location = new Point(this.Location.X - _toolKit.Width, this.Location.Y);
                else if (WindowState == FormWindowState.Maximized)
                    _toolKit.Location = new Point(this.Width - 355, 95);

                _toolKit.Show(this);
            }
            else
            {
                _toolKit.Visible = !_toolKit.Visible;
            }
        }

        private void CommandSetting()
        {
            frm_Setting setting = new frm_Setting();
            setting.UpdateSetting += delegate(object sender, EventArgs e) { LoadSetting(); };
            setting.ShowDialog();
            setting.Dispose();
        }

        private void CommandHighlight()
        {
            _component_mapPanel.HighLightMapLayer = !_component_mapPanel.HighLightMapLayer;
        }

        private void CommandZoomIn()
        {
            _component_mapPanel.SetZoomIn();
        }

        private void CommandZoomOut()
        {
            _component_mapPanel.SetZoomOut();
        }

        private void CommandZoom(int rate, bool zoomIn)
        {
            _component_mapPanel.SetZoom(rate, zoomIn);
        }

        private void CommandDefaultSize()
        {
            _component_mapPanel.SetDefaultSize();
        }

        private void CommandPointer()
        {
            _component_mapPanel.ToolStyle = ToolStyle.Pointer;
        }

        private void CommandSelectBox()
        {
            _component_mapPanel.ToolStyle = ToolStyle.SelectBox;
        }

        private void CommandPen()
        {
            _component_mapPanel.ToolStyle = ToolStyle.Pen;
        }

        private void CommandRectangle()
        {
            _component_mapPanel.ToolStyle = ToolStyle.Rectangle;
        }

        private void CommandFill()
        {
            _component_mapPanel.ToolStyle = ToolStyle.Fill;
        }

        private void CommandEraser()
        {
            _component_mapPanel.ToolStyle = ToolStyle.Eraser;
        }

        private void CommandAbout()
        {
            frm_About about = new frm_About();
            about.ShowDialog();
            about.Dispose();
        }

        private void CommandActivate()
        {
            this.Activate();
        }

        private void CommandStatusMessage()
        {
            if (_docManager.HasDocument)
                toolStatus_msg.Text = _docManager.FilePath;
            else
                toolStatus_msg.Text = "就绪";
        }

        private void CommandOutputMap()
        {
            _component_mapPanel.OutputMap();
        }

        private void CommandTest()
        {
            //_component_mapPanel.TestMap();

            var map = _component_projectPanel.CurrentMap;
            if (map == null) return;

            if (toolItem_Test.Enabled)
            {
                toolItem_Test.Enabled = false;
                menuItem_TestSetting.Enabled = false;
                menuItem_GameTest.Enabled = false;

                var game = new Test.MyGame(this, map);
                game.GameWindowShutDown += (sender, e) =>
                {
                    toolItem_Test.Enabled = true;
                    menuItem_TestSetting.Enabled = true;
                    menuItem_GameTest.Enabled = true;
                };
                game.Run();
            }
        }

        private void CommandTestSetting()
        {
            var setting = new Test.Setting.frm_GameSetting();
            setting.ShowDialog();
            setting.Dispose();
        }

        #endregion
    }
}
