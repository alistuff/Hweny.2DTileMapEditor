using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TileMapEditor.Test.Setting
{
    public interface ISettingPage
    {
        Control Owner { get; }
        string PageName { get; }
        void HidePage();
        void ShowPage();
        void Save();
    }
}
