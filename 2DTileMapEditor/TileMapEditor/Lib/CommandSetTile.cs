using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 设置图素命令
    /// </summary>
    class CommandSetTile : Command
    {
        private UndoGroup _undoGroup;
        private EditableMap _map;

        public CommandSetTile(string name, EditableMap map)
            : base(name)
        {
            _undoGroup = new UndoGroup();
            _map = map;
        }

        public void SaveChange(UndoGroup item)
        {
            _undoGroup = item;
        }

        public override bool Undo()
        {
            _undoGroup = _undoGroup.Populate(_map);
            return _undoGroup.Count == 0 ? false : true;

        }

        public override bool Redo()
        {
            _undoGroup = _undoGroup.Populate(_map);
            return _undoGroup.Count == 0 ? false : true;
        }
    }
}
