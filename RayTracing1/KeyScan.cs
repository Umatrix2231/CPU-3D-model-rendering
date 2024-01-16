﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracing1
{
    public class KeyScan
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);



        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;  
        public static void Init()
        { 
            new Thread(() => {
                _hookID = SetHook(_proc);
                Application.Run();
                UnhookWindowsHookEx(_hookID);
            }).Start();
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        public static void Stop()
        {
            Application.Exit();
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_KEYUP))
            {
                int vkCode = Marshal.ReadInt32(lParam); 
                int keyState = wParam.ToInt32(); 
                if (vkCode == 87 && keyState == WM_KEYDOWN) Camera.W = true;
                if (vkCode == 83 && keyState == WM_KEYDOWN) Camera.S = true;
                if (vkCode == 65 && keyState == WM_KEYDOWN) Camera.A = true;
                if (vkCode == 68 && keyState == WM_KEYDOWN) Camera.D = true;
                if (vkCode == 87 && keyState != WM_KEYDOWN) Camera.W = false;
                if (vkCode == 83 && keyState != WM_KEYDOWN) Camera.S = false;
                if (vkCode == 65 && keyState != WM_KEYDOWN) Camera.A = false;
                if (vkCode == 68 && keyState != WM_KEYDOWN) Camera.D = false;


                if (vkCode == 69 && keyState == WM_KEYDOWN) Camera.Up = true;
                if (vkCode == 69 && keyState != WM_KEYDOWN) Camera.Up = false;
                if (vkCode == 81 && keyState == WM_KEYDOWN) Camera.Down = true;
                if (vkCode == 81 && keyState != WM_KEYDOWN) Camera.Down = false;
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn,
            IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
