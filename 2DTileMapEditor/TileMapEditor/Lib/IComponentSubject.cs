using System;

namespace TileMapEditor.Lib
{
    public interface IComponentSubject
    {
        void RegisterObserver(IComponentObserver observer);
        void RemoveObserver(IComponentObserver observer);

        void Notification();
    }
}
