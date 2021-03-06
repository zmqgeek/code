    using System;
    using System.Threading;
    using System.Runtime.InteropServices;
    public delegate void OnOutputDebugStringHandler(int pid, string text);
    public sealed class DebugMonitor
    {
        private DebugMonitor()
        {
            ;
        }
        #region Win32 API Imports
        [StructLayout(LayoutKind.Sequential)]
        private struct SECURITY_DESCRIPTOR
        {
            public byte revision;
            public byte size;
            public short control;
            public IntPtr owner;
            public IntPtr group;
            public IntPtr sacl;
            public IntPtr dacl;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }
        [Flags]
        private enum PageProtection : uint
        {
            NoAccess = 0x01,
            Readonly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            Guard = 0x100,
            NoCache = 0x200,
            WriteCombine = 0x400,
        }
        private const int WAIT_OBJECT_0 = 0;
        private const uint INFINITE = 0xFFFFFFFF;
        private const int ERROR_ALREADY_EXISTS = 183;
        private const uint SECURITY_DESCRIPTOR_REVISION = 1;
        private const uint SECTION_MAP_READ = 0x0004;
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint
            dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow,
            uint dwNumberOfBytesToMap);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool InitializeSecurityDescriptor(ref SECURITY_DESCRIPTOR sd, uint dwRevision);
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetSecurityDescriptorDacl(ref SECURITY_DESCRIPTOR sd, bool daclPresent, IntPtr dacl, bool daclDefaulted);
        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateEvent(ref SECURITY_ATTRIBUTES sa, bool bManualReset, bool bInitialState, string lpName);
        [DllImport("kernel32.dll")]
        private static extern bool PulseEvent(IntPtr hEvent);
        [DllImport("kernel32.dll")]
        private static extern bool SetEvent(IntPtr hEvent);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile,
            ref SECURITY_ATTRIBUTES lpFileMappingAttributes, PageProtection flProtect, uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow, string lpName);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        private static extern Int32 WaitForSingleObject(IntPtr handle, uint milliseconds);
        #endregion
        public static event OnOutputDebugStringHandler OnOutputDebugString;
        private static IntPtr m_AckEvent = IntPtr.Zero;
        private static IntPtr m_ReadyEvent = IntPtr.Zero;
        private static IntPtr m_SharedFile = IntPtr.Zero;
        private static IntPtr m_SharedMem = IntPtr.Zero;
        private static Thread m_Capturer = null;
        private static object m_SyncRoot = new object();
        private static Mutex m_Mutex = null;
        public static void Start()
        {
            lock (m_SyncRoot)
            {
                if (m_Capturer != null)
                    throw new ApplicationException("This DebugMonitor is already started.");
                if (Environment.OSVersion.ToString().IndexOf("Microsoft") == -1)
                    throw new NotSupportedException("This DebugMonitor is only supported on Microsoft operating systems.");
                bool createdNew = false;
                m_Mutex = new Mutex(false, typeof(DebugMonitor).Namespace, out createdNew);
                if (!createdNew)
                    throw new ApplicationException("There is already an instance of 'DbMon.NET' running.");
                SECURITY_DESCRIPTOR sd = new SECURITY_DESCRIPTOR();
                if (!InitializeSecurityDescriptor(ref sd, SECURITY_DESCRIPTOR_REVISION))
                {
                    throw CreateApplicationException("Failed to initializes the security descriptor.");
                }
                if (!SetSecurityDescriptorDacl(ref sd, true, IntPtr.Zero, false))
                {
                    throw CreateApplicationException("Failed to initializes the security descriptor");
                }
                SECURITY_ATTRIBUTES sa = new SECURITY_ATTRIBUTES();
                m_AckEvent = CreateEvent(ref sa, false, false, "DBWIN_BUFFER_READY");
                if (m_AckEvent == IntPtr.Zero)
                {
                    throw CreateApplicationException("Failed to create event 'DBWIN_BUFFER_READY'");
                }
                m_ReadyEvent = CreateEvent(ref sa, false, false, "DBWIN_DATA_READY");
                if (m_ReadyEvent == IntPtr.Zero)
                {
                    throw CreateApplicationException("Failed to create event 'DBWIN_DATA_READY'");
                }
                m_SharedFile = CreateFileMapping(new IntPtr(-1), ref sa, PageProtection.ReadWrite, 0, 4096, "DBWIN_BUFFER");
                if (m_SharedFile == IntPtr.Zero)
                {
                    throw CreateApplicationException("Failed to create a file mapping to slot 'DBWIN_BUFFER'");
                }
                m_SharedMem = MapViewOfFile(m_SharedFile, SECTION_MAP_READ, 0, 0, 512);
                if (m_SharedMem == IntPtr.Zero)
                {
                    throw CreateApplicationException("Failed to create a mapping view for slot 'DBWIN_BUFFER'");
                }
                m_Capturer = new Thread(new ThreadStart(Capture));
                m_Capturer.Start();
            }
        }
        private static void Capture()
        {
            try
            {
                IntPtr pString = new IntPtr(
                    m_SharedMem.ToInt32() + Marshal.SizeOf(typeof(int))
                );
                while (true)
                {
                    SetEvent(m_AckEvent);
                    int ret = WaitForSingleObject(m_ReadyEvent, INFINITE);
                    if (m_Capturer == null)
                        break;
                    if (ret == WAIT_OBJECT_0)
                    {
                        FireOnOutputDebugString(
                            Marshal.ReadInt32(m_SharedMem),
                                Marshal.PtrToStringAnsi(pString));
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Dispose();
            }
        }
        private static void FireOnOutputDebugString(int pid, string text)
        {
            if (OnOutputDebugString == null)
                return;
            #if !DEBUG
                try
                {
            #endif
                    OnOutputDebugString(pid, text);
            #if !DEBUG
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An 'OnOutputDebugString' handler failed to execute: " + ex.ToString());
                }
            #endif
        }
        private static void Dispose()
        {
            if (m_AckEvent != IntPtr.Zero)
            {
                if (!CloseHandle(m_AckEvent))
                {
                    throw CreateApplicationException("Failed to close handle for 'AckEvent'");
                }
                m_AckEvent = IntPtr.Zero;
            }
            if (m_ReadyEvent != IntPtr.Zero)
            {
                if (!CloseHandle(m_ReadyEvent))
                {
                    throw CreateApplicationException("Failed to close handle for 'ReadyEvent'");
                }
                m_ReadyEvent = IntPtr.Zero;
            }
            if (m_SharedFile != IntPtr.Zero)
            {
                if (!CloseHandle(m_SharedFile))
                {
                    throw CreateApplicationException("Failed to close handle for 'SharedFile'");
                }
                m_SharedFile = IntPtr.Zero;
            }
            if (m_SharedMem != IntPtr.Zero)
            {
                if (!UnmapViewOfFile(m_SharedMem))
                {
                    throw CreateApplicationException("Failed to unmap view for slot 'DBWIN_BUFFER'");
                }
                m_SharedMem = IntPtr.Zero;
            }
            if (m_Mutex != null)
            {
                m_Mutex.Close();
                m_Mutex = null;
            }
        }
        public static void Stop()
        {
            lock (m_SyncRoot)
            {
                if (m_Capturer == null)
                    throw new ObjectDisposedException("DebugMonitor", "This DebugMonitor is not running.");
                m_Capturer = null;
                PulseEvent(m_ReadyEvent);
                while (m_AckEvent != IntPtr.Zero)
                    ;
            }
        }
        private static ApplicationException CreateApplicationException(string text)
        {
            if (text == null || text.Length < 1)
                throw new ArgumentNullException("text", "'text' may not be empty or null.");
            return new ApplicationException(string.Format("{0}. Last Win32 Error was {1}",
                text, Marshal.GetLastWin32Error()));
        }
    }
