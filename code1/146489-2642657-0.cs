    public sealed class Test
    {
        static void Main()
        {
            // Code not shown.
        }
        public void Run()
        {
            // This code is written by an application developer.
            // Create a channel factory.
            BasicHttpBinding myBinding = new BasicHttpBinding();
	
            EndpointAddress myEndpoint = new EndpointAddress("http://localhost/MathService/Ep1");
            ChannelFactory<IMath> myChannelFactory = new ChannelFactory<IMath>(myBinding, myEndpoint);
            // Create a channel.
            IMath wcfClient1 = myChannelFactory.CreateChannel();
            double s = wcfClient1.Add(3, 39);
            Console.WriteLine(s.ToString());
	        ((IClientChannel)wcfClient1).Close();
            // Create another channel.
            IMath wcfClient2 = myChannelFactory.CreateChannel();
            s = wcfClient2.Add(15, 27);
            Console.WriteLine(s.ToString());
	        ((IClientChannel)wcfClient2).Close();
	        myChannelFactory.Close();
        }
    }