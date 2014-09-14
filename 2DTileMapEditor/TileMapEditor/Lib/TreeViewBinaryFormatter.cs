using System;
using System.Windows.Forms;

namespace TileMapEditor
{
    /// <summary>
    /// 封装TreeView，用于对TreeView序列化
    /// </summary>
    [Serializable]
    public class TreeViewData
    {
        public TreeNodeData[] Nodes;

        public TreeViewData(TreeView treeView)
        {
            this.Nodes = new TreeNodeData[treeView.Nodes.Count];
            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                this.Nodes[i] = new TreeNodeData(treeView.Nodes[i]);
            }
        }

        public void PopulateTree(TreeView treeView)
        {
            if (this.Nodes == null || this.Nodes.Length == 0)
            {
                return;
            }
            TreeNode newNode = null;
            treeView.Nodes.Clear();
            treeView.BeginUpdate();
            foreach (TreeNodeData node in this.Nodes)
            {
                newNode = node.ToTreeNode();
                treeView.Nodes.Add(newNode);
                BindSelectedNode(treeView, newNode);
            }
            treeView.EndUpdate();
        }

        private void BindSelectedNode(TreeView treeView, TreeNode node)
        {
            foreach (TreeNode tn in node.Nodes)
            {
                if (tn.Checked)
                {
                    treeView.SelectedNode = tn;
                    break;
                }
                else
                {
                    BindSelectedNode(treeView, tn);
                }
            }
        }
    }

    [Serializable]
    public class TreeNodeData
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public int ImageIndex { get; set; }
        public int SelectedImageIndex { get; set; }
        public bool IsSelected { get; set; }
        public bool Expanded { get; set; }
        public object Tag { get; set; }
        public TreeNodeData[] Nodes;

        public TreeNodeData(TreeNode node)
        {
            this.Text = node.Text;
            this.Name = node.Name;
            this.ImageIndex = node.ImageIndex;
            this.SelectedImageIndex = node.SelectedImageIndex;
            this.Expanded = node.IsExpanded;
            this.IsSelected = node.IsSelected;

            if (node.Tag != null && node.Tag.GetType().IsSerializable)
                this.Tag = node.Tag;
            else
                this.Tag = null;

            this.Nodes = new TreeNodeData[node.Nodes.Count];
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                this.Nodes[i] = new TreeNodeData(node.Nodes[i]);
            }
        }

        public TreeNode ToTreeNode()
        {
            TreeNode node = new TreeNode(this.Text, this.ImageIndex, this.SelectedImageIndex);
            node.Name = this.Name;
            node.Tag = this.Tag;
            if (this.Expanded)
            {
                node.Expand();
            }
            if (this.IsSelected)
            {
                node.Checked = true;
            }
            if (this.Nodes == null && this.Nodes.Length == 0)
            {
                return null;
            }
            if (node != null && this.Nodes.Length == 0)
            {
                return node;
            }
            foreach (TreeNodeData tn in this.Nodes)
            {
                node.Nodes.Add(tn.ToTreeNode());
            }
            return node;
        }
    }
}
