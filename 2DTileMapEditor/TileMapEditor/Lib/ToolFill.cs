using System;
using TileMapEditor.Controls;
using System.Windows.Forms;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 填充工具
    /// </summary>
    class ToolFill:Tool
    {
        public ToolFill()
        {
            Cursor = new System.Windows.Forms.Cursor(GetType(), "Fill.cur");
        }

        public override void OnMouseDown(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseDown(editor, e);

            if (e.Button == MouseButtons.Left)
            {
                editor.FillTiles(e.X, e.Y);
                editor.SetDirty();
            }
        }

        public override void OnMouseMove(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseMove(editor, e);

            if (e.Button == MouseButtons.Left)
            {
                editor.FillTiles(e.X, e.Y);
                editor.SetDirty();
            }

            editor.Cursor = Cursor;
            editor.SetRendererMouseStartPoint(e.X,e.Y);
            editor.SetRendererMouseEndPoint(e.X,e.Y);
        }

        public override void OnMouseUp(EditableMapView editor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editor.AddCommandToHistory("填充");
            }
        }
    }
}
