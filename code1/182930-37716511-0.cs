    namespace Inspection
    {
        /// <summary>
        /// Static class that adds convenient methods for getting information on the running computers basic hardware and os setup.
        /// </summary>
        public static class ComputerInfo
        {
            /// <summary>
            ///     Returns the Windows major version number for this computer.
            /// </summary>
            public static uint WinMajorVersion
            {
                get
                {
                    dynamic major;
                    // The 'CurrentMajorVersionNumber' string value in the CurrentVersion key is new for Windows 10, 
                    // and will most likely (hopefully) be there for some time before MS decides to change this - again...
                    if (TryGeRegistryKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMajorVersionNumber", out major))
                    {
                        return (uint) major;
                    }
                    // When the 'CurrentMajorVersionNumber' value is not present we fallback to reading the previous key used for this: 'CurrentVersion'
                    dynamic version;
                    if (!TryGeRegistryKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", out version))
                        return 0;
                    var versionParts = ((string) version).Split('.');
                    if (versionParts.Length != 2) return 0;
                    uint majorAsUInt;
                    return uint.TryParse(versionParts[0], out majorAsUInt) ? majorAsUInt : 0;
                }
            }
            /// <summary>
            ///     Returns the Windows minor version number for this computer.
            /// </summary>
            public static uint WinMinorVersion
            {
                get
                {
                    dynamic minor;
                    // The 'CurrentMinorVersionNumber' string value in the CurrentVersion key is new for Windows 10, 
                    // and will most likely (hopefully) be there for some time before MS decides to change this - again...
                    if (TryGeRegistryKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMinorVersionNumber",
                        out minor))
                    {
                        return (uint) minor;
                    }
                    // When the 'CurrentMinorVersionNumber' value is not present we fallback to reading the previous key used for this: 'CurrentVersion'
                    dynamic version;
                    if (!TryGeRegistryKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", out version))
                        return 0;
                    var versionParts = ((string) version).Split('.');
                    if (versionParts.Length != 2) return 0;
                    uint minorAsUInt;
                    return uint.TryParse(versionParts[1], out minorAsUInt) ? minorAsUInt : 0;
                }
            }
            /// <summary>
            ///     Returns whether or not the current computer is a server or not.
            /// </summary>
            public static uint IsServer
            {
                get
                {
                    dynamic installationType;
                    if (TryGeRegistryKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "InstallationType",
                        out installationType))
                    {
                        return (uint) (installationType.Equals("Client") ? 0 : 1);
                    }
                    return 0;
                }
            }
            private static bool TryGeRegistryKey(string path, string key, out dynamic value)
            {
                value = null;
                try
                {
                    var rk = Registry.LocalMachine.OpenSubKey(path);
                    if (rk == null) return false;
                    value = rk.GetValue(key);
                    return value != null;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
