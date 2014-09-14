using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using TileMapRenderer;

namespace TileMapEditor.Lib
{
    /// <summary>
    /// 应用程序参数配置类
    /// </summary>
    [Serializable]
    public class Setting
    {
        private static Setting _instance;

        private TileSetRendererStyle _tilesetStyle = new TileSetRendererStyle();
        private MapRendererStyle _mapStyle = new MapRendererStyle();
        private int _mruFileCount = 10;
        private int _mruFileCharsLength = 50;
        private int _undoLevel = 20;

        public TileSetRendererStyle TileSetStyle
        {
            get { return _tilesetStyle; }
            set { _tilesetStyle = value; }
        }

        public MapRendererStyle TileMapStyle
        {
            get { return _mapStyle; }
            set { _mapStyle = value; }
        }

        public int MruFileCount
        {
            get { return _mruFileCount; }
            set { _mruFileCount = value; }
        }

        public int MruFileCharsLength
        {
            get { return _mruFileCharsLength; }
            set { _mruFileCharsLength = value; }
        }

        public int UndoLevel
        {
            get { return _undoLevel; }
            set { _undoLevel = value; }
        }

        public static Setting CreateInstance()
        {
            if (_instance == null)
            {
                LoadSetting();
            }

            return _instance;
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        private static void LoadSetting()
        {
            string filename = Application.StartupPath + "\\" + ApplicationConsts.TILEMAP_SETTING_PATH;
            if (File.Exists(filename))
            {
                Stream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                IFormatter bf = new BinaryFormatter();
                Setting setting = (Setting)bf.Deserialize(fs);
                _instance = setting;
                fs.Close();
                fs = null;
            }
            else
            {
                _instance = new Setting();
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
            string filename = Application.StartupPath + "\\" + ApplicationConsts.TILEMAP_SETTING_PATH;
            //string fileDir = Path.GetDirectoryName(filename);

            //if (!Directory.Exists(fileDir))
            //    Directory.CreateDirectory(fileDir);

            Stream fs = new FileStream(filename,FileMode.Create,FileAccess.ReadWrite);
            IFormatter bf = new BinaryFormatter();
            bf.Serialize(fs,this);
            fs.Close();
            fs = null;
        }
    }
}
