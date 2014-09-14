using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapEditor.Test.Sprite
{
    public class AnimationEventArgs : EventArgs
    {
        public AnimationKeyFrame CurrentFrame
        { 
            get;
            private set;
        }
        public AnimationEventArgs(AnimationKeyFrame frame)
        {
            this.CurrentFrame = frame;
        }
        public override string ToString()
        {
            return CurrentFrame.Source.ToString();
        }
    }
}
