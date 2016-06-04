using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using FjuColorForm;
using System.Net.NetworkInformation;
using System.Net;

namespace FjuColorForm
{
    public partial class Skin : FjuColorForm.FrmMain
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        private int LVM_SETICONSPACING = 0x1035;
        private static int size = 6;
        private int width=16*size;
        private int height=9*size;
        public Skin()
        {
            InitializeComponent();
            SendMessage(this.listView1.Handle, LVM_SETICONSPACING, 0, 0x10000 * height + width);
        }
        public static Color[] COLOR = {  
                                          Color.FromArgb(255, 196, 43, 45), 
                                          Color.FromArgb(255, 244, 143, 51), 
                                          Color.FromArgb(255, 32, 64, 89) ,
                                          Color.FromArgb(255, 63, 142, 137),
                                          Color.FromArgb(255, 237,56,107),
                                          Color.FromArgb(255, 181,230,29),
                                          Color.FromArgb(255, 191, 181, 227),                                       
                                          Color.FromArgb(255, 163, 73, 164), 
                                          Color.FromArgb(255, 0, 0, 128),
                                          Color.FromArgb(255, 0, 0, 0)
                                      };

        private void Skin_Load(object sender, EventArgs e)
        {
            //SysButtonItems.Clear();
            skinPanel1.Location = new Point(0, 30);
            skinPanel1.Width = this.Width;
            
            //skinPanel1.Height = this.Height - 30;

            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.WorkerReportsProgress = true;
            Control.CheckForIllegalCrossThreadCalls = false; 

        }
        private Image[] skinImgs;
        private string[] skinsPath;
        private string MainImageName = "\\main_r.jpg";

        private void getSkinLib()//获取图片库
        {
            string path = "Skins\\";
            
            try
            {             
                skinsPath = Directory.GetDirectories(path);
                skinImgs = new Image[skinsPath.Length];//多少个皮肤
                for(int i=0;i<skinsPath.Length;i++)
                {
                    skinImgs[skinsPath.Length-i-1] = Image.FromFile(skinsPath[i] + MainImageName);       
                }
            }
            catch (Exception ee)
            {
                myMessageBox.show(ee.Message);
            }
           
        }

        public static void saveSkinINI(int index)//颜色皮肤single
        {
            StreamWriter sw = new StreamWriter("skin.ini");
            string ss;
            ss = "Color=" + Skin.COLOR[index - 1].A.ToString()+",";
            ss += Skin.COLOR[index - 1].R.ToString() + ",";
            ss += Skin.COLOR[index - 1].G.ToString() + ",";
            ss += Skin.COLOR[index - 1].B.ToString();
            sw.WriteLine(ss);      
            sw.Close();
        }
        public static Color readSkinINI()
        {
            StreamReader sr;
            try
            {
                sr = new StreamReader("skin.ini");
            }
            catch
            {
                return Skin.COLOR[0];
            }
            int A, R, G, B;
            string tmp = sr.ReadLine();
            if (tmp == null)
            {
                return Skin.COLOR[0];
            }
                
            sr.Close();
            tmp = tmp.Substring(6, tmp.Length - 6);
            string[] str = tmp.Split(',');
            A = int.Parse(str[0]);
            R = int.Parse(str[1]);
            G = int.Parse(str[2]);
            B = int.Parse(str[3]);

            return Color.FromArgb(A, R, G, B);

        }
        public  void saveSkinImgINI(int index)//图片皮肤both
        {
            StreamWriter sw = new StreamWriter("skin.ini");
            string ss;
            Color tmp = getMainColor(skinImgs[index]);
            ss = "Color=" + tmp.A.ToString() + ",";
            ss += tmp.R.ToString() + ",";
            ss += tmp.G.ToString() + ",";
            ss += tmp.B.ToString();
            sw.WriteLine(ss);

            ss = "Path=";
            ss += skinsPath[skinImgs.Length - index-1] + MainImageName;
            sw.WriteLine(ss);        
            sw.Close();
            
        }

        public static void readSkinImgINI(out Image img,out Color color)
        {
            StreamReader sr;
            try
            {

                sr = new StreamReader("skin.ini");               
            }
            catch
            {
                img = null;
                color = Skin.COLOR[0];        
                return;
            }

            string tmp;
            //color
            int A, R, G, B;
            tmp = sr.ReadLine();
            if(tmp==null)
            {
                img = null;
                color = Skin.COLOR[0];
                return;
            }
                
            tmp = tmp.Substring(6, tmp.Length - 6);
            string[] str = tmp.Split(',');
            A = int.Parse(str[0]);
            R = int.Parse(str[1]);
            G = int.Parse(str[2]);
            B = int.Parse(str[3]);
            color = Color.FromArgb(A, R, G, B);
            //end color 
            tmp = sr.ReadLine();
            sr.Close();
            if (tmp == null)
            {
                img = null;               
                return;
            }

            try
            {
                tmp = tmp.Substring(5, tmp.Length - 5);
                img = Image.FromFile(tmp);
            }
            catch
            {
                img = null;
            }
            
        }   
        List<List<Color>> CreateM(Bitmap img)
        {
            List<List<Color>> BaseColor = new List<List<Color>>();
            int Loop = 0;
            Random ran = new Random();
            for (int i = 0; i < 100; i++)
            {
                Color getColor = img.GetPixel(ran.Next(img.Width), ran.Next(img.Height));
                bool isBt = false;
                //判断随机获取的颜色是否属于此聚类，如果不属于则将此颜色当作新的聚类点。
                for (int j = 0; j < BaseColor.Count; j++)
                {
                    if (BaseColor[j].Count > 0)
                    {
                        if (isBtBaseColor(BaseColor[j][0], getColor))
                        {
                            isBt = true;
                            break;
                        }
                    }
                }
                if (isBt)
                {
                    i--;
                    Loop++;
                    if (Loop >= 100) break;
                    continue;
                }
                List<Color> tempC = new List<Color>();
                tempC.Add(getColor);
                BaseColor.Add(tempC);
            }
            return BaseColor;
        }
        /// <summary>
        /// 判断颜色是否属于聚类
        /// </summary>
        /// <param name="BaseColor"></param>
        /// <param name="newColor"></param>
        /// <returns></returns>
        bool isBtBaseColor(Color BaseColor, Color newColor)
        {
            if (BaseColor.A <= newColor.A + 10 & BaseColor.A >= newColor.A - 10
                & BaseColor.R <= newColor.R + 10 & BaseColor.R >= newColor.R - 10
                & BaseColor.G <= newColor.G + 10 & BaseColor.G >= newColor.G - 10
                & BaseColor.B <= newColor.B + 10 & BaseColor.B >= newColor.B - 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private Color getMainColor(Image img)
        {
            Bitmap baseImg = (Bitmap)img;
            List<List<Color>> BaseColor = CreateM(baseImg);
            Random ran = new Random();
            //随机取10000个点，并比较此点颜色是否属于聚类，属于则添加到此聚类
            for (int x = 0; x < 10000; x++)
            {
                Color tempColor = baseImg.GetPixel(ran.Next(baseImg.Width), ran.Next(baseImg.Height));
                for (int j = 0; j < BaseColor.Count; j++)
                {
                    if (isBtBaseColor(BaseColor[j][0], tempColor))
                    {
                        BaseColor[j].Add(tempColor);
                        break;
                    }
                }
            }
            List<Color> MainColor = new List<Color>();
            MainColor = BaseColor[0];
            //取所有聚类中颜色最多的。
            for (int i = 1; i < BaseColor.Count; i++)
            {
                if (BaseColor[i].Count > MainColor.Count)
                {
                    MainColor = BaseColor[i];
                }
            }
            //求RGBA平均数并将此作为主色调。
            int R = 0;
            int G = 0;
            int B = 0;
            int A = 0;
            for (int i = 0; i < MainColor.Count; i++)
            {
                R += MainColor[i].R;
                G += MainColor[i].G;
                B += MainColor[i].B;
                A += MainColor[i].A;
            }
            R /= MainColor.Count;
            G /= MainColor.Count;
            B /= MainColor.Count;
            A /= MainColor.Count;

            return Color.FromArgb(A, R, G, B);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button2.BackColor;
            }
            saveSkinINI(2);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button1.BackColor;                
            }
            saveSkinINI(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button3.BackColor;
            }
            saveSkinINI(3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button4.BackColor;
            }
            saveSkinINI(4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button5.BackColor;
            }
            saveSkinINI(5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button6.BackColor;
            }
            saveSkinINI(6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button7.BackColor;
            }
            saveSkinINI(7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button8.BackColor;
            }
            saveSkinINI(8);
        }

        public enum BLURSTYLE
        {
            TOP, LEFT, RIGHT, BOTTOM
        } 
        public static Bitmap SetEdgeBlur(Bitmap bitmap, Color backColor, int blurRange, BLURSTYLE blurStyle)
        {
            Bitmap b = bitmap;
            Graphics g = Graphics.FromImage(b);
            Rectangle rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);

            switch (blurStyle)
            {
                case BLURSTYLE.TOP:
                    {
                        rect = new System.Drawing.Rectangle(0, 0, b.Width, blurRange);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.LEFT:
                    {
                        rect = new System.Drawing.Rectangle(0, 0, blurRange, b.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, backColor, Color.FromArgb(0, backColor), LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.RIGHT:
                    {
                        rect = new System.Drawing.Rectangle(b.Width - blurRange, 0, blurRange, b.Height);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Horizontal))
                        {
                            g.FillRectangle(brush, rect);

                        }
                    }
                    break;
                case BLURSTYLE.BOTTOM:
                    {
                        rect = new System.Drawing.Rectangle(0, b.Height - blurRange, b.Width, blurRange);
                        using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.FromArgb(0, backColor), backColor, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(brush, rect);
                        }
                    }
                    break;
                default:
                    break;
            }
            return b;
        }

        void setListView()
        {
            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.ImageSize = new Size(width, height);//显示大小
            for (int i = 0; i < skinImgs.Length; i++)
            {
                il.Images.Add(skinImgs[i]);
            }
            this.listView1.LargeImageList = il;
            for (int i = 0; i < skinImgs.Length; i++)
                listView1.Items.Add("", i);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = button9.BackColor;
            }
            saveSkinINI(9);
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            int i = listView1.Items.IndexOf(listView1.FocusedItem);
            if (i >= 0)
            {

                try
                {
                    Color C = getMainColor(skinImgs[i]);
                    Bitmap old = (Bitmap)skinImgs[i];

                    Bitmap pic = SetEdgeBlur(old, C, 64, BLURSTYLE.RIGHT);
                    pic = SetEdgeBlur(old, C, 64, BLURSTYLE.BOTTOM);
                    foreach (Form f in Application.OpenForms)
                    {

                        f.BackgroundImage = pic;
                        f.BackColor = C;
                    }
                    saveSkinImgINI(i);
                }
                catch (Exception ee)
                {
                    myMessageBox.show(ee.Message);
                }


            }
        }

        private void skinColorSelectPanel1_Click(object sender, EventArgs e)
        {
            //button10.BackColor = skinColorSelectPanel1.SelectedColor;
            //button10_Click(sender, e);
            
        }

        private void skinColorSelectPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            colorpanel c = new colorpanel();
            c.BackColor = this.BackColor;
            c.ShowDialog();
            
            //foreach (Form f in Application.OpenForms)
            //{
            //    f.BackgroundImage = null;
            //    f.BackColor = button10.BackColor;
            //}
            //Skin.COLOR[9] = button10.BackColor;
            //saveSkinINI(10);
        }

        private void skinColorSelectPanel1_SelectedColorChanged(object sender, EventArgs e)
        {
            //button10.BackColor = skinColorSelectPanel1.SelectedColor;
            //button10_Click(sender, e);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            button1.BackColor = Color.FromArgb(255, 196, 43, 45);
            button2.BackColor = Color.FromArgb(255, 244, 143, 51);
            button3.BackColor = Color.FromArgb(255, 32, 64, 89);
            button4.BackColor = Color.FromArgb(255, 63, 142, 137);
            button5.BackColor = Color.FromArgb(255, 237, 56, 107);
            button6.BackColor = Color.FromArgb(255, 181, 230, 29);
            button7.BackColor = Color.FromArgb(255, 191, 181, 227);
            button8.BackColor = Color.FromArgb(255, 163, 73, 164);
            button9.BackColor = Color.FromArgb(255, 0, 0, 128);

            try
            {
                if (getStateOfWeb())
                {
                    getPicFromBing();
                    getPicFromQQFS("1");
                    getPicFromQQFS("2");
                    getPicFromQQFS("3");
                }                             
                getSkinLib();
            }
            catch(Exception ee)
            {
                myMessageBox.show(ee.Message);
            }
        
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {          
            setListView();
            pictureBox1.Visible = false;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        bool getStateOfWeb()
        {
            //----------------------------------------------------------------//检查网络
            try
            {
                Ping p = new Ping();
                PingReply pr;
                pr = p.Send("119.75.218.45");
                if (pr.Status != IPStatus.Success)
                {
                    return false;
                }
            }
            catch
            {
                // myMessageBox.show(ee.Message);
                return false;
            }
            return true;
        }

        void getPicFromBing()
        {
            if (!Directory.Exists("Skins"))
            {
                Directory.CreateDirectory("Skins");
            }



            string time = DateTime.Now.ToShortDateString().ToString().Replace("/", "");
            string d = "Skins\\" + time +"Bing"+ "\\";

            if (Directory.Exists(d))
               return;
            if (File.Exists(d + "main_r.jpg"))
               return;      
          
            
   
 //-----------------------------------------------------------------保存网页数据
            string url = "http://cn.bing.com";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = Encoding.GetEncoding("utf-8");
            StreamReader sr = new StreamReader(stream, encode);
            string html = null;
            char[] readbuffer = new char[256];
            int n = sr.Read(readbuffer, 0, 256);
            while (n > 0)
            {
                string str = new string(readbuffer, 0, n);
                html += str;
                n = sr.Read(readbuffer, 0, 256);
            }
            StreamWriter streamw = File.CreateText("Skins\\webpic.data");
            streamw.WriteLine(html);
            streamw.Close();
            //---------------------------------------------------------检索图片地址并保存
            StreamReader s = new StreamReader("Skins\\webpic.data");
            string x = s.ReadToEnd();
            s.Close();
            File.Delete("Skins\\webpic.data");
            int a = x.IndexOf("g_img={url:'");
            int b = x.IndexOf(".jpg'");
            string path = x.Substring(a + 12, b - a - 8);

            WebClient client = new WebClient();
            Directory.CreateDirectory(d);       
            client.DownloadFile(path, d + "main_r.jpg");                      

        }

        void getPicFromQQFS(string id)
        {

            string time = DateTime.Now.ToShortDateString().ToString().Replace("/", "");
            string d = "Skins\\" + time + "QQFS" + id + "\\";
            if (Directory.Exists(d))
                return;
            if (File.Exists(d + "main_r.jpg"))
                return; 
            Random r = new Random();
            int x = r.Next(0, 9);
            int y = r.Next(400, 500);
            //y = y * 10 + x;
            //this.Text = x.ToString() + "--" + y.ToString();
            
            string url = "http://imgcache.qq.com/club/item/wallpic/items/" + x + "/" + y + x + "/1366_768_" + y + x + ".jpg";
            System.Net.ServicePointManager.DefaultConnectionLimit = 10;
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                WebClient client = new WebClient();
                if (!Directory.Exists(d))
                    Directory.CreateDirectory(d);
                client.DownloadFile(url, d + MainImageName);  
                response.Close();

            }
            catch(Exception ee)
            {
                //myMessageBox.show(ee.Message);
                getPicFromQQFS(id);
                return;
            }
        }

        private void Skin_SysBottomClick(object sender, CCWin.SkinControl.SysButtonEventArgs e)
        {
            about m = new about();

            m.ShowDialog();
        }




    }
}
