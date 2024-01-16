using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace RayTracing1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            KeyScan.Init();
            Camera.SetCameraLocation();
        }
         
        private void button2_Click(object sender, EventArgs e)
        { 
            SelectRun.CppRun(this, pictureBox1);
        }
         

        bool CameraRoControl = false;
        int Mx = -1, My = -1;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Mx = e.Location.X;
                My = e.Location.Y;
                CameraRoControl = true;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CameraRoControl = false;
            }
        }


        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (CameraRoControl == true)
            {
                Camera.HROtemp += (float)(Mx - e.Location.X) * 0.1f * Camera.Speed;
                Camera.VROtemp += (float)(My - e.Location.Y) * 0.1f * Camera.Speed;
                Mx = e.Location.X;
                My = e.Location.Y;
            }
        }
    }
}
