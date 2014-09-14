using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TileMapEditor.Lib;
using System.Collections;

namespace TileMapEditor.Controls
{
    public partial class ctl_EditHistory : UserControl
    {
        private CommandManager _commandManager;
        public ctl_EditHistory()
        {
            InitializeComponent();

            _commandManager = CommandManager.CreateInstance();
            _commandManager.HistoryUpdate += UpdateHistory;
            _commandManager.UndoEvent += UpdateHistoryIndex;
            btnDelete.Click += new EventHandler(btnDelete_Click);
        }

        private void UpdateHistoryIndex(object sender, UndoEvents e)
        {
            if (lsbHistory.Items.Count == 0) return;

            lsbHistory.SelectedIndex = e.UndoIndex;
        }

        private void UpdateHistory(object sender, EventArgs e)
        {
            List<Command> historyList = _commandManager.HistoryList;

            lsbHistory.Items.Clear();
            int i = 0;
            foreach (Command cmd in historyList)
            {
                i++;
                lsbHistory.Items.Add(string.Format("OP{0}: {1}", i.ToString("000"), cmd.Name));
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsbHistory.Items.Count == 0) return;
            if (MessageBox.Show("确认删除历史记录吗?", "提示", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==DialogResult.Yes)
            {
                _commandManager.ClearHistory();
            }
        }
    }
}
