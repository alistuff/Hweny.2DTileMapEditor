using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace TileMapEditor.Test.Main
{
    public abstract class AssetsLoader : IDisposable
    {
        private Dictionary<string, Image> imageAssets;
        //Other assets

        public AssetsLoader()
        {
            imageAssets = new Dictionary<string, Image>();
        }

        public abstract void LoadAssets();

        public Image GetImage(string key)
        {
            if (!imageAssets.ContainsKey(key))
                throw new ArgumentException("key");

            return imageAssets[key];
        }
        //GetOther(string key);

        protected virtual void AddImage(string key, string fileName)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (!File.Exists(fileName))
                throw new FileNotFoundException("fileName");

            imageAssets.Add(key, Image.FromFile(fileName));
        }

        protected virtual void AddImage(string key, Image image)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (image == null)
                throw new FileNotFoundException("image");

            imageAssets.Add(key, image);
        }

        //AddOther(string key, string fileName);

        public void Dispose()
        {
            foreach (string key in imageAssets.Keys)
            {
                var temp = imageAssets[key];
                temp.Dispose();
                temp = null;
            }
            imageAssets.Clear();
        }
    }
}
