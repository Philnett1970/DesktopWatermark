using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using DesktopWatermark.Properties;

namespace DesktopWatermark
{
    public partial class Form1 : Form
    {
        private PictureBox box = new PictureBox();
        AboutBox1 abt = new AboutBox1();
        public Form1()
        {
            InitializeComponent();
            this.checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
         //   this.Paint += new PaintEventHandler(DrawString);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.Fixed3D)
            {
                  this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
            else if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notify1.Visible = true;
           
            checkBox1.Checked = Settings.Default.chkBox;
            lblText.Text = Settings.Default.Tstring + "                                                   ";
           // pictureBox1.Image = Image.FromFile(Settings.Default.Img);
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height -30);
            textBox1.Text = Settings.Default.Img;
            textBox2.Text = Settings.Default.Tstring;
            IfBlank();
        }

        private void IfBlank()
        {
            if (Settings.Default.Img == "")
            {
                pictureBox1.Image = Properties.Resources.pnett;
            }
            else
            {
                pictureBox1.Image = Image.FromFile(Settings.Default.Img);
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.Fixed3D)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
            else if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            }
        }
               

        private void timer1_Tick(object sender, EventArgs e)
        {
             lblText.Text = lblText.Text.Substring(1, lblText.Text.Length - 1) + lblText.Text.Substring(0, 1);
             Invalidate();
        }



        //remove image flicker
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x2000000;
                return cp;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notify1.Visible = false;
        }

        private void notify1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                contextMenuStrip1.Show(this, this.PointToClient(Cursor.Position));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                lblText.Text = Settings.Default.Tstring;
                timer1.Start();
                this.Height = 108;
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - 30);
                lblText.Text = Settings.Default.Tstring + "                                                   ";
                panel1.Visible = false;              
            }
            else if (checkBox1.Checked == false)
            {
                timer1.Stop();
                lblText.Text = "";
                this.Height = 78;
                this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height -30);
                panel1.Visible = false;
            }
        }

        private void selectImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Height = 108;
            panel1.Visible = true;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height - 30);
            this.Invalidate();
            
        }

        private void changeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Height = 108;
            panel1.Visible = true;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height -30);
            this.Invalidate();
            
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            btnPanelClose.ForeColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            btnPanelClose.ForeColor = Color.Blue;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result== System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName.ToString());

            }

        }

        private void btnPanelClose_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            lblText.Text = " ";
            Settings.Default.Tstring = textBox2.Text;
            Settings.Default.Img = textBox1.Text;
            Settings.Default.Save();
            panel1.Visible = false;
            lblText.Invalidate();
            lblText.Text = Settings.Default.Tstring + "                                                   ";
            timer1.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abt.ShowDialog();
        }









    }
}
