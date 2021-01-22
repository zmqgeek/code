    public class ProcessRunner
    {
        #region "API imports"
        private const int DBG_CONTINUE = 0x00010002;
        private const int DBG_EXCEPTION_NOT_HANDLED = unchecked((int) 0x80010001);
        private enum DebugEventType : int
        {
            CREATE_PROCESS_DEBUG_EVENT = 3,
            //Reports a create-process debugging event. The value of u.CreateProcessInfo specifies a CREATE_PROCESS_DEBUG_INFO structure.
            CREATE_THREAD_DEBUG_EVENT = 2,
            //Reports a create-thread debugging event. The value of u.CreateThread specifies a CREATE_THREAD_DEBUG_INFO structure.
            EXCEPTION_DEBUG_EVENT = 1,
            //Reports an exception debugging event. The value of u.Exception specifies an EXCEPTION_DEBUG_INFO structure.
            EXIT_PROCESS_DEBUG_EVENT = 5,
            //Reports an exit-process debugging event. The value of u.ExitProcess specifies an EXIT_PROCESS_DEBUG_INFO structure.
            EXIT_THREAD_DEBUG_EVENT = 4,
            //Reports an exit-thread debugging event. The value of u.ExitThread specifies an EXIT_THREAD_DEBUG_INFO structure.
            LOAD_DLL_DEBUG_EVENT = 6,
            //Reports a load-dynamic-link-library (DLL) debugging event. The value of u.LoadDll specifies a LOAD_DLL_DEBUG_INFO structure.
            OUTPUT_DEBUG_STRING_EVENT = 8,
            //Reports an output-debugging-string debugging event. The value of u.DebugString specifies an OUTPUT_DEBUG_STRING_INFO structure.
            RIP_EVENT = 9,
            //Reports a RIP-debugging event (system debugging error). The value of u.RipInfo specifies a RIP_INFO structure.
            UNLOAD_DLL_DEBUG_EVENT = 7,
            //Reports an unload-DLL debugging event. The value of u.UnloadDll specifies an UNLOAD_DLL_DEBUG_INFO structure.
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct DEBUG_EVENT
        {
            [MarshalAs(UnmanagedType.I4)] public DebugEventType dwDebugEventCode;
            public int dwProcessId;
            public int dwThreadId;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)] public byte[] bytes;
        }
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool DebugActiveProcess(int dwProcessId);
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool WaitForDebugEvent([Out] out DEBUG_EVENT lpDebugEvent, int dwMilliseconds);
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern bool ContinueDebugEvent(int dwProcessId, int dwThreadId, int dwContinueStatus);
        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool IsDebuggerPresent();
        #endregion
        public Process ChildProcess { get; set; }
        public bool StartProcess(string fileName)
        {
            var processStartInfo = new ProcessStartInfo(fileName)
            {
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Normal,
                ErrorDialog = false
            };
            this.ChildProcess = Process.Start(processStartInfo);
            if (ChildProcess == null)
                return false;
            new Thread(NullDebugger) {IsBackground = true}.Start(ChildProcess.Id);
            return true;
        }
        private void NullDebugger(object arg)
        {
            // Attach to the process we provided the thread as an argument
            if (DebugActiveProcess((int) arg))
            {
                var debugEvent = new DEBUG_EVENT {bytes = new byte[1024]};
                while (!this.ChildProcess.HasExited)
                {
                    if (WaitForDebugEvent(out debugEvent, 1000))
                    {
                        // return DBG_CONTINUE for all events but the exception type
                        var continueFlag = DBG_CONTINUE;
                        if (debugEvent.dwDebugEventCode == DebugEventType.EXCEPTION_DEBUG_EVENT)
                            continueFlag = DBG_EXCEPTION_NOT_HANDLED;
                        ContinueDebugEvent(debugEvent.dwProcessId, debugEvent.dwThreadId, continueFlag);
                    }
                }
            }
            else
            {
                //we were not able to attach the debugger
                //do the processes have the same bitness?
                //throw ApplicationException("Unable to attach debugger") // Kill child? // Send Event? // Ignore?
            }
        }
    }
