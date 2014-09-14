using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TileMapEditor.Controls.ControlsEx
{
    public class DraggableTree : TreeView
    {
        public event EventHandler DragDropEvent;
            
        public DraggableTree()
        {
            AllowDrop = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            if (e.Button == MouseButtons.Right)
            {
                SelectedNode = e.Node;
            }
        }

        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (e.Button == MouseButtons.Left)
                DoDragDrop(e.Item, DragDropEffects.Move);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            drgevent.Effect = drgevent.AllowedEffect;
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            Point targetPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
            SelectedNode = GetNodeAt(targetPoint);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            Point tartgetPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
            TreeNode targetNode = GetNodeAt(tartgetPoint);

            TreeNode draggedNode = (TreeNode)drgevent.Data.GetData(typeof(TreeNode));
            if (!draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode))
            {
                if (drgevent.Effect == DragDropEffects.Move)
                {
                    draggedNode.Remove();
                    targetNode.Nodes.Add(draggedNode);

                    if (DragDropEvent != null)
                        DragDropEvent(this,new EventArgs());
                }
                targetNode.Expand();
            }
        }

        private bool ContainsNode(TreeNode draggedNode, TreeNode targetNode)
        {
            if (targetNode.Parent == null) return false;
            if (targetNode.Parent.Equals(draggedNode)) return true;

            return ContainsNode(draggedNode, targetNode.Parent);
        }
    }
}
