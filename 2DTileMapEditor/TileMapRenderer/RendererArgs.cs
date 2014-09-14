using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TileMapRenderer
{
    public delegate void RendererEventHandler(RendererArgs e);
    public class RendererArgs:EventArgs
    {
        protected Rectangle _clientRegion;
        public Rectangle ClientRegion
        {
            get { return _clientRegion; }
        }

        public RendererArgs(Rectangle region)
        {
            _clientRegion = region;
        }
    }
}
