using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapEditor.Test.Sprite
{
    public class AnimationKeyFrame
    {
        private Rectangle sourceRegion;
        public Rectangle Source
        {
            get
            { 
                return sourceRegion;
            }
        }
        public int X
        {
            get 
            { 
                return sourceRegion.X;
            }
        }
        public int Y
        {
            get 
            { 
                return sourceRegion.Y;
            }
        }
        public int Width
        {
            get 
            {
                return sourceRegion.Width;
            }
        }
        public int Height
        {
            get 
            {
                return sourceRegion.Height;
            }
        }
        public AnimationKeyFrame(int x, int y, int width, int height)
        {
            sourceRegion = new Rectangle(x, y, width, height);
        }
    }
}
