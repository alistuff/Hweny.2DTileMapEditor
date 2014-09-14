using System;
using TileMapEditor.Controls;
using System.Windows.Forms;
using System.Drawing;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 矩形绘制工具
    /// </summary>
    class ToolRectangle : Tool
    {
        public ToolRectangle()
        {
            Cursor = new System.Windows.Forms.Cursor(GetType(), "Rectangle.cur");
        }

        public override void OnMouseDown(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseDown(editor, e);
            if (e.Button == MouseButtons.Left)
            {
                editor.SetRendererMouseStartPoint(e.X, e.Y);
            }
        }

        public override void OnMouseMove(EditableMapView editor, MouseEventArgs e)
        {
            base.OnMouseMove(editor, e);
            editor.Cursor = Cursor;
            if (e.Button == MouseButtons.Left)
            {
                editor.SetRendererMouseEndPoint(e.X, e.Y);
            }
        }

        public override void OnMouseUp(EditableMapView editor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editor.SetRendererMouseEndPoint(e.X, e.Y);
                editor.SetTiles(editor.RealSelectedArea);
                editor.RealSelectedArea = Rectangle.Empty;
                editor.SetDirty();

                editor.AddCommandToHistory("矩形");
            }
        }
    }
}
