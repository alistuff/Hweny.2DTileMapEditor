using System;
using System.Collections.Generic;
using System.Text;
using TileMapLib;
using System.Drawing;
using TileMapRenderer;
using TileMapEditor.Test.Main;

namespace TileMapEditor.Test.TileMap
{
    public class TileMapRender
    {
        private Map tileMap;
        private Camera camera;

        public TileMapRender(Map tileMap, Camera camera)
        {
            this.tileMap = tileMap;
            this.camera = camera;
        }

        public void RenderBackground(Graphics g)
        {
            RenderTileMapWithGDI(g, true);
            // RenderTileMap(g,true);
        }
        public void RenderForeground(Graphics g)
        {
            RenderTileMapWithGDI(g, false);
            // RenderTileMap(g, false);
        }

        //win32 gdi
        private void RenderTileMapWithGDI(Graphics g, bool backgroundLayer)
        {
            IntPtr target = g.GetHdc();
            IntPtr memoryDc = Win32Gdi.CreateCompatibleDC(target);

            int cameraMinX = camera.X / tileMap.TileWidth;
            int cameraMinY = camera.Y / tileMap.TileHeight;
            int cameraMaxX = (int)Math.Floor((double)camera.Width / tileMap.TileWidth + cameraMinX);
            int cameraMaxY = (int)Math.Floor((double)camera.Height / tileMap.TileHeight + cameraMinY);

            List<MapLayer> mapLayers = tileMap.MapLayers;
            for (int i = 0; i < mapLayers.Count; i++)
            {
                if (mapLayers[i].Visible && (mapLayers[i].BackgroundLayer == backgroundLayer))
                {
                    for (int x = cameraMinX; x <= cameraMaxX; x++)
                    {
                        for (int y = cameraMinY; y <= cameraMaxY; y++)
                        {
                            if (x < 0 || x > tileMap.Width - 1 || y < 0 || y > tileMap.Height - 1)
                                continue;

                            Tile tile = mapLayers[i][x, y];

                            if (tile == null || tile.IsEmpty)
                                continue;

                            IntPtr oldObj = Win32Gdi.SelectObject(memoryDc, tile.Tileset.PtrImage);

                            Rectangle dest = new Rectangle(
                                x * tileMap.TileWidth - camera.X,
                                y * tileMap.TileHeight - camera.Y,
                                tileMap.TileWidth,
                                tileMap.TileHeight
                            );

                            Rectangle src = new Rectangle(
                                tile.OffsetX,
                                tile.OffsetY,
                                tile.Tileset.TileWidth,
                                tile.Tileset.TileHeight
                            );

                            Win32Gdi.AlphaBlend(
                                target,
                                dest,
                                memoryDc,
                                src,
                                new Win32Gdi.BLENDFUNCTION(Win32Gdi.AC_SRC_OVER, 0, 255, Win32Gdi.AC_SRC_ALPHA)
                            );

                            Win32Gdi.SelectObject(memoryDc, oldObj);
                        }
                    }
                }
            }

            Win32Gdi.DeleteDC(memoryDc);
            g.ReleaseHdc(target);
        }

        //.net gdi+
        private void RenderTileMap(Graphics g, bool background)
        {
            var cameraMinX = camera.X / tileMap.TileWidth;
            var cameraMinY = camera.Y / tileMap.TileHeight;
            var cameraMaxX = (int)Math.Floor((double)camera.Width / tileMap.TileWidth + cameraMinX);
            var cameraMaxY = (int)Math.Floor((double)camera.Height / tileMap.TileHeight + cameraMinY);

            var mapLayers = tileMap.MapLayers;
            for (int i = 0; i < mapLayers.Count; i++)
            {
                if (mapLayers[i].Visible && (mapLayers[i].BackgroundLayer == background))
                {
                    for (int x = cameraMinX; x <= cameraMaxX; x++)
                    {
                        for (int y = cameraMinY; y <= cameraMaxY; y++)
                        {
                            if (x < 0 || x > tileMap.Width - 1) continue;
                            if (y < 0 || y > tileMap.Height - 1) continue;

                            var tile = mapLayers[i][x, y];
                            if (tile != null && !tile.IsEmpty)
                            {
                                g.DrawImage(tile.Tileset.Image,
                                    new Rectangle(x * tileMap.TileWidth - camera.X, y * tileMap.TileHeight - camera.Y,
                                        tileMap.TileWidth, tileMap.TileHeight),
                                    new Rectangle(tile.OffsetX, tile.OffsetY,
                                        tileMap.TileWidth, tileMap.TileHeight),
                                    GraphicsUnit.Pixel
                                );
                            }
                        }
                    }
                }
            }
        }
    }
}
