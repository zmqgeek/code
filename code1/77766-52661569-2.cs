    /// <sumary>Check if file exists with timeout</sumary>
    /// <param name="fileInfo">source</param>
    /// <param name="millisecondsTimeout">The number of milliseconds to wait,
    ///  or <see cref="System.Threading.Timeout.Infinite"/> (-1) to wait indefinitely.</param>
    /// <returns>Gets a value indicating whether a file exists.</returns>
    public static bool Exists(this FileInfo fileInfo, int millisecondsTimeout)
    {
        var task = new Task<bool>(() => fileInfo.Exists);
        task.Start();
        return task.Wait(millisecondsTimeout) && task.Result;
    }
