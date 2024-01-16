using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RayTracing1
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe public class vec3s
    {
        public float x = 0, y = 0, z = 0, w = 255;
        public float c1 = 1, c2 = 1, c3 = 1;
        public vec3s()
        {

        }

        public vec3s(double _x, double _y, double _z, double _w)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
            w = (float)_w;
        }
        public vec3s(float[] _v)
        {
            for (int i = 0; i < _v.Length; i++)
            {
                if (i == 0) x = _v[0];
                if (i == 1) y = _v[1];
                if (i == 2) z = _v[2];
                if (i == 3) w = _v[3];
            }
        }
        public vec3s(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public vec3s(double _x, double _y, double _z)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
        }
        public float dot( vec3s b)
        {
            vec3s a = this;
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        public vec3s cross( vec3s b)
        {
            vec3s a = this;
            return new vec3s(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }
        public vec3s GetColor()
        {
            return new vec3s(c1,c2, c3);
        }
        public vec3s Norm()
        { 
            return this / this.Len();
        }
        public float Len()
        {
            float dist = (float)Math.Sqrt(x * x + y * y + z * z);
            return dist;
        }
        public float Max()
        {
            return Math.Max(Math.Max(x, y), z);
        }
        public float Min()
        {
            return Math.Min(Math.Min(x, y), z);
        }
        public vec3s Reverse()
        {
            return new vec3s(z, y, x);
        }
        public float[] ToFloat()
        {
            return new float[] { x, y, z };
        }
        public vec3s Clone()
        {
            return new vec3s(x, y, z, w) { c1 = c1, c2 = c2, c3 = c3 };
        }
        public bool IsZero()
        {
            return x == 0 && y == 0 && z == 0;
        }
        public vec3s Abs()
        {
            return new vec3s(Math.Abs(x), Math.Abs(y), Math.Abs(z));
        }
        public float this[int index]
        {
            get
            { 
                if (index >= 0 && index < 4)
                {
                    return index == 0 ? x : index == 1 ? y : index == 2 ? z : w;
                }
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }
            set
            {
                if (index >= 0 && index < 4)
                {
                    if (index == 0) x = value;
                    else if (index == 1) y = value;
                    else if (index == 2) z = value;
                    else if (index == 3) w = value;
                }
                else
                    throw new IndexOutOfRangeException("Index is out of range");
            }
        }
        public static bool operator >=(vec3s a, float b)
        {
            return a.x >= b && a.y >= b && a.z >= b;
        }
        public static bool operator <=(vec3s a, float b)
        {
            return a.x <= b && a.y <= b && a.z <= b;
        }
        public static bool operator >=(vec3s a, vec3s b)
        {
            return a.x >= b.x && a.y >= b.y && a.z >= b.z;
        }
        public static bool operator <=(vec3s a, vec3s b)
        {
            return a.x <= b.x && a.y <= b.y && a.z <= b.z;
        }
        public static bool operator >(vec3s a, vec3s b)
        {
            return a.x > b.x && a.y > b.y && a.z > b.z;
        }
        public static bool operator <(vec3s a, vec3s b)
        {
            return a.x < b.x && a.y < b.y && a.z < b.z;
        }
        public static vec3s operator +(vec3s a, vec3s b)
        {
            return new vec3s(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static vec3s operator -(vec3s a, vec3s b)
        {
            return new vec3s(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static vec3s operator *(vec3s a, vec3s b)
        {
            return new vec3s(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static vec3s operator /(vec3s a, vec3s b)
        {
            return new vec3s(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static vec3s operator +(vec3s a, float b)
        {
            return new vec3s(a.x + b, a.y + b, a.z + b);
        }
        public static vec3s operator -(vec3s a, float b)
        {
            return new vec3s(a.x - b, a.y - b, a.z - b);
        }
        public static vec3s operator *(vec3s a, float b)
        {
            return new vec3s(a.x * b, a.y * b, a.z * b);
        }
        public static vec3s operator /(vec3s a, float b)
        {
            return new vec3s(a.x / b, a.y / b, a.z / b);
        }
        public static vec3s operator +(float b, vec3s a)
        {
            return new vec3s(b + a.x, b + a.y, b + a.z);
        }
        public static vec3s operator -(float b, vec3s a)
        {
            return new vec3s(b - a.x, b - a.y, b - a.z);
        }
        public static vec3s operator *(float b, vec3s a)
        {
            return new vec3s(b * a.x, b * a.y, b * a.z);
        }
        public static vec3s operator /(float b, vec3s a)
        {
            return new vec3s(b / a.x, b / a.y, b / a.z);
        }
        public static bool operator ==(vec3s b, vec3s a)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(vec3s b, vec3s a)
        {
            return !(a.x == b.x && a.y == b.y && a.z == b.z);
        }
    } 
    public static class Ex
    { 
        public static float Len(this vec3s a, vec3s b)
        {
            float dist = (float)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2) + Math.Pow(a.z - b.z, 2));
            return dist;
        } 
    }
}
