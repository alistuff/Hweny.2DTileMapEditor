using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapEditor.Test.Main
{
    public class Camera
    {
        public int X
        {
            get;
            set;
        }
        public int Y
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public int Height
        {
            get;
            set;
        }

        public Camera(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        public Camera(int x, int y, int width, int height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public void Move(float playerX, float playerY)
        {
            X = (int)(Math.Ceiling(playerX - (Width >> 1)));
            Y = (int)(Math.Ceiling(playerY - (Height >> 1)));
        }
    }
}
