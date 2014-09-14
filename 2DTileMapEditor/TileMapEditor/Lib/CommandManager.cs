using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 命令历史记录
    /// </summary>
    public class CommandManager
    {
        public event EventHandler HistoryUpdate;
        public event UndoEventHandler UndoEvent;

        private static CommandManager _instance;

        private List<Command> _historyList;
        private int _next;
        private int _capacity;

        private CommandManager()
        {
            SetCapacity(0);
            ClearHistory();
        }

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static CommandManager CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new CommandManager();
            }
            return _instance;
        }

        /// <summary>
        /// 设置历史记录列表容量，并调整历史记录列表
        /// </summary>
        /// <param name="capacity"></param>
        public void SetCapacity(int capacity)
        {
            if (capacity <= 0 || capacity == _capacity) return;
            if (capacity > _capacity)
            {
                _capacity = capacity;
                return;
            }
            int count = _capacity - capacity;
            if (_next <= count)
                _next = 0;
            else
                _next -= count;

            if (count <= _historyList.Count)
                _historyList.RemoveRange(0, count);
            _capacity = capacity;

            OnHistoryUpdate(new EventArgs());
        }

        /// <summary>
        /// 返回命令历史记录
        /// </summary>
        public List<Command> HistoryList
        {
            get { return _historyList; }
        }

        /// <summary>
        /// 是否能撤消
        /// </summary>
        public bool CanUndo
        {
            get
            {
                if (_next < 0 ||
                    _next > _historyList.Count - 1)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// 是否能重做
        /// </summary>
        public bool CanRedo
        {
            get
            {
                if (_next == _historyList.Count - 1)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// 清空命令历史记录
        /// </summary>
        public void ClearHistory()
        {
            _historyList = new List<Command>();
            _next = -1;

            OnHistoryUpdate(new EventArgs());
        }

        /// <summary>
        /// 添加命令到到命令历史记录列表
        /// </summary>
        /// <param name="cmd"></param>
        public void AddCommand(Command cmd)
        {
            AdjustHistoryList();

            _historyList.Add(cmd);
            _next++;
            if (_next == _capacity)
            {
                _historyList.RemoveAt(0);
                _next--;
            }

            OnHistoryUpdate(new EventArgs());
        }

        /// <summary>
        /// 撤消
        /// </summary>
        public void Undo()
        {
            if (!CanUndo)
                return;
            Command command = _historyList[_next];
            if (!command.Undo())
                _historyList.RemoveAt(_next);

            OnUndoEvent(new UndoEvents(_next));
            _next--;
        }

        /// <summary>
        /// 全部撤消
        /// </summary>
        public void UndoAll()
        {
            while (CanUndo)
            {
                Undo();
            }
        }

        /// <summary>
        /// 重做
        /// </summary>
        public void Redo()
        {
            if (!CanRedo)
                return;
            int index = _next + 1;
            Command command = _historyList[index];
            if (!command.Redo())
            {
                _historyList.RemoveAt(index);
            }
            else
            {
                OnUndoEvent(new UndoEvents(index));
                _next++;
            }
        }

        /// <summary>
        /// 全部重做
        /// </summary>
        public void RedoAll()
        {
            while (CanRedo)
            {
                Redo();
            }
        }

        /// <summary>
        /// 调整历史记录
        /// </summary>
        private void AdjustHistoryList()
        {
            if (_historyList.Count == 0) return;
            if (_historyList.Count - 1 == _next) return;

            for (int i = _historyList.Count - 1; i > _next; i--)
            {
                _historyList.RemoveAt(i);
            }
        }

        private void OnHistoryUpdate(EventArgs e)
        {
            if (HistoryUpdate != null)
                HistoryUpdate(this, e);
        }

        private void OnUndoEvent(UndoEvents e)
        {
            if (UndoEvent != null)
                UndoEvent(this,e);
        }
    }
}
