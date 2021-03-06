           var process = System.Diagnostics.Process.GetCurrentProcess();
           var parent = process.Parent();
           var procIsService = process?.IsService;
           var parentIsService = parent?.IsService;
           ...
    public static class Extensions
    {
        private static string FindIndexedProcessName(int pid)
        {
            var processName = Process.GetProcessById(pid).ProcessName;
            var processesByName = Process.GetProcessesByName(processName);
            string processIndexdName = null;
            for (var index = 0; index < processesByName.Length; index++)
            {
                processIndexdName = index == 0 ? processName : processName + "#" + index;
                var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                if ((int)processId.NextValue() == pid)
                {
                    return processIndexdName;
                }
            }
            return processIndexdName;
        }
        private static Process FindPidFromIndexedProcessName(string indexedProcessName)
        {
            var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
            return Process.GetProcessById((int)parentId.NextValue());
        }
        public static Process Parent(this Process process)
        {
            return FindPidFromIndexedProcessName(FindIndexedProcessName(process.Id));
        }
        public static bool IsService(this Process process)
        {
            using (ManagementObjectSearcher Searcher = new ManagementObjectSearcher(
            "SELECT * FROM Win32_Service WHERE ProcessId =" + "\"" + process.Id + "\""))
            {
                foreach (ManagementObject service in Searcher.Get())
                    return true;
            }
            return false;
        }
    }
