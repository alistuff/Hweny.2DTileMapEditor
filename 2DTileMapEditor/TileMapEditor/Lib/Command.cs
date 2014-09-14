using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 命令类
    /// </summary>
    public abstract class Command
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        protected Command()
        {
            _name = "";
        }
        protected Command(string name)
        {
            _name = name;
        }

        public abstract bool Undo();
        public abstract bool Redo();
    }
}
