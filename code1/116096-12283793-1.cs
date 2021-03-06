    private void copyDirectory(string strSource, string strDestination)
    {
        if (!Directory.Exists(strDestination))
        {
            Directory.CreateDirectory(strDestination);
        }
        DirectoryInfo dirInfo = new DirectoryInfo(strSource);
        FileInfo[] files = dirInfo.GetFiles();
        foreach(FileInfo tempfile in files )
        {
            tempfile.CopyTo(Path.Combine(strDestination,tempfile.Name));
        }
        DirectoryInfo[] directories = dirInfo.GetDirectories();
        foreach(DirectoryInfo tempdir in directories)
        {
            copyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
        }
        
    }
