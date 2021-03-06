    var timeToWait = TimeSpan.FromSeconds(10);
    
    var udpClient = new UdpClient( portNumber );
    var asyncResult = udpClient.BeginReceive( null, null );
    asyncResult.AsyncWaitHandle.WaitOne( timeToWait );
    if (asyncResult.IsCompleted)
    {
        try
        {
            IPEndPoint remoteEP = null;
            byte[] receivedData = udpClient.EndReceive( asyncResult, ref remoteEP );
            // EndReceive worked and we have received data and remote endpoint
        }
        catch (Exception ex)
        {
            // EndReceive failed and we ended up here
        }
    } 
    else
    {
        // The operation wasn't completed before the timeout and we're off the hook
    }
