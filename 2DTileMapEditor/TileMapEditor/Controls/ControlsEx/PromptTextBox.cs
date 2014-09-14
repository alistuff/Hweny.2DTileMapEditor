using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace TileMapEditor.Controls.ControlsEx
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TextBox))]
    public class PromptTextBox : TextBox
    {
        private bool _dirty;
        private string _promptText;

        public PromptTextBox()
        {
            _dirty = false;
            _promptText = "";
            ForeColor = Color.Gray;
            Enter += new EventHandler(MyTextBox_Enter);
            Leave += new EventHandler(MyTextBox_Leave);
            TextChanged += new EventHandler(PromptTextBox_TextChanged);
        }

        void PromptTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Text.Trim() == "" || Text.Trim() == _promptText)
            {
                _dirty = false;
                ForeColor = Color.Gray;
            }
            else
            {
                ForeColor = Color.Black;
                _dirty = true;
            }
        }

        void MyTextBox_Leave(object sender, EventArgs e)
        {
            if (Text.Trim() == "" || Text.Trim() == _promptText)
            {
                Text = _promptText;
                _dirty = false;
                ForeColor = Color.Gray;
            }
            else
            {
                ForeColor = Color.Black;
                _dirty = true;
            }
        }

        void MyTextBox_Enter(object sender, EventArgs e)
        {
            if (Text == _promptText)
            {
                Text = "";
                ForeColor = Color.Black;
            }
        }

        [Browsable(false)]
        public bool Dirty
        {
            get { return _dirty; }
        }

        [Category("HWL")]
        [Description("默认提示文本信息")]
        [Browsable(true)]
        public string PromptText
        {
            get { return _promptText; }
            set { _promptText = value; Text = value; }
        }
    }
}
