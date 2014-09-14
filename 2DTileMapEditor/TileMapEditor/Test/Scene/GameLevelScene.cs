using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using TileMapLib;
using TileMapEditor.Test.Scene;
using TileMapEditor.Test.Main;
using TileMapEditor.Test.Sprite;

using TileMapEditor.Test.TileMap;
using TileMapEditor.Test.Setting;

namespace TileMapEditor.Test.Scene
{
    public class GameLevelScene : GameScene
    {
        private MyGame game;
        private TileMapRender tileMapRender;
        private Camera camera;
        private Player player;

        public GameLevelScene(MyGame game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            camera = new Camera(0, 0, game.Width, game.Height);
            tileMapRender = new TileMapRender(game.CurrentMap, camera);

            player = new Player(new Point(game.Width / 2, game.Height / 2));
            player.Initialize(game.CurrentMap, camera, game.AssetsLoader, game.Setting.GetCurrentPlayerSetting());

            AddKeyListener(player);
            AddMouseListener(player);
        }

        public override void Update(float gameTime, float elapsedSeconds)
        {
            player.Update(gameTime, elapsedSeconds);
            camera.Move(player.CenterPosition.X, player.CenterPosition.Y);
        }

        public override void Render(Graphics g)
        {
            g.Clear(Color.Black);
            tileMapRender.RenderBackground(g);
            player.Render(g);
            tileMapRender.RenderForeground(g);
        }
    }
}
