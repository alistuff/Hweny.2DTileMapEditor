using System;
using System.Collections.Generic;
using System.Text;
using TileMapEditor.Controls;
using System.Windows.Forms;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 选择框工具
    /// </summary>
    class ToolSelectBox : Tool
    {
        public ToolSelectBox()
        {
            Cursor = new Cursor(GetType(), "SelectBox.cur");
        }

        public override void OnMouseDown(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseDown(editor, e);

            if (e.Button == MouseButtons.Left)
            {
                editor.SetRendererMouseStartPoint(e.X, e.Y);
                editor.SetRendererMouseEndPoint(e.X,e.Y);
            }
        }

        public override void OnMouseMove(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseMove(editor, e);
            editor.Cursor = Cursor;
            editor.ActiveClipboard();
            if (e.Button == MouseButtons.Left)
            {
                editor.SetRendererMouseEndPoint(e.X, e.Y);
            }
        }

        public override void OnMouseUp(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseUp(editor, e);

            if (e.Button == MouseButtons.Left)
            {
                editor.ActiveClipboard();
            }
        }
    }
}
