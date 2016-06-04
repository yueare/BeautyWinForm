namespace FjuColorForm
{
    partial class colorpanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(colorpanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxmainc = new System.Windows.Forms.PictureBox();
            this.skinColorSelectPanel1 = new CCWin.SkinControl.SkinColorSelectPanel();
            this.skinTrackBar1 = new CCWin.SkinControl.SkinTrackBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxmainc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinTrackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.pictureBoxmainc);
            this.panel1.Controls.Add(this.skinColorSelectPanel1);
            this.panel1.Controls.Add(this.skinTrackBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 169);
            this.panel1.TabIndex = 1;
            // 
            // pictureBoxmainc
            // 
            this.pictureBoxmainc.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxmainc.BackgroundImage")));
            this.pictureBoxmainc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBoxmainc.Location = new System.Drawing.Point(2, 123);
            this.pictureBoxmainc.Name = "pictureBoxmainc";
            this.pictureBoxmainc.Size = new System.Drawing.Size(272, 46);
            this.pictureBoxmainc.TabIndex = 26;
            this.pictureBoxmainc.TabStop = false;
            this.pictureBoxmainc.Click += new System.EventHandler(this.pictureBoxmainc_Click);
            // 
            // skinColorSelectPanel1
            // 
            this.skinColorSelectPanel1.Location = new System.Drawing.Point(1, 0);
            this.skinColorSelectPanel1.Name = "skinColorSelectPanel1";
            this.skinColorSelectPanel1.Size = new System.Drawing.Size(273, 95);
            this.skinColorSelectPanel1.TabIndex = 0;
            this.skinColorSelectPanel1.Text = "skinColorSelectPanel1";
            this.skinColorSelectPanel1.SelectedColorChanged += new System.EventHandler(this.skinColorSelectPanel1_SelectedColorChanged);
            this.skinColorSelectPanel1.Click += new System.EventHandler(this.skinColorSelectPanel1_Click);
            // 
            // skinTrackBar1
            // 
            this.skinTrackBar1.BackColor = System.Drawing.Color.Transparent;
            this.skinTrackBar1.Bar = ((System.Drawing.Image)(resources.GetObject("skinTrackBar1.Bar")));
            this.skinTrackBar1.BarStyle = CCWin.SkinControl.HSLTrackBarStyle.Saturation;
            this.skinTrackBar1.BaseColor = System.Drawing.Color.MediumSeaGreen;
            this.skinTrackBar1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.skinTrackBar1.Location = new System.Drawing.Point(3, 86);
            this.skinTrackBar1.Name = "skinTrackBar1";
            this.skinTrackBar1.Size = new System.Drawing.Size(270, 45);
            this.skinTrackBar1.TabIndex = 27;
            this.skinTrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.skinTrackBar1.Track = null;
            this.skinTrackBar1.Value = 100;
            this.skinTrackBar1.Scroll += new System.EventHandler(this.skinTrackBar1_Scroll);
            // 
            // colorpanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkKhaki;
            this.ClientSize = new System.Drawing.Size(284, 199);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.DimGray;
            this.Name = "colorpanel";
            this.Text = "调色板";
            this.TitleColor = System.Drawing.Color.Black;
            this.Load += new System.EventHandler(this.colorpanel_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxmainc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.skinTrackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinColorSelectPanel skinColorSelectPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxmainc;
        private CCWin.SkinControl.SkinTrackBar skinTrackBar1;
    }
}