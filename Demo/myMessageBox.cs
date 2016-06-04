using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FjuColorForm;

namespace FjuColorForm
{
    public partial class myMessageBox : FrmMain
    {
        
        public enum  myMessageBoxIcon
        {
            ok=0, warn=1, error=2,qustion=3,info=4
        };
        public enum myMessageBoxButtion
        {
            OK=1,YESORNO=2
        }

        public myMessageBox()
        {
            
            InitializeComponent();
           
        }

        private void myMessageBox_Load(object sender, EventArgs e)
        {
            SysButtonItems.Clear();
            Width = label1.Width + label1.Location.X*2;
            skinPanel1.Width = this.Width;
            skinPanel1.Height = this.Height - 30;
            skinPanel1.Location = new Point(0, 30);
            button1.Location = new Point(Width-button1.Width-20,button1.Location.Y);
            button2.Location = new Point(button1.Location.X - button2.Width - 10, button1.Location.Y);
            this.Left = (Screen.PrimaryScreen.WorkingArea.Width - Width) / 2;
            this.Top = (Screen.PrimaryScreen.WorkingArea.Height - Height) / 2;

            BackColor = Skin.readSkinINI();
        
        }
        public static DialogResult show(string msg)
        {
            myMessageBox m = new myMessageBox();
            m.label1.Text = msg;       
            m.skinPictureBox1.Visible = false;
            m.button2.Visible = false;
            m.ShowDialog();
            return m.DialogResult;
        }
        public static DialogResult show(string msg,string title)
        {
            myMessageBox m = new myMessageBox();
            m.label1.Text = msg;
            m.Text = title;
            m.skinPictureBox1.Visible = false;
            m.ShowDialog();    
            return m.DialogResult;
        }
        public static DialogResult show(string msg, string title,myMessageBoxIcon ico,myMessageBoxButtion btn)
        {
            myMessageBox m = new myMessageBox();
            m.label1.Text = msg;
            m.Text = title;
            m.skinPictureBox1.Image = m.imageList1.Images[(int)ico];
            if (btn == myMessageBoxButtion.OK)
                m.button2.Visible = false;
            else if(btn==myMessageBoxButtion.YESORNO)
            {
                m.button1.Text = "是";
                m.button1.DialogResult = DialogResult.Yes;
                m.button2.DialogResult = DialogResult.No;
            }


            m.ShowDialog();
            return m.DialogResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void skinPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timerm_Tick(object sender, EventArgs e)
        {

        }
       
     
    }
}
