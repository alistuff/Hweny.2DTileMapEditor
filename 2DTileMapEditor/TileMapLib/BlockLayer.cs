using System;
using System.Collections.Generic;
using System.Text;

namespace TileMapLib
{
    /// <summary>
    /// 阻挡层
    /// </summary>
    public class BlockLayer : Layer<bool>
    {
        public BlockLayer(int width, int height)
            : base(width, height)
        {
            _visible = false;
        }
    }
}
