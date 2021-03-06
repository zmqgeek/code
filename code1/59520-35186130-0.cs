    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    
    namespace HQ.Util.Unmanaged
    {
    	public class ScreenSaverHelper
    	{
    		[DllImport("User32.dll")]
    		public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    
    		[DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
    		private static extern IntPtr GetDesktopWindow();
    
    		// Signatures for unmanaged calls
    		[DllImport("user32.dll", CharSet = CharSet.Auto)]
    		private static extern bool SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int flags);
    
    		// Constants
    		private const int SPI_GETSCREENSAVERACTIVE = 16;
    		private const int SPI_SETSCREENSAVERACTIVE = 17;
    		private const int SPI_GETSCREENSAVERTIMEOUT = 14;
    		private const int SPI_SETSCREENSAVERTIMEOUT = 15;
    		private const int SPI_GETSCREENSAVERRUNNING = 114;
    		private const int SPIF_SENDWININICHANGE = 2;
    
    		private const uint DESKTOP_WRITEOBJECTS = 0x0080;
    		private const uint DESKTOP_READOBJECTS = 0x0001;
    		private const int WM_CLOSE = 16;
    
    		public const uint WM_SYSCOMMAND = 0x112;
    		public const uint SC_SCREENSAVE = 0xF140;
    		public enum SpecialHandles
    		{
    			HWND_DESKTOP = 0x0,
    			HWND_BROADCAST = 0xFFFF
    		}
    		public static void TurnScreenSaver(bool turnOn = true)
    		{
    			// Does not work on Windows 7
    			// int nullVar = 0;
    			// SystemParametersInfo(SPI_SETSCREENSAVERACTIVE, 1, ref nullVar, SPIF_SENDWININICHANGE);
    
    			// Does not work on Windows 7, can't broadcast. Also not needed.
    			// SendMessage(new IntPtr((int) SpecialHandles.HWND_BROADCAST), WM_SYSCOMMAND, SC_SCREENSAVE, 0);
    
    			SendMessage(GetDesktopWindow(), WM_SYSCOMMAND, (IntPtr)SC_SCREENSAVE, (IntPtr)0);
    		}
    	}
    }
