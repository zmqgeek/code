    var identity = WindowsIdentity.GetCurrent();
    if (identity == null) throw new InvalidOperationException("Couldn't get the current user identity");
    var principal = new WindowsPrincipal(identity);
    // Check if this user has the Administrator role. If they do, return immediately.
    // If UAC is on, and the process is not elevated, then this will actually return false.
    if (principal.IsInRole(WindowsBuiltInRole.Administrator)) return true;
    // If we're not running in Vista onwards, we don't have to worry about checking for UAC.
    if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 6)
    {
         // Operating system does not support UAC; skipping elevation check.
         return false;
    }
    int tokenInfLength = Marshal.SizeOf(typeof(int));
    IntPtr tokenInformation = Marshal.AllocHGlobal(tokenInfLength);
    try
    {
        var token = identity.Token;
        var result = GetTokenInformation(token, TokenInformationClass.TokenElevationType, tokenInformation, tokenInfLength, out tokenInfLength);
        if (!result)
        {
            var exception = Marshal.GetExceptionForHR( Marshal.GetHRForLastWin32Error() );
            throw new InvalidOperationException("Couldn't get token information", exception);
        }
        var elevationType = (TokenElevationType)Marshal.ReadInt32(tokenInformation);
        
        switch (elevationType)
        {
            case TokenElevationType.TokenElevationTypeDefault:
                // TokenElevationTypeDefault - User is not using a split token, so they cannot elevate.
                return false;
            case TokenElevationType.TokenElevationTypeFull:
                // TokenElevationTypeFull - User has a split token, and the process is running elevated. Assuming they're an administrator.
                return true;
            case TokenElevationType.TokenElevationTypeLimited:
                // TokenElevationTypeLimited - User has a split token, but the process is not running elevated. Assuming they're an administrator.
                return true;
            default:
                // Unknown token elevation type.
                return false;
         }
    }
    finally
    {    
        if (tokenInformation != IntPtr.Zero) Marshal.FreeHGlobal(tokenInformation);
    }
