using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms; 
using System.Security.Cryptography;
namespace RayTracing1
{
    public class SelectRun
    {
        [DllImport("RayTracingCore.dll")]
        public static extern void BVHAddTriangles(Trangle trangleArray);
        [DllImport("RayTracingCore.dll")]
        public static extern void BuildBVH();
        [DllImport("RayTracingCore.dll")]
        public static extern void RunTracing(vec3s CenterRay, vec3s Hvec, vec3s Vvec, vec3s CameraLoaction, int wid, int hei, IntPtr ImageData,int numThreads);
         
         
        unsafe public static void CppRun(Form Form1, PictureBox pictureBox1)
        {
            var GetData = OBJReader.ReadFile(@"2.obj");
            Console.WriteLine("Triangles:" + GetData.Count);
             
            foreach (var a in GetData) BVHAddTriangles(a); 
            TimeWatcher.Start();
            BuildBVH();
            TimeWatcher.Stop();
            Console.WriteLine("BVH Build Time:" + TimeWatcher.UsedTime + "ms");


            new Thread(() => {

                int Mag = 1;
                int Wid = pictureBox1.Width * Mag; 
                int Hei = pictureBox1.Height * Mag; 

                float[] ImageData = new float[Wid * Hei * 3];
                var ImagePTR = Marshal.AllocHGlobal(ImageData.Length * sizeof(float));
                int numThreads = 10;
                while (true)
                {
                    Camera.CameraSet(Wid, Hei);

                    TimeWatcher.Start(); 
                    RunTracing(Camera. CenterRay, Camera.Hvec, Camera.Vvec, Camera.Location,  Wid,  Hei, ImagePTR, numThreads);
                    TimeWatcher.Stop();  

                    Marshal.Copy(ImagePTR, ImageData, 0, ImageData.Length);

                    pictureBox1.Invoke(new Action(() =>
                    {
                        Form1.Text = "FPS:" + (1000d / TimeWatcher.UsedTime).ToString("f2") + " RenderTime:" + TimeWatcher.UsedTime + "ms" + " Trangles:" + GetData.Count + " RenderingResolution:" + Wid + "*" + Hei;
                        if (pictureBox1.Image != null) pictureBox1.Image.Dispose();
                        pictureBox1.Image = MapCopy.Copy(ImageData, Wid, Hei);
                    }));
                }
            }).Start();


             

        }
    }
}
