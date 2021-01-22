     [Flags]
        public enum ThreadAccess : int
        {
          TERMINATE = (0x0001),
          SUSPEND_RESUME = (0x0002),
          GET_CONTEXT = (0x0008),
          SET_CONTEXT = (0x0010),
          SET_INFORMATION = (0x0020),
          QUERY_INFORMATION = (0x0040),
          SET_THREAD_TOKEN = (0x0080),
          IMPERSONATE = (0x0100),
          DIRECT_IMPERSONATION = (0x0200)
        }
    
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);
    
    
    
        private void SuspendProcess(int PID)
        {
          Process proc = Process.GetProcessById(PID);
    
          if (proc.ProcessName == string.Empty)
            return;
    
          foreach (ProcessThread pT in proc.Threads)
          {
            IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
    
            if (pOpenThread.Equals(null))
            {
              break;
            }
    
            SuspendThread(pOpenThread);
          }
        }
    
        public void ResumeProcess(int PID)
        {
          Process proc = Process.GetProcessById(PID);
    
          if (proc.ProcessName == string.Empty)
            return;
    
          foreach (ProcessThread pT in proc.Threads)
          {
            IntPtr pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)pT.Id);
    
            if (pOpenThread.Equals(null))
            {
              break;
            }
    
            ResumeThread(pOpenThread);
          }
        }
