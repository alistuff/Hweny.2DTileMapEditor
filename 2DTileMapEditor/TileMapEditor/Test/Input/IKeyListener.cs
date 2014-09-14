using System.Windows.Forms;

namespace TileMapEditor.Test.Input
{
    public interface IKeyListener
    {
        void KeyPressed(KeyEventArgs e);
        void KeyReleased(KeyEventArgs e);
    }
}
