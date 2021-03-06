    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    
    namespace IconExtractor
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
    
        public enum FolderType
        {
            Closed,
            Open
        }
    
        public enum IconSize
        {
            Large,
            Small
        }
    
        public partial class Form1 : Form
        {
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint uFlags);
    
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool DestroyIcon(IntPtr hIcon);
    
            public const uint SHGFI_ICON = 0x000000100; 
            public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010; 
            public const uint SHGFI_OPENICON = 0x000000002; 
            public const uint SHGFI_SMALLICON = 0x000000001; 
            public const uint SHGFI_LARGEICON = 0x000000000; 
            public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
    
            public static Icon GetFolderIcon(IconSize size, FolderType folderType)
            {    
                // Need to add size check, although errors generated at present!    
                uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;    
                
                if (FolderType.Open == folderType)    
                {        
                    flags += SHGFI_OPENICON;    
                }    
                if (IconSize.Small == size)    
                {       flags += SHGFI_SMALLICON;    
                }     
                else     
                {       
                    flags += SHGFI_LARGEICON;    
                }    
                // Get the folder icon    
                var shfi = new SHFILEINFO();    
    
                var res = SHGetFileInfo(@"C:\Windows",                             
                    FILE_ATTRIBUTE_DIRECTORY,                             
                    out shfi,                             
                    (uint) Marshal.SizeOf(shfi),                             
                    flags );
    
                if (res == IntPtr.Zero)
                    throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                
                // Load the icon from an HICON handle  
                Icon.FromHandle(shfi.hIcon);    
                  
                // Now clone the icon, so that it can be successfully stored in an ImageList
                var icon = (Icon)Icon.FromHandle(shfi.hIcon).Clone();    
                
                DestroyIcon( shfi.hIcon );        // Cleanup    
                
                return icon;}
    
            public Form1()
            {
                InitializeComponent();
            }
    
            private void Form1_Load(object sender, EventArgs e)
            {
                try
                {
    
                    Icon icon = GetFolderIcon(IconSize.Large, FolderType.Open);
                    pictureBox1.Image = icon.ToBitmap();
                    // Note: The image actually should be disposed somewhere
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
