using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TileMapDoc
{
    /// <summary>
    /// 实现拖放工程文件打开工程
    /// </summary>
    public class DocDragDropManager
    {
        private Form _owner;
        public event FileDragDropEventHandler FileDragDrop;

        public DocDragDropManager(Form owner)
        {
            _owner = owner;

            _owner.AllowDrop = true;

            _owner.DragEnter += _owner_DragEnter;
            _owner.DragDrop += _owner_DragDrop;
        }

        private void _owner_DragDrop(object sender, DragEventArgs e)
        {
            Array data = (Array)e.Data.GetData(DataFormats.FileDrop);
            if (data != null)
            {
                if (FileDragDrop != null)
                {
                    FileDragDrop.Invoke(this,
                        new FileDragDropEventArgs(data.GetValue(0).ToString()));
                    _owner.Activate();
                }
            }
        }

        private void _owner_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
    }
}
