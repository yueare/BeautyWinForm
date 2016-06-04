using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FjuColorForm;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace FjuColorForm
{
    public partial class MyColorForm : FrmMain
    {
        public MyColorForm()
        {
            InitializeComponent();
            
        }
        //[System.Runtime.InteropServices.DllImport("user32")]
        //private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        ////正面_水平方向
        //const int AW_HOR_POSITIVE = 0x0001;
        ////负面_水平方向
        //const int AW_HOR_NEGATIVE = 0x0002;
        ////正面_垂直方向
        //const int AW_VER_POSITIVE = 0x0004;
        ////负面_垂直方向
        //const int AW_VER_NEGATIVE = 0x0008;
        ////由中间四周展开或由四周向中间缩小
        //const int AW_CENTER = 0x0010;
        ////隐藏对象
        //const int AW_HIDE = 0x10000;
        ////显示对象
        //const int AW_ACTIVATE = 0x20000;
        ////拉幕滑动效果
        //const int AW_SLIDE = 0x40000;
        ////淡入淡出渐变效果
        //const int AW_BLEND = 0x80000; 
        private void Form2_Load(object sender, EventArgs e)
        {
            Image back;
            Color color;
            Skin.readSkinImgINI(out back, out color);         
            BackColor = color;
                if (back != null)
                {              
                    Bitmap pic = Skin.SetEdgeBlur((Bitmap)back, color, 64, Skin.BLURSTYLE.RIGHT);
                    BackgroundImage = Skin.SetEdgeBlur(pic, color, 64, Skin.BLURSTYLE.BOTTOM);
                }
                //AnimateWindow(this.Handle, 2000,  AW_HOR_NEGATIVE);
        }
        

        private void Form2_SysBottomClick(object sender, CCWin.SkinControl.SysButtonEventArgs e)
        {
            Skin s = new Skin();
            s.BackColor = this.BackColor;
            s.BackgroundImage = this.BackgroundImage;
            s.ShowDialog();
        }
    }
}
