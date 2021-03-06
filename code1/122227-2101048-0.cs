    class Program
    {
        static void Main(string[] args)
        {
            System.Type activator = typeof(RemoteProxy);
            AppDomain domain = 
                AppDomain.CreateDomain(
                    "friendly name", null,
                    new AppDomainSetup()
                    {
                        ApplicationName = "application name"
                    });
            RemoteProxy proxy = 
                domain.CreateInstanceAndUnwrap(
                    Assembly.GetAssembly(activator).FullName,
                    activator.ToString()) as RemoteProxy;
            proxy.DoSomething();
            AppDomain.Unload(domain);
        }
    }
