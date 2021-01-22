    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private class MEMORYSTATUSEX
    {
    	public uint dwLength;
    	public uint dwMemoryLoad;
    	public ulong ullTotalPhys;
    	public ulong ullAvailPhys;
    	public ulong ullTotalPageFile;
    	public ulong ullAvailPageFile;
    	public ulong ullTotalVirtual;
    	public ulong ullAvailVirtual;
    	public ulong ullAvailExtendedVirtual;
    
    	public MEMORYSTATUSEX()
    	{
    		this.dwLength = (uint)Marshal.SizeOf(typeof(NativeMethods.MEMORYSTATUSEX));
    	}
    }
