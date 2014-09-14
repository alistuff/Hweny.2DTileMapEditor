using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapLib;
using TileMapEditor.Lib;

namespace TileMapEditor.Controls
{
    public partial class ctl_EditMapLayer : UserControl,IComponentObserver
    {
        public event EventHandler Dirty;

        private EditableMap _map;

        public ctl_EditMapLayer()
        {
            InitializeComponent();

            SetupButton();
            SetupDataGridView();
        }

        public void Update(EditableMap mapData)
        {
            if (mapData == null)
            {
                return;
            }
            {
                _map = (EditableMap)mapData;
                PopulateDataGridView();
            }
        }

        private void SetupButton()
        {
            btnSelectAll.Click += new EventHandler(btnSelectAll_Click);
            btnUnSelect.Click += new EventHandler(btnUnSelect_Click);
            btnAdd.Click += new EventHandler(btnAdd_Click);
            btnUp.Click += new EventHandler(btnUp_Click);
            btnDown.Click += new EventHandler(btnDown_Click);
            btnDelete.Click += new EventHandler(btnDelete_Click);

            btnSelectAll.ToolTipText = "全选";
            btnUnSelect.ToolTipText = "反选";
            btnAdd.ToolTipText = "添加图层";
            btnUp.ToolTipText = "上移图层";
            btnDown.ToolTipText = "下移图层";
            btnDelete.ToolTipText = "删除图层";
        }

        private void SetupDataGridView()
        {
            this.SuspendLayout();

            DataGridViewColumn[] columns = new DataGridViewColumn[5];
            columns[0] = new DataGridViewCheckBoxColumn(false)
            {
                Name = "选择",
                Resizable = DataGridViewTriState.False,
                Width = 34
            };

            columns[1] = new DataGridViewTextBoxColumn()
            {
                Name = "层次",
                Resizable = DataGridViewTriState.False,
                ReadOnly = true,
                Width = 35,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            columns[2] = new DataGridViewCheckBoxColumn(false)
            {
                Name = "显示",
                Resizable = DataGridViewTriState.False,
                Width = 35
            };

            columns[3] = new DataGridViewCheckBoxColumn(false)
            {
                Name = "背景",
                Resizable = DataGridViewTriState.False,
                Width = 35
            };

            columns[4] = new DataGridViewTextBoxColumn()
            {
                Name = "图层名称",
                Resizable = DataGridViewTriState.False,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Width=85
            };

            layerGridView.Columns.AddRange(columns);

            layerGridView.ColumnHeadersDefaultCellStyle = getColumnDefaultStyle();
            layerGridView.RowsDefaultCellStyle = getRowDefaultStyle();

            layerGridView.AllowUserToAddRows = false;
            layerGridView.AllowUserToDeleteRows = false;
            layerGridView.RowHeadersVisible = false;
            layerGridView.GridColor = Color.Black;
            layerGridView.BackgroundColor = SystemColors.Control;
            layerGridView.Dock = DockStyle.Fill;
            layerGridView.ScrollBars = ScrollBars.Vertical;
            layerGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            layerGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            layerGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            layerGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            layerGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            layerGridView.EnableHeadersVisualStyles = false;
            layerGridView.AllowUserToResizeRows = false;
            layerGridView.MultiSelect = false;
            layerGridView.CurrentCellDirtyStateChanged += new EventHandler(layerGridView_CurrentCellDirtyStateChanged);
            layerGridView.CellValueChanged+=new DataGridViewCellEventHandler(layerGridView_CellValueChanged);
            layerGridView.CellFormatting += new DataGridViewCellFormattingEventHandler(layerGridView_CellFormatting);
            layerGridView.SelectionChanged += new EventHandler(layerGridView_SelectionChanged);

            this.ResumeLayout();
        }

        void layerGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (layerGridView.Rows.Count == 0||layerGridView.SelectedRows.Count==0) return;
            _map.SetCurrentMapLayer(layerGridView.Rows.Count - 1 - layerGridView.SelectedRows[0].Index);
        }

        private DataGridViewCellStyle getColumnDefaultStyle()
        {
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Black;
            columnHeaderStyle.ForeColor = Color.Snow;
            columnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return columnHeaderStyle;
        }

        private DataGridViewCellStyle getRowDefaultStyle()
        {
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.SelectionBackColor = Color.YellowGreen;
            rowStyle.SelectionForeColor = Color.Black;
            rowStyle.BackColor = SystemColors.Control;
            rowStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            return rowStyle;
        }

        private DataGridViewCellStyle getRowSelectedStyle()
        {
            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle();
            rowStyle.BackColor = Color.DarkGray;
            return rowStyle;
        }

        private void setSelectedRowStyle(int index, bool selected)
        {
            if (selected)
                layerGridView.Rows[index].DefaultCellStyle = getRowSelectedStyle();
            else
                layerGridView.Rows[index].DefaultCellStyle = getRowDefaultStyle();
        }

        private void PopulateDataGridView()
        {
            layerGridView.Rows.Clear();
            if (_map != null)
            {
                for (int i = 0; i < _map.MapLayers.Count; i++)
                {
                    object[] values = new object[]
                    {
                        false,
                        "层"+(i+1),
                        _map.MapLayers[i].Visible,
                        _map.MapLayers[i].BackgroundLayer,
                        _map.MapLayers[i].Name
                    };
                    layerGridView.Rows.Insert(0, values);
                }
            }
        }

        #region Events

        void layerGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                switch (e.ColumnIndex)
                {
                    case 0:/*选择*/
                        setSelectedRowStyle(e.RowIndex, (bool)e.Value);
                        break;
                    case 1:
                        break;
                    case 2:/*显示*/
                        break;
                    case 3:/*背景*/
                        break;
                    case 4:/*图层名称*/
                        if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString().Trim()))
                            e.Value = "[未命名图块]";
                        else
                            e.Value = e.Value.ToString().Trim();
                        break;
                    default:
                        break;
                }
            }
        }

        void layerGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (layerGridView.IsCurrentCellDirty)
            {
                layerGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        void layerGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==2)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)layerGridView.
                    Rows[e.RowIndex].Cells[e.ColumnIndex];
                _map.SetLayerVisible(layerGridView.Rows.Count-1-e.RowIndex,(bool)checkCell.Value);
                layerGridView.Invalidate();

                OnDirty(new EventArgs());
            }
            else if (e.ColumnIndex == 3)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)layerGridView.
                    Rows[e.RowIndex].Cells[e.ColumnIndex];
                _map.SetLayerBackground(layerGridView.Rows.Count - 1 - e.RowIndex, (bool)checkCell.Value);
                layerGridView.Invalidate();

                OnDirty(new EventArgs());
            }
            else if (e.ColumnIndex == 4)
            {
                DataGridViewTextBoxCell txtCell = (DataGridViewTextBoxCell)layerGridView.
                    Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (txtCell.Value != null)
                {
                    _map.SetLayerName(layerGridView.Rows.Count - 1 - e.RowIndex, txtCell.Value.ToString());
                    layerGridView.Invalidate();

                    OnDirty(new EventArgs());
                }
            }
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            delLayer();
            adjustLayerNumber();
        }

        void btnDown_Click(object sender, EventArgs e)
        {
            layerDown();         
        }

        void btnUp_Click(object sender, EventArgs e)
        {
            layerUp();
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            addLayer();
        }

        void btnUnSelect_Click(object sender, EventArgs e)
        {
            unSelet();
        }

        void btnSelectAll_Click(object sender, EventArgs e)
        {
            selectAll();
        }

        /// <summary>
        /// 全选
        /// </summary>
        void selectAll()
        {
            if (layerGridView.Rows.Count == 0) return;
            layerGridView.SelectedRows[0].Cells[1].Selected = true;
            for (int i = 0; i < layerGridView.Rows.Count; i++)
            {
                layerGridView.Rows[i].Cells[0].Value = true;
            }
        }

        /// <summary>
        /// 反选
        /// </summary>
        void unSelet()
        {
            if (layerGridView.Rows.Count == 0) return;
            layerGridView.SelectedRows[0].Cells[1].Selected = true;
            for (int i = 0; i < layerGridView.Rows.Count; i++)
            {
                layerGridView.Rows[i].Cells[0].Value = !Convert.ToBoolean(layerGridView.Rows[i].Cells[0].Value);
            }
        }

        /// <summary>
        /// 添加图层
        /// </summary>
        void addLayer()
        {
            if (_map != null)
            {
                string newLayerName = "[新建图层]";
                int zIndex = layerGridView.Rows.Count;

                _map.AddMapLayer(newLayerName, zIndex, true);

                layerGridView.Rows.Insert(0, new object[]
                        { 
                            false,
                            "层" + (zIndex+1),
                            true, 
                            true,
                            newLayerName
                        });
                layerGridView.Rows[0].Selected = true;
                OnDirty(new EventArgs());
            }
        }

        /// <summary>
        /// 上移图层
        /// </summary>
        void layerUp()
        {
            if (layerGridView.Rows.Count == 0) return;
            layerGridView.SelectedRows[0].Cells[1].Selected = true;
            DataGridViewRow selectedRow = layerGridView.SelectedRows[0];
            int index = selectedRow.Index;
            if (index == 0) return;
            object[] values = new object[layerGridView.Columns.Count];
            for (int i = 0; i < values.Length; i++)
                values[i] = selectedRow.Cells[i].Value;
            layerGridView.Rows.Remove(selectedRow);
            layerGridView.Rows.Insert(index - 1, values);
            adjustLayerNumber();
            layerGridView.Rows[index - 1].Selected = true;

            _map.SwapMapLayer(layerGridView.Rows.Count - 1 - index, layerGridView.Rows.Count - index);

            OnDirty(new EventArgs());
        }

        /// <summary>
        /// 下移图层
        /// </summary>
        void layerDown()
        {
            if (layerGridView.Rows.Count == 0) return;
            layerGridView.SelectedRows[0].Cells[1].Selected = true;
            DataGridViewRow selectedRow = layerGridView.SelectedRows[0];
            int index = selectedRow.Index;
            if (index == layerGridView.Rows.Count - 1) return;
            object[] values = new object[layerGridView.Columns.Count];
            for (int i = 0; i < values.Length; i++)
                values[i] = selectedRow.Cells[i].Value;
            layerGridView.Rows.Remove(selectedRow);
            layerGridView.Rows.Insert(index + 1, values);
            adjustLayerNumber();
            layerGridView.Rows[index + 1].Selected = true;

            _map.SwapMapLayer(layerGridView.Rows.Count - 1 - index, layerGridView.Rows.Count - index - 2);

            OnDirty(new EventArgs());
        }

        /// <summary>
        /// 删除图层
        /// </summary>
        void delLayer()
        {
            if (layerGridView.Rows.Count == 0 || _map == null) return;
            layerGridView.SelectedRows[0].Cells[1].Selected = true;
            DialogResult result = MessageBox.Show("确定删除图层吗?", "提示", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                int index = 0;
                if (isSelectedLayer())
                {
                    for (int i = layerGridView.Rows.Count - 1; i >= 0; i -= 1)
                    {
                        bool flag = Convert.ToBoolean(layerGridView.Rows[i].Cells[0].Value);
                        if (flag)
                        {
                            index = layerGridView.Rows[i].Index;
                            layerGridView.Rows.RemoveAt(index);
                            _map.DeleteMapLayer(_map.MapLayers.Count - index - 1);
                        }
                    }
                }
                else
                {
                    index = layerGridView.SelectedRows[0].Index;
                    layerGridView.Rows.RemoveAt(index);
                    _map.DeleteMapLayer(_map.MapLayers.Count - index - 1);
                }

                OnDirty(new EventArgs());
            }
        }

        /// <summary>
        /// 调整层次数字
        /// </summary>
        void adjustLayerNumber()
        {
            for (int i = 0; i < layerGridView.Rows.Count; i++)
                layerGridView.Rows[i].Cells[1].Value = "层" + (layerGridView.Rows.Count - i);
        }

        /// <summary>
        /// 是否勾选图层
        /// </summary>
        /// <returns></returns>
        bool isSelectedLayer()
        {
            bool result = false;
            for (int i = 0; i < layerGridView.Rows.Count; i++)
            {
                bool flag = Convert.ToBoolean(layerGridView.Rows[i].Cells[0].Value);
                if (flag)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        void OnDirty(EventArgs e)
        {
            EventHandler temp = Dirty;
            if (temp != null)
                temp(this,e);
        }

        #endregion
    }
}
