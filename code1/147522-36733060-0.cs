    public static bool IsConnected(this Socket client)
	{
			// This is how you can determine whether a socket is still connected.
			bool blockingState = client.Blocking;
			try
			{
				byte[] tmp = new byte[1];
				client.Blocking = false;
				client.Send(tmp, 0, 0);
				return true;
			}
			catch (SocketException e)
			{
				// 10035 == WSAEWOULDBLOCK
				if (e.NativeErrorCode.Equals(10035))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			finally
			{
				client.Blocking = blockingState;
			}
		}
