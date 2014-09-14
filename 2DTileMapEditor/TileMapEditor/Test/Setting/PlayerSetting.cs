using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using TileMapEditor.Test.Sprite;

namespace TileMapEditor.Test.Setting
{
    [Serializable]
    public class PlayerSetting
    {
        public string Name { get; set; }
        public string SpriteSheet { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float Accelerated { get; set; }
        public float MaxVelocity { get; set; }
        public Rectangle CollisionBounds { get; set; }
        public SpriteSheetSplitOptions SpliteOption { get; set; }
        public AnimationAttribute WalkLeftAttr;
        public AnimationAttribute WalkRightAttr;
        public AnimationAttribute WalkUpAttr;
        public AnimationAttribute WalkDownAttr;

        public PlayerSetting()
        {
            Name = string.Empty;
            SpriteSheet = string.Empty;
            Width = 1;
            Height = 1;
            Accelerated = 10000f;
            MaxVelocity = 200f;
            CollisionBounds = new Rectangle(0, 0, 1, 1);
            SpliteOption = SpriteSheetSplitOptions.FromRow;

            AnimationAttribute attrTemplate = new AnimationAttribute();
            attrTemplate.Loop = true;

            WalkLeftAttr = attrTemplate.Copy();
            WalkLeftAttr.Name = Player.ANI_LT;

            WalkRightAttr = attrTemplate.Copy();
            WalkRightAttr.Name = Player.ANI_RT;

            WalkUpAttr = attrTemplate.Copy();
            WalkUpAttr.Name = Player.ANI_UP;

            WalkDownAttr = attrTemplate.Copy();
            WalkDownAttr.Name = Player.ANI_DN;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    [Serializable]
    public struct AnimationAttribute
    {
        public string Name;
        public float Fps;
        public bool Loop;
        public int OffsetX;
        public int OffsetY;
        public int Frames;
        public int FrameWidth;
        public int FrameHeight;

        public AnimationAttribute Copy()
        {
            AnimationAttribute clone = new AnimationAttribute();
            clone.Name = this.Name;
            clone.Fps = this.Fps;
            clone.Loop = this.Loop;
            clone.OffsetX = this.OffsetX;
            clone.OffsetY = this.OffsetY;
            clone.Frames = this.Frames;
            clone.FrameWidth = this.FrameWidth;
            clone.FrameHeight = this.FrameHeight;
            return clone;
        }

        public string[] ToArray()
        {
            return new string[] { 
                Name,
                Fps.ToString(),
                Loop.ToString(),
                OffsetX.ToString(),
                OffsetY.ToString(),
                Frames.ToString(),
                FrameWidth.ToString(),
                FrameHeight.ToString()
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
