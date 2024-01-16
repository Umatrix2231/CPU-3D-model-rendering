using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing1
{ 
    [StructLayout(LayoutKind.Sequential)]
    public class Trangle
    {
        public vec3s p1, p2, p3;
    }  
}
