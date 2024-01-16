using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing1
{
    public class MapCopy
    {
        public static Bitmap Copy(float[] floatArray,int wid,int hei)
        {  
            Bitmap bitmap = new Bitmap(wid, hei, PixelFormat.Format32bppArgb); 
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, wid, hei), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            Marshal.Copy(floatArray.Select(s=>(byte)s).ToArray(), 0, bitmapData.Scan0, floatArray.Length);
            bitmap.UnlockBits(bitmapData);
            return bitmap;
        }
         
    }
}
