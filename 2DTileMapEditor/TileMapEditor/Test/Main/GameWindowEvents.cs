using System;
using System.Drawing;

namespace TileMapEditor.Test.Main
{
    public class UpdateFrameEventArgs : EventArgs
    {
        public float GameTime
        {
            get;
            private set;
        }
        public float ElapsedSeconds
        {
            get;
            private set;
        }
        public UpdateFrameEventArgs(float gameTime, float elapsedSeconds)
        {
            this.GameTime = gameTime;
            this.ElapsedSeconds = elapsedSeconds;
        }
    }

    public class RenderFrameEventArgs : EventArgs
    {
        public Graphics Graphics
        {
            get;
            private set;
        }
        public RenderFrameEventArgs(Graphics g)
        {
            this.Graphics = g;
        }
    }
}
