using System;
using TileMapEditor.Controls;
using System.Windows.Forms;
using System.Drawing;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 指针工具
    /// </summary>
    class ToolPointer : Tool
    {
        public ToolPointer()
        {

        }

        public override void OnMouseDown(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseDown(editor, e);
        }

        public override void OnMouseMove(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseMove(editor, e);
            editor.Cursor = Cursors.Default;
            editor.RealSelectedArea = Rectangle.Empty;
        }
    }
}
