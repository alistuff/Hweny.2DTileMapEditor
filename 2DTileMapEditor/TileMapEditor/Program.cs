/* 
 * ///////////////////////////////////////////////////////////////////// 
 * Filename: Add License
 * Author  : [LI-Games.ALi][alistuff@163.com] 
 * Date    : 2014/7/14    
 * Resume  : 基于Tile的2D地图编辑器，支持创建多图层地图及编辑障碍物
 *           
 * ///////////////////////////////////////////////////////////////////// 
 * Modifiy History 
 *  
 * Date    :
 * Resume  :
 *  
 */

// github：https://github.com/alistuff/Hweny.2DTileMapEditor
// e-mail：alistuff@163.com 

//The MIT License (MIT)
//
//Copyright (c) 2014 alistuff
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TileMapEditor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Forms.frm_MainForm mainForm = new TileMapEditor.Forms.frm_MainForm();

            if (args.Length > 0)
            {
                mainForm.Argument = args[0];
            }

            Application.Run(mainForm);
        }
    }
}
