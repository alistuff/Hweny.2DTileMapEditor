using System;
using System.Windows.Forms;
using TileMapEditor.Controls;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 工具类
    /// </summary>
    internal abstract class Tool
    {
        private Cursor _cursor;
        protected Cursor Cursor
        {
            get { return _cursor; }
            set { _cursor = value; }
        }

        protected Tool()
        {
            _cursor = Cursors.Default;
        }

        public virtual void OnMouseDown(EditableMapView editor, MouseEventArgs e)
        {
            //
        }

        public virtual void OnMouseMove(EditableMapView editor, MouseEventArgs e)
        {
            //
        }

        public virtual void OnMouseUp(EditableMapView editor, MouseEventArgs e)
        {
            //
        }
    }
}
