                proxy.Abort();
            }
            else
            {
                try
                {
                    proxy.Close();
                }
                catch
                {
                    proxy.Abort();
                }
            }
