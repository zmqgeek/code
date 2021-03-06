    var result = new Dictionary<int, string[]>();
    
    var pid = Process.GetCurrentProcess().Id;
    
    using (var dataTarget = DataTarget.AttachToProcess(pid, 5000, AttachFlag.Passive))
    {
        ClrInfo runtimeInfo = dataTarget.ClrVersions[0];
        var runtime = runtimeInfo.CreateRuntime();
    
        foreach (var t in runtime.Threads)
        {
            result.Add(
                t.ManagedThreadId,
                t.StackTrace.Select(f =>
                {
                    if (f.Method != null)
                    {
                        return f.Method.Type.Name + "." + f.Method.Name;
                    }
    
                    return null;
                }).ToArray()
            );
        }
    }
    var json = JsonConvert.SerializeObject(result);
    
    zip.AddEntry("_threads.json", json);
