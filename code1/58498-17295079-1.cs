        private bool isRdInstalled() {
            ManagementObjectSearcher p = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
            foreach (ManagementObject program in p.Get()) {
                if (program != null && program.GetPropertyValue("Name") != null && program.GetPropertyValue("Name").ToString().Contains("Microsoft Visual Studio 2012 Remote Debugger")) {
                    return true;
                }
                if (program != null && program.GetPropertyValue("Name") != null) {
                    Trace.WriteLine(program.GetPropertyValue("Name"));
                }
            }
            return false;
        }
