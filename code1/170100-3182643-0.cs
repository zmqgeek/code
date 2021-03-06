    [StructLayout(LayoutKind.Sequential)] 
    public struct WIN32_FILE_ATTRIBUTE_DATA 
    {
        public FileAttributes dwFileAttributes;
        public FILETIME ftCreationTime; 
        public FILETIME ftLastAccessTime; 
        public FILETIME ftLastWriteTime; 
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
    }
    public enum GET_FILEEX_INFO_LEVELS {
        GetFileExInfoStandard,
        GetFileExMaxInfoLevel
    }
    public class MyClass
    {
    [DllImport("kernel32.dll", SetLastError=true, CharSet=CharSet.Unicode)]
    private static extern bool GetFileAttributesEx(string lpFileName,
          GET_FILEEX_INFO_LEVELS fInfoLevelId, out WIN32_FILE_ATTRIBUTE_DATA fileData);
    public long GetFileLength(string path)
    {
         //check path here
         WIN32_FILE_ATTRIBUTE_DATA fileData;
         //append special suffix to allow paths upto 32767
         path = @"\\?\" + path;
         
         if(!GetFileAttributesEx(path, 
                 GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out fileData))
         {
               throw new Win32Exception();
         }
         return (long)(((ulong)fileData.nFileSizeHigh << 32) + 
                        (ulong)fileData.nFileSizeLow); 
    }
    }
