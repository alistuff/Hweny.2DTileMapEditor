using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace TileMapLib
{
    public class Map : IComparable
    {
        protected int _id;
        protected string _name;
        protected int _width;
        protected int _height;
        protected int _tileWidth;
        protected int _tileHeight;
        protected List<Tileset> _tilesets;
        protected List<MapLayer> _mapLayers;
        protected BlockLayer _blockLayer;

        public delegate Image ImportTileset(string fileName);

        public int ID
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int RealWidth
        {
            get { return _width * _tileWidth; }
        }

        public int RealHeight
        {
            get { return _height * _tileHeight; }
        }

        public int TileWidth
        {
            get { return _tileWidth; }
        }

        public int TileHeight
        {
            get { return _tileHeight; }
        }

        public List<Tileset> Tilesets
        {
            get { return _tilesets; }
        }

        public List<MapLayer> MapLayers
        {
            get { return _mapLayers; }
        }

        public BlockLayer BlockLayer
        {
            get { return _blockLayer; }
        }

        //public EventLayer EventLayer
        //{
        //    get { return _eventLayer; }
        //}

        public Map()
        {
            _id = 0;
            _name = "";
            _width = 0;
            _height = 0;
            _tileWidth = 0;
            _tileHeight = 0;
            _tilesets = new List<Tileset>(0);
            _mapLayers = new List<MapLayer>(0);
            _blockLayer = new BlockLayer(_width, _height);
            //  _eventLayer = new EventLayer(width, height);
        }

        public void Load(string filePath, ImportTileset importTileset)
        {
            Stream fs = File.Open(filePath, FileMode.Open, FileAccess.Read);
            Load(fs, importTileset);
            fs.Close();
        }
        public void Load(Stream stream, ImportTileset importTileset)
        {
            ReadMapFileHeader(stream);
            ReadMapFileInfo(stream);
            ReadMapFileData(stream, importTileset);
            ReadMapFileEnd(stream);
        }
        public void Save(string filePath)
        {
            Stream fs = File.Open(filePath, FileMode.Create, FileAccess.Write);
            Save(fs);
            fs.Close();
        }
        public void Save(Stream stream)
        {
            WriteMapFileHeader(stream);
            WriteMapFileInfo(stream);
            WriteMapFileData(stream);
            WriteMapFileEnd(stream);
        }
        public void SaveTileset(string filePath)
        {
            if (_tilesets.Count == 0) return;
            foreach (Tileset tileset in _tilesets)
            {
                if (tileset.Image != null)
                {
                    tileset.Image.Save(string.Format("{0}\\{1}{2}", filePath, tileset.Name, ".png"),
                        System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        #region private functions

        private void ReadMapFileHeader(Stream stream)
        {
            byte[] buffer = new byte[128];
            string copyright = ReadString(stream);
            if (copyright != MapFileHeader.MAP_FILE_COPYRIGHT)
                throw new Exception("非RpgTileMapEditor文件!");
            stream.Read(buffer, 0, 4);
            float version = BitConverter.ToSingle(buffer, 0);
            if (version > MapFileHeader.MAP_FILE_VERSION)
                throw new Exception("软件版本不兼容此文件!");
        }

        private void ReadMapFileInfo(Stream stream)
        {
            byte[] buffer = new byte[128];
            _name = ReadString(stream);
            stream.Read(buffer, 0, 20);
            _id = BitConverter.ToInt32(buffer, 0);
            _width = BitConverter.ToInt32(buffer, 4);
            _height = BitConverter.ToInt32(buffer, 8);
            _tileWidth = BitConverter.ToInt32(buffer,12);
            _tileHeight = BitConverter.ToInt32(buffer,16);
        }

        private void ReadMapFileData(Stream stream, ImportTileset importTileset)
        {
            byte[] buffer = new byte[128];
            stream.Read(buffer, 0, 4);
            int count = BitConverter.ToInt32(buffer, 0);
            for (int i = 0; i < count; i++)
            {
                string name = ReadString(stream);
                Image image = importTileset(name);

                stream.Read(buffer, 0, 20);
                int id = BitConverter.ToInt32(buffer, 0);
                int tileWidth = BitConverter.ToInt32(buffer, 12);
                int tileHeight = BitConverter.ToInt32(buffer, 16);

                Tileset tileset = new Tileset(id, name, tileWidth, tileHeight, image);
                _tilesets.Add(tileset);
            }

            stream.Read(buffer, 0, 4);
            count = BitConverter.ToInt32(buffer, 0);
            for (int i = 0; i < count; i++)
            {
                string name = ReadString(stream);
                stream.Read(buffer, 0, 10);
                int id = BitConverter.ToInt32(buffer, 0);
                int zindex = BitConverter.ToInt32(buffer, 4);
                bool background = BitConverter.ToBoolean(buffer, 8);
                bool visible = BitConverter.ToBoolean(buffer, 9);

                MapLayer layer = new MapLayer(id, name, _width, _height, zindex, background);
                layer.Visible = visible;
                layer.Resize(_width, _height);

                for (int x = 0; x < _width; x++)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        stream.Read(buffer, 0, 1);
                        bool existsTile = BitConverter.ToBoolean(buffer, 0);
                        if (existsTile)
                        {
                            stream.Read(buffer, 0, 8);
                            int tsId = BitConverter.ToInt32(buffer, 0);
                            int offset = BitConverter.ToInt32(buffer, 4);

                            foreach (Tileset ts in _tilesets)
                            {
                                if (ts.ID == tsId)
                                {
                                    layer[x, y] = new Tile(ts, offset);
                                    break;
                                }
                            }
                        }
                    }
                }

                _mapLayers.Add(layer);
            }

          //  stream.Read(buffer, 0, 4);
            _blockLayer.Resize(_width, _height);
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    stream.Read(buffer, 0, 1);
                    bool block = BitConverter.ToBoolean(buffer, 0);
                    _blockLayer[x, y] = block;
                }
            }
        }

        private void ReadMapFileEnd(Stream stream)
        {
          //  byte[] buffer = new byte[128];
         //   stream.Read(buffer, 0, 1);
            int endding = stream.ReadByte();
            if (endding != MapFileHeader.MAP_FILE_ENDDING)
                throw new Exception("文件加载失败!");
        }

        private void WriteMapFileHeader(Stream stream)
        {
            WriteString(stream, MapFileHeader.MAP_FILE_COPYRIGHT);
            WriteFloat(stream, MapFileHeader.MAP_FILE_VERSION);
        }

        private void WriteMapFileInfo(Stream stream)
        {
            WriteString(stream, _name);
            WriteInt(stream, _id);
            WriteInt(stream, _width);
            WriteInt(stream, _height);
            WriteInt(stream, _tileWidth);
            WriteInt(stream, _tileHeight);
        }

        private void WriteMapFileData(Stream stream)
        {
            WriteInt(stream, _tilesets.Count);
            foreach (Tileset tileset in _tilesets)
            {
                WriteString(stream, tileset.Name);
                WriteInt(stream, tileset.ID);
                WriteInt(stream, tileset.Width);
                WriteInt(stream, tileset.Height);
                WriteInt(stream, tileset.TileWidth);
                WriteInt(stream, tileset.TileHeight);
            }

            WriteInt(stream, _mapLayers.Count);
            foreach (MapLayer layer in _mapLayers)
            {
                WriteString(stream, layer.Name);
                WriteInt(stream, layer.ID);
                WriteInt(stream, layer.ZIndex);
                WriteBoolean(stream, layer.BackgroundLayer);
                WriteBoolean(stream, layer.Visible);

                for (int x = 0; x < _width; x++)
                {
                    for (int y = 0; y < _height; y++)
                    {
                        if (layer[x, y] == null || layer[x, y].IsEmpty)
                        {
                            WriteBoolean(stream, false);
                        }
                        else
                        {
                            WriteBoolean(stream, true);
                            WriteInt(stream, layer[x, y].Tileset.ID);
                            WriteInt(stream, layer[x, y].OffsetID);
                        }
                    }
                }
            }

           // Utility.WriteInt(stream, 1);
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    WriteBoolean(stream, _blockLayer[x, y]);
                }
            }
        }

        private void WriteMapFileEnd(Stream stream)
        {
            stream.WriteByte(MapFileHeader.MAP_FILE_ENDDING);
            // Utility.WriteInt(stream, MapFileHeader.MAP_FILE_ENDDING);
        }

        public void WriteInt(Stream outStream, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            outStream.Write(buffer, 0, buffer.Length);
        }

        public void WriteFloat(Stream outStream, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            outStream.Write(buffer, 0, buffer.Length);
        }

        public void WriteBoolean(Stream outStream, bool value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            outStream.Write(buffer, 0, buffer.Length);
        }

        public void WriteString(Stream outStream, string value)
        {
            byte[] buffer = Encoding.Default.GetBytes(value);
            WriteInt(outStream, buffer.Length);
            outStream.Write(buffer, 0, buffer.Length);
        }

        public string ReadString(Stream inStream)
        {
            byte[] buffer = new byte[128];
            inStream.Read(buffer, 0, 4);
            int length = BitConverter.ToInt32(buffer, 0);
            inStream.Read(buffer, 0, length);
            return Encoding.Default.GetString(buffer, 0, length);
        }

        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            int result = 0;
            if (_id > ((Map)obj).ID)
                result = 1;
            else if (_id < ((Map)obj).ID)
                result = -1;
            return result;
        }

        #endregion
    }
}
