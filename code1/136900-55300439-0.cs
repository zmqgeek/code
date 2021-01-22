    DTE2 dte = DesignTimeProjectPath.Processes.GetDTE();
        if (dte != null)
         {
            var solution = dte.Solution;
            if (solution != null)
             {
                 string baseDir = Path.GetDirectoryName(solution.FullName);
                 MessageBox.Show(baseDir);
             }
         }
        
      namespace DesignTimeProjectPath
        {
            /// <summary>
            /// This class takes care of fetching the correct DTE instance for the current process
            /// The current implementation works it way down from Visual Studio version 20 to 10 so
            /// it should be farely version independent
            /// </summary>
            public static class Processes
            {
                [DllImport("ole32.dll")]
                private static extern void CreateBindCtx(int reserved, out IBindCtx ppbc);
                [DllImport("ole32.dll")]
                private static extern void GetRunningObjectTable(int reserved, out IRunningObjectTable prot);
        
                private const int m_MaxVersion = 20;
                private const int m_MinVersion = 10;
        
                internal static DTE2 GetDTE()
                {
                    DTE2 dte = null;
        
                    for (int version = m_MaxVersion; version >= m_MinVersion; version--)
                    {
                        string versionString = string.Format("VisualStudio.DTE.{0}.0", version);
        
                        dte = Processes.GetCurrent(versionString);
        
                        if (dte != null)
                        {
                            return dte;
                        }
                    }
        
                    throw new Exception(string.Format("Can not get DTE object tried versions {0} through {1}", m_MaxVersion, m_MinVersion));
                }
        
                /// <summary>
                /// When multiple instances of Visual Studio are running there also multiple DTE available
                /// The method below takes care of selecting the right DTE for the current process
                /// </summary>
                /// <remarks>
                /// Found this at: http://stackoverflow.com/questions/4724381/get-the-reference-of-the-dte2-object-in-visual-c-sharp-2010/27057854#27057854
                /// </remarks>
                private static DTE2 GetCurrent(string versionString)
                {
                    Process parentProc = GetParent(Process.GetCurrentProcess());
                    int parentProcId = parentProc.Id;
                    string rotEntry = String.Format("!{0}:{1}", versionString, parentProcId);
        
                    IRunningObjectTable rot;
                    GetRunningObjectTable(0, out rot);
        
                    IEnumMoniker enumMoniker;
                    rot.EnumRunning(out enumMoniker);
                    enumMoniker.Reset();
        
                    IntPtr fetched = IntPtr.Zero;
                    IMoniker[] moniker = new IMoniker[1];
        
                    while (enumMoniker.Next(1, moniker, fetched) == 0)
                    {
                        IBindCtx bindCtx;
                        CreateBindCtx(0, out bindCtx);
                        string displayName;
                        moniker[0].GetDisplayName(bindCtx, null, out displayName);
        
                        if (displayName == rotEntry)
                        {
                            object comObject;
        
                            rot.GetObject(moniker[0], out comObject);
        
                           return (EnvDTE80.DTE2)comObject;
                        }
                    }
        
                    return null;
                }
        
                private static Process GetParent(Process process)
                {
                    var processName = process.ProcessName;
                    var nbrOfProcessWithThisName = Process.GetProcessesByName(processName).Length;
                    for (var index = 0; index < nbrOfProcessWithThisName; index++)
                    {
                        var processIndexdName = index == 0 ? processName : processName + "#" + index;
                        var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                        if ((int)processId.NextValue() == process.Id)
                        {
                            var parentId = new PerformanceCounter("Process", "Creating Process ID", processIndexdName);
                            return Process.GetProcessById((int)parentId.NextValue());
                        }
                    }
                    return null;
                }
            }
        }
        
   
