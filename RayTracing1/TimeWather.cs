using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing1
{
    public class TimeWatcher
    {
        static Stopwatch GlobalTime = new Stopwatch();
        static Stopwatch SleepWatch = new Stopwatch();
        static Stopwatch TimeCheck = new Stopwatch();
        static long delayMilliseconds = 0;
        static long LastMS = 0;
        public static int UsedTime = 0;
        public static void GlobalTimeStart() { GlobalTime.Start(); }
        public static long GlobalTimeGet() => GlobalTime.ElapsedMilliseconds;
        public static void Sleep(int ms)
        {
            delayMilliseconds += ms;
            SleepWatch.Start();
            while (SleepWatch.ElapsedMilliseconds < delayMilliseconds)
            {
            }
            SleepWatch.Stop();
        }
        public static void Start()
        {
            TimeCheck.Start();
        }
        public static void Stop()
        {
            TimeCheck.Stop();
            UsedTime = (int)(TimeCheck.ElapsedMilliseconds - LastMS);
            LastMS = TimeCheck.ElapsedMilliseconds;
        }
    }
}
