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
    public partial class about : FjuColorForm.FrmMain
    {
        public about()
        {
            InitializeComponent();
        }

        private void about_Load(object sender, EventArgs e)
        {
            
            SysButtonItems.Clear();
            BackgroundImage = null;
            CanResize = false;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowDrawIcon = false;
            Text = "关于";
        }
    }
}
