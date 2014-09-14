using System;
using System.Windows.Forms;
using TileMapEditor.Controls;
using TileMapRenderer;
using System.Drawing;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 铅笔工具
    /// </summary>
    class ToolPen : Tool
    {
        public ToolPen()
        {
            Cursor = new Cursor(GetType(), "Pen.cur");
        }

        public override void OnMouseDown(EditableMapView editor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editor.SetTiles(e.X, e.Y);
                editor.SetDirty();
            }
            else if (e.Button == MouseButtons.Right)
            {
                editor.SetTileEmpty(e.X, e.Y);
                editor.SetDirty();
            }
        }

        public override void OnMouseMove(EditableMapView editor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                editor.SetTiles(e.X, e.Y);
                editor.SetDirty();
            }
            else if (e.Button == MouseButtons.Right)
            {
                editor.SetTileEmpty(e.X, e.Y);
                editor.SetDirty();
            }

            editor.Cursor = Cursor;
            editor.SetRendererMouseStartPoint(e.X, e.Y);
            editor.SetRendererMouseEndPoint(e.X + editor.GetSelectedTilesSize().Width,
                e.Y + editor.GetSelectedTilesSize().Height);
        }

        public override void OnMouseUp(EditableMapView editor, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                editor.AddCommandToHistory("铅笔");
            }
        }
    }
}
