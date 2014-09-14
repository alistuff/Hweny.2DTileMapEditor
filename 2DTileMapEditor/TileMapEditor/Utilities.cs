using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace TileMapEditor
{
    public static class Utilities
    {
        /// <summary>
        /// 把源位图转换成32位带alpha通道的png格式位图
        /// </summary>
        /// <param name="source"></param>
        /// <param name="transparentColor"></param>
        /// <returns></returns>
        public static Bitmap BitmapToPng32Argb(Bitmap source, Color transparentColor)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }
            if (source.PixelFormat != PixelFormat.Format8bppIndexed &&
                source.PixelFormat != PixelFormat.Format24bppRgb &&
                source.PixelFormat != PixelFormat.Format32bppRgb &&
                source.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ArgumentException("源图像色深不是8位 24位 32位格式!");
            }
            if (source.PixelFormat == PixelFormat.Format32bppArgb && transparentColor == Color.Empty)
            {
                return (Bitmap)source.Clone();
            }
            try
            {
                Bitmap newPng = new Bitmap(source.Width, source.Height, PixelFormat.Format32bppArgb);
                unsafe
                {
                    BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height),
                            ImageLockMode.ReadOnly, source.PixelFormat);
                    BitmapData destData = newPng.LockBits(new Rectangle(0, 0, newPng.Width, newPng.Height),
                            ImageLockMode.WriteOnly, newPng.PixelFormat);

                    //8位调色板
                    Color[] palette = source.Palette.Entries;

                    for (int x = 0; x < source.Width; x++)
                    {
                        for (int y = 0; y < source.Height; y++)
                        {
                            byte* pSource = (byte*)sourceData.Scan0 + y * sourceData.Stride;
                            byte* pDest = (byte*)destData.Scan0 + x * 4 + y * destData.Stride;
                            byte a = 255, r = 0, g = 0, b = 0;

                            if (source.PixelFormat == PixelFormat.Format8bppIndexed)
                            {
                                pSource += x;
                                b = palette[*pSource].B;
                                g = palette[*pSource].G;
                                r = palette[*pSource].R;
                            }
                            else if (source.PixelFormat == PixelFormat.Format24bppRgb)
                            {
                                pSource += x * 3;
                                b = *pSource;
                                g = *(pSource + 1);
                                r = *(pSource + 2);
                            }
                            else if (source.PixelFormat == PixelFormat.Format32bppRgb)
                            {
                                pSource += x * 4;
                                b = *pSource;
                                g = *(pSource + 1);
                                r = *(pSource + 2);
                            }
                            else if (source.PixelFormat == PixelFormat.Format32bppArgb)
                            {
                                pSource += x * 4;
                                b = *pSource;
                                g = *(pSource + 1);
                                r = *(pSource + 2);
                                a = *(pSource + 3);
                            }

                            if (Color.FromArgb(a, r, g, b) != Color.FromArgb(transparentColor.A,
                                transparentColor.R, transparentColor.G, transparentColor.B))
                            {
                                *(pDest) = b;
                                *(pDest + 1) = g;
                                *(pDest + 2) = r;
                                *(pDest + 3) = a;
                            }
                        }
                    }
                    source.UnlockBits(sourceData);
                    newPng.UnlockBits(destData);
                }
                return newPng;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string ToGZipBase64String(Image image)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var zs = new GZipStream(ms, CompressionMode.Compress))
                    {
                        image.Save(zs, ImageFormat.Png);
                        zs.Close();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Image GZipBase64StringToBitmap(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                throw new ArgumentNullException("base64String");

            try
            {
                using (var ms = new MemoryStream(Convert.FromBase64String(base64String)))
                {
                    using (var zs = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        using (var rs = new MemoryStream())
                        {
                            byte[] buffer = new byte[4096];
                            var read = 0;
                            while ((read = zs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                rs.Write(buffer, 0, read);
                            }
                            return Image.FromStream(rs);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
