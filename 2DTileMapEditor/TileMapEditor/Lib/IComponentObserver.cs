using System;
using TileMapLib;

namespace TileMapEditor.Lib
{
    public interface IComponentObserver
    {
        void Update(EditableMap mapData);
    }
}
