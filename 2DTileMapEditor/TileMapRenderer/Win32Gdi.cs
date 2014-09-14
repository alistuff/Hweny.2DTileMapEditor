using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace TileMapRenderer
{
    public sealed class Win32Gdi
    {
        /// <summary>
        /// 对指定的源设备环境区域中的像素进行位块（bit_block）转换，以传送到目标设备环境
        /// </summary>
        /// <param name="hdcDest">指向目标设备环境的句柄</param>
        /// <param name="nXDest">指定目标矩形区域左上角的X轴逻辑坐标</param>
        /// <param name="nYDest">指定目标矩形区域左上角的Y轴逻辑坐标</param>
        /// <param name="nWidth">指定源和目标矩形区域的逻辑宽度</param>
        /// <param name="nHeight">指定源和目标矩形区域的逻辑高度</param>
        /// <param name="hdcSrc">指向源设备环境的句柄</param>
        /// <param name="nXSrc">指定源矩形区域左上角的X轴逻辑坐标</param>
        /// <param name="nYSrc">指定源矩形区域左上角的Y轴逻辑坐标</param>
        /// <param name="dwRop">指定光栅操作代码</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt
            (
                IntPtr hdcDest,
                int nXDest,
                int nYDest,
                int nWidth,
                int nHeight,
                IntPtr hdcSrc,
                int nXSrc,
                int nYSrc,
                Win32GdiRasterOperation dwRop
            );

        /// <summary>
        /// 对指定的源设备环境中的矩形区域像素的颜色数据进行位块（bit_block）转换，并将结果置于目标设备环境
        /// </summary>
        /// <param name="hdcDest">指向目标设备环境的句柄</param>
        /// <param name="nXOriginDest">指定目标矩形区域左上角的X轴逻辑坐标</param>
        /// <param name="nYOriginDest">指定目标矩形区域左上角的Y轴逻辑坐标</param>
        /// <param name="nWidthDest">指定源和目标矩形区域的逻辑宽度</param>
        /// <param name="hHeightDest">指定源和目标矩形区域的逻辑高度</param>
        /// <param name="hdcSrc">指向源设备环境的句柄</param>
        /// <param name="nXOriginSrc">指定源矩形区域左上角的X轴逻辑坐标</param>
        /// <param name="nYOriginSrc">指定源矩形区域左上角的Y轴逻辑坐标</param>
        /// <param name="nWidthSrc">指定源矩形的宽度</param>
        /// <param name="nHeightSrc">指定源矩形的高度</param>
        /// <param name="crTransparent">源位图中的RGB值当作透明颜色</param>
        /// <returns></returns>
        [DllImport("msimg32.dll")]
        public static extern bool TransparentBlt
            (
                IntPtr hdcDest,
                int nXOriginDest,
                int nYOriginDest,
                int nWidthDest,
                int hHeightDest,
                IntPtr hdcSrc,
                int nXOriginSrc,
                int nYOriginSrc,
                int nWidthSrc,
                int nHeightSrc,
                uint crTransparent
            );

        /// <summary>
        /// 创建一个与指定设备兼容的内存设备上下文环境
        /// </summary>
        /// <param name="hdc">现有设备上下文环境的句柄</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        /// <summary>
        /// 删除指定的设备上下文环境
        /// </summary>
        /// <param name="hdc">设备上下文环境的句柄</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        /// <summary>
        /// 选择一对象到指定的设备上下文环境中
        /// </summary>
        /// <param name="hdc">设备上下文环境的句柄</param>
        /// <param name="hgdiobj">被选择的对象的句柄</param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        /// <summary>
        /// 删除一个逻辑对象
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        public static uint ParseRGB(Color color)
        {
            return (uint)(((uint)color.B << 16) | (ushort)(((ushort)color.G << 8) | color.R));
        }

        public static byte AC_SRC_OVER = 0x00;
        public static byte AC_SRC_ALPHA = 0x01;

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            byte BlendOp;
            byte BlendFlags;
            byte SourceConstantAlpha;
            byte AlphaFormat;

            public BLENDFUNCTION(byte op, byte flags, byte alpha, byte format)
            {
                BlendOp = op;
                BlendFlags = flags;
                SourceConstantAlpha = alpha;
                AlphaFormat = format;
            }
        }

        [DllImport("gdi32.dll", EntryPoint = "GdiAlphaBlend")]
        public extern static Int32 AlphaBlend(IntPtr hdcDest, Int32 xDest, Int32 yDest,
            Int32 cxDest, Int32 cyDest, IntPtr hdcSrc, Int32 xSrc, Int32 ySrc,
            Int32 cxSrc, Int32 cySrc, BLENDFUNCTION blendFunction);

        public static Int32 AlphaBlend(IntPtr hdcDest, Rectangle dest, IntPtr hdcSrc,
            Rectangle src, BLENDFUNCTION blendFunction)
        {
            return AlphaBlend(hdcDest, dest.X, dest.Y, dest.Width, dest.Height,
                hdcSrc, src.X, src.Y, src.Width, src.Height, blendFunction);
        }
    }
}
