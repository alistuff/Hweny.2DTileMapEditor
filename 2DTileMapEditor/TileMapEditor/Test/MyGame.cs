using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using TileMapLib;
using TileMapEditor.Test.Main;
using TileMapEditor.Test.Scene;
using TileMapEditor.Test.Setting;

namespace TileMapEditor.Test
{
    public class MyGame : Game
    {
        //scene
        public const string SCENE_PLAY = "PLAY";

        private Form parent;

        public event EventHandler GameWindowShutDown;
        public Map CurrentMap { get; private set; }
        public AssetsLoader AssetsLoader { get; private set; }
        public GameSetting Setting { get; private set; }

        public MyGame(Form parent, Map map)
        {
            this.parent = parent;
            this.CurrentMap = map;
        }

        protected override void LoadContent()
        {
            //load setting
            Setting = GameSetting.FromFile(Application.StartupPath + "//" +
                ApplicationConsts.TILEMAP_GAME_SETTING_PATH);

            //load assets
            AssetsLoader = new MyAssetsLoader(Setting);
            AssetsLoader.LoadAssets();
        }

        protected override void UnLoadContent()
        {
            //dispose assets
            AssetsLoader.Dispose();
            Setting = null;

            var temp = GameWindowShutDown;
            if (temp != null)
            {
                temp(this, null);
                temp = null;
            }
        }

        protected override void Initialize()
        {
            Title = Setting.Title;
            Width = Setting.Width;
            Height = Setting.Height;
            TargetFps = Setting.TargetFps;

            Window.Owner = parent;

            Gsm.Add(SCENE_PLAY, new GameLevelScene(this));
            Gsm.Push(SCENE_PLAY);
        }
    }
}
