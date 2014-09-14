using System;
using System.Collections.Generic;
using System.Text;
using TileMapEditor.Test.Main;
using TileMapEditor.Test.Setting;

namespace TileMapEditor.Test
{
    public class MyAssetsLoader : AssetsLoader
    {
        public const string IM_PLAYER = "player";

        private GameSetting setting;

        public MyAssetsLoader(GameSetting setting)
            : base()
        {
            this.setting = setting;
        }

        public override void LoadAssets()
        {
            var currentPlayer = setting.GetCurrentPlayerSetting();

            try
            {
                if (currentPlayer != null)
                {
                    AddImage(IM_PLAYER, System.Windows.Forms.Application.StartupPath + "//"
                        + ApplicationConsts.TILEMAP_GAME_CHARSET_DIR + "//"
                        + currentPlayer.SpriteSheet);
                }
                else
                {
                    AddImage(IM_PLAYER, Properties.Resources.player);
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw e;
            }
        }
    }
}
