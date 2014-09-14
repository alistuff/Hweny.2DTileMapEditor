using System;
using System.Collections.Generic;
using System.Text;
using TileMapEditor.Test.Main;
using System.Drawing;
using TileMapEditor.Test.Sprite;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TileMapEditor.Test.Setting
{
    [Serializable]
    public class GameSetting
    {
        public string Title { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TargetFps { get; set; }
        public string CurrentPlayer { get; set; }
        public GameType GameType { get; set; }
        public List<PlayerSetting> PlayerSettings { get; set; }

        public GameSetting()
        {
            PlayerSettings = new List<PlayerSetting>();
            SetDefault();
        }

        public void SetDefault()
        {
            Title = "LI-Games";
            Width = 480;
            Height = 320;
            TargetFps = 10000;
            GameType = GameType.TopDown2d;

            if (PlayerSettings.Count > 0)
                CurrentPlayer = PlayerSettings[0].Name;
            else
                CurrentPlayer = string.Empty;
        }

        public PlayerSetting AddPlayer(string name)
        {
            if (GetPlayerByName(name) != null)
                throw new ArgumentException("name");

            PlayerSetting ps = new PlayerSetting();
            ps.Name = name;
            PlayerSettings.Add(ps);
            return ps;
        }

        public void RemovePlayer(int index)
        {
            PlayerSettings.RemoveAt(index);
        }

        public PlayerSetting GetCurrentPlayerSetting()
        {
            PlayerSetting ps = GetPlayerByName(CurrentPlayer);
            if (ps != null && ps.SpriteSheet != string.Empty)
                return ps;
            return null;
        }

        private PlayerSetting GetPlayerByName(string name)
        {
            for (int i = 0; i < PlayerSettings.Count; i++)
            {
                if (PlayerSettings[i].Name == name)
                    return PlayerSettings[i];
            }
            return null;
        }

        public static GameSetting FromFile(string fileName)
        {
            try
            {
                IFormatter ft = new BinaryFormatter();
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                GameSetting setting = (GameSetting)ft.Deserialize(fs);
                fs.Close();
                return setting;
            }
            catch
            {
                return new GameSetting();
            }
        }

        public void Save(string fileName)
        {
            try
            {
                IFormatter ft = new BinaryFormatter();
                System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
                ft.Serialize(fs, this);
                fs.Close();
            }
            catch (System.IO.IOException e)
            {
                throw e;
            }
        }
    }
}
