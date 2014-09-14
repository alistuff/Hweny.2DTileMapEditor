using System;
using System.Collections.Generic;
using System.Text;
using TileMapEditor.Controls;
using System.Windows.Forms;
using System.Drawing;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 橡皮擦工具
    /// </summary>
    class ToolEraser : Tool
    {
        public ToolEraser()
        {
            Cursor = new System.Windows.Forms.Cursor(GetType(), "Eraser.cur");
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
                editor.SetEmpty(editor.RealSelectedArea);
                editor.RealSelectedArea = Rectangle.Empty;
                editor.SetDirty();

                editor.AddCommandToHistory("橡皮擦");
            }
        }
    }
}
