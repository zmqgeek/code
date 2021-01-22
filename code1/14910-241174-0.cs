    /// <summary>
    /// Indicates if we're running on battery power.
    /// If we are, then disable CPU wasting things like animations, background operations, network, I/O, etc
    /// </summary>
    public static Boolean IsRunningOnBattery
    {
    	get
    	{
    		PowerLineStatus pls = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus;
    		if (pls == PowerLineStatus.Offline)
    		{
    			//Offline means running on battery
    			return true;
    		}
    		else
    		{
    			return false;
    		}
    	}
    }
