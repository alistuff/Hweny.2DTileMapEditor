using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapEditor
{
    /// <summary>
    /// 用户定义
    /// </summary>
    class ApplicationConsts
    {
        //version
        public const string TILEMAP_VERSION="2DTileMapEditor.1.0";

        //registy
        public const string TILEMAP_FILETYPE_EXTENSION = ".rtmproj";
        public const string TILEMAP_FILETYPE_PROGRAMID = "2DTileMapEditor.Launcher._rtmproj";
        public const string TILEMAP_FILETYPE_DISPLAYNAME = "2DTileMapEditor File";
        public const string TILEMAP_SOFTWARE_REGISTRY_PATH = "Software\\HWL\\2DTileMapEditor";

        //setting
        public const string TILEMAP_SETTING_PATH = "TileMapEditor.setting";

        //project
        public const string TILEMAP_PROJECT_DIR = "Project";
        public const string TILEMAP_PROJECT_NAME = "Game";
        public const string TILEMAP_PROJECT_EXTENSION = ".rtmproj";
        public const string TILEMAP_PROJECT_FILE_FILTER = "rtmproj (*.rtmproj)|*.rtmproj|All File (*.*)|*.*";

        //map data dir
        public const string TILEMAP_DATA_DIR = "Data";
        public const string TILEMAP_DATA_MAP_DIR = TILEMAP_DATA_DIR + "\\Map";
        public const string TILEMAP_DATA_FILE_Extension = ".rtmdata";

        //map graphics dir
        public const string TILEMAP_GRAPHICS_DIR = "Graphics";
        public const string TILEMAP_GRAPHICS_TILESET_DIR = TILEMAP_GRAPHICS_DIR + "\\Tileset";
        
        //game
        public const string TILEMAP_GAME_SETTING_PATH = "TileMapEditor.game.setting";
        public const string TILEMAP_GAME_CHARSET_DIR = "Assets\\Charset";
    }
}
