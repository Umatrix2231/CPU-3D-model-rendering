using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RayTracing1
{
    public class Camera
    {
        public static bool W, A, S, D,Up,Down;
        public static float Speed = 3;
        public static float H_half_fov = 45;
        static float H_half_len = 0, V_half_len = 0;
        public static vec3s CenterRay, Hvec, Vvec;
        static float Half_Wid, Half_Hei;
        public static vec3s Location = new vec3s(300, 0, 40);
        static vec3s LocationTemp = new vec3s(300, 0, 40);
        static float HRO = 180, VRO = 0;
        public static  float HROtemp = 180, VROtemp = 0;
        public static void SetCameraLocation()
        {
            new Thread(() => {
                while (true)
                {
                    if (W)
                    {
                        LocationTemp += CenterRay * Speed;
                    }
                    if (S)
                    {
                        LocationTemp -= CenterRay * Speed;
                    }
                    if (A)
                    {
                        LocationTemp -= CenterRay.cross(new vec3s(0, 0, 1)) * Speed;
                    }
                    if (D)
                    {
                        LocationTemp += CenterRay.cross(new vec3s(0, 0, 1)) * Speed;
                    }
                    if (Up)
                    {
                        LocationTemp += new vec3s(0, 0, 1) * Speed;
                    }
                    if (Down)
                    {
                        LocationTemp += new vec3s(0, 0, -1) * Speed;
                    }
                    Thread.Sleep(30);
                }
            }).Start();
        }
        public static void CameraSet(float Wid, float Hei)
        {
            HRO = HROtemp;
            VRO = VROtemp;
            Location = LocationTemp.Clone();
            float HRO_T = HRO;
            float VRO_T = VRO;
            H_half_len = (float)Math.Sin(H_half_fov / 180d * Math.PI);
            V_half_len = H_half_len * Hei / Wid;

            Half_Wid = Wid / 2f;
            Half_Hei = Hei / 2f;

            float CenterAxisLen = (float)Math.Sqrt(1d - H_half_len * H_half_len); 
            var dx = Math.Cos(HRO_T * Math.PI / 180d);
            var dy = Math.Sin(HRO_T * Math.PI / 180d);
            var dz = Math.Sin(VRO_T * Math.PI / 180d);
            var v = Math.Cos(VRO_T * Math.PI / 180d);
            CenterRay = new vec3s(dx * v, dy * v, dz) * CenterAxisLen;
            Hvec = CenterRay.cross(new vec3s(0, 0, 1)).Norm() * H_half_len / Half_Wid;
            Vvec = CenterRay.cross(Hvec).Norm() * V_half_len / Half_Hei; 
        }
        public static vec3s RayDir(int Px,int Py)
        {
            return (CenterRay + Hvec * (Px - Half_Wid) + Vvec * (Py - Half_Hei)).Norm(); 
        }
    }
}
