using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FjuColorForm
{
    public partial class colorpanel : FjuColorForm.FrmMain
    {
        public colorpanel()
        {
            InitializeComponent();
        }
        Bitmap myBitmap;
        private void colorpanel_Load(object sender, EventArgs e)
        {
            this.ShowDrawIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.SysButtonItems.Clear();
            skinTrackBar1.BaseColor = this.BackColor;

            try
            {
                myBitmap = new Bitmap(pictureBoxmainc.BackgroundImage);
               
            }
            catch (Exception ee)
            {
                myMessageBox.show(ee.Message);
            }

          
            
        }

        private void skinColorSelectPanel1_Click(object sender, EventArgs e)
        {
            changeColor(skinColorSelectPanel1.SelectedColor);
        }

        private void skinColorSelectPanel1_SelectedColorChanged(object sender, EventArgs e)
        {
            changeColor(skinColorSelectPanel1.SelectedColor);
        }
        private void changeColor(Color  c)
        {
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = c;
            }
            Skin.COLOR[9] =c;
            Skin.saveSkinINI(10);
            this.skinTrackBar1.BaseColor = c;
            double hu, sa, br;
            colorpanel.RGB2HSB(c.R, c.G, c.B, out hu, out sa, out br);
            skinTrackBar1.Value = (int)(sa * 100);
            
        }



        private void pictureBoxmainc_Click(object sender, EventArgs e)
        {
            Point p = pictureBoxmainc.PointToClient(Control.MousePosition);
             Color pixelColor = myBitmap.GetPixel(p.X, p.Y);
            if(pixelColor!=Color.FromArgb(255,255,255,255))
                changeColor(pixelColor);
        }

        private void skinTrackBar1_Scroll(object sender, EventArgs e)
        {
            Color c = skinTrackBar1.BaseColor;

            double hu, sa, br;
            colorpanel.RGB2HSB(c.R, c.G, c.B, out hu, out sa, out br);

            int r, g, b;
            colorpanel.HSB2RGB(hu, sa*skinTrackBar1.Value/100.0f, br, out r, out g, out b);
                      
            Color tmp = Color.FromArgb(255, r, g, b);
            this.BackColor = tmp;
            foreach (Form f in Application.OpenForms)
            {
                f.BackgroundImage = null;
                f.BackColor = tmp;
            }
            Skin.COLOR[9] = tmp;
            Skin.saveSkinINI(10);
        }
        public static void HSB2RGB(double hue, double sat, double bri, out int red, out int green, out int blue)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            if (sat == 0)
            {
                r = g = b = bri;
            }
            else
            {
                // the color wheel consists of 6 sectors. Figure out which sector you're in.
                double sectorPos = hue / 60.0;
                int sectorNumber = (int)(Math.Floor(sectorPos));
                // get the fractional part of the sector
                double fractionalSector = sectorPos - sectorNumber;

                // calculate values for the three axes of the color. 
                double p = bri * (1.0 - sat);
                double q = bri * (1.0 - (sat * fractionalSector));
                double t = bri * (1.0 - (sat * (1 - fractionalSector)));

                // assign the fractional colors to r, g, and b based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = bri;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = bri;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = bri;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = bri;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = bri;
                        break;
                    case 5:
                        r = bri;
                        g = p;
                        b = q;
                        break;
                }
            }
            red = Convert.ToInt32(r * 255);
            green = Convert.ToInt32(g * 255);
            blue = Convert.ToInt32(b * 255); ;
        }
        public static void RGB2HSB(int red, int green, int blue, out double hue, out double sat, out double bri)
        {
            double r = ((double)red / 255.0);
            double g = ((double)green / 255.0);
            double b = ((double)blue / 255.0);

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            hue = 0.0;
            if (max == r && g >= b)
            {
                if (max - min == 0) hue = 0.0;
                else hue = 60 * (g - b) / (max - min);
            }
            else if (max == r && g < b)
            {
                hue = 60 * (g - b) / (max - min) + 360;
            }
            else if (max == g)
            {
                hue = 60 * (b - r) / (max - min) + 120;
            }
            else if (max == b)
            {
                hue = 60 * (r - g) / (max - min) + 240;
            }

            sat = (max == 0) ? 0.0 : (1.0 - ((double)min / (double)max));
            bri = max;
        }

    }
}
