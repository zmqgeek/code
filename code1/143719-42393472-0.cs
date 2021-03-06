    using System;
    using System.Runtime.InteropServices;
    
    namespace MyDummyNamespace
    {
       class MyProgram
       {
          private static int Main(string[] args)
          {
             // your program code here
             // ...
             
             NativeMethods.MonitorOff();
             System.Threading.Thread.Sleep(5000);
             NativeMethods.MonitorOn();
             
             return 0;
          }
          
          private static class NativeMethods
          {
             internal static void MonitorOn()
             {
                SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_FLAGS.MONITOR_ON);
             }
             
             internal static void MonitorOff()
             {
                SendMessage(HWND_BROADCAST, WM_SYSCOMMAND, SC_MONITORPOWER, MONITOR_FLAGS.MONITOR_OFF);
             }
             
             [DllImport("user32.dll", CharSet = CharSet.Auto)]
             private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, MONITOR_FLAGS lParam);
             
             [FlagsAttribute]
             private enum MONITOR_FLAGS : Int32
             {
                MONITOR_ON = -1,
                MONITOR_OFF = 2,
                MONITOR_STANBY = 1
             }
             
             private static IntPtr HWND_BROADCAST = new IntPtr(0xffff);
             private static UInt32 WM_SYSCOMMAND = 0x0112;
             private static IntPtr SC_MONITORPOWER = new IntPtr(0xF170);
          }
       }
    }
