    var r = new List<long>();
    var s = Stopwatch.StartNew();
    s.Restart();
    string root1 = Application.StartupPath;
    r.Add(s.ElapsedTicks);
    s.Restart();
    string root2 = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
    r.Add(s.ElapsedTicks);
    s.Restart();
    string root3 = Path.GetDirectoryName(new FileInfo(Assembly.GetExecutingAssembly().Location).FullName);
    r.Add(s.ElapsedTicks);
    s.Restart();
    string root4 = AppDomain.CurrentDomain.BaseDirectory;
    r.Add(s.ElapsedTicks);
    s.Restart();
    string root5 = Path.GetDirectoryName(Assembly.GetAssembly( typeof( Form1 ) ).Location);
    r.Add(s.ElapsedTicks);
