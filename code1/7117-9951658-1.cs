    public static class MultiplatformDllLoader
    {
        private static bool _isEnabled;
        public static bool Enable
        {
            get { return _isEnabled; }
            set
            {
                lock (typeof (MultiplatformDllLoader))
                {
                    if (_isEnabled != value)
                    {
                        if (value)
                            AppDomain.CurrentDomain.AssemblyResolve += Resolver;
                        else
                            AppDomain.CurrentDomain.AssemblyResolve -= Resolver;
                        _isEnabled = value;
                    }
                }
            }
        }
        /// Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            string assemblyName = args.Name.Split(new[] {','}, 2)[0] + ".dll";
            string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   assemblyName);
            return File.Exists(archSpecificPath)
                       ? Assembly.LoadFile(archSpecificPath)
                       : null;
        }
    }
