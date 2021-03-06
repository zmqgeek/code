    public void DoStuffInOtherDomain()
    {
        const string assemblyPath = @"[AsmPath]";
        var newDomain = AppDomain.CreateDomain("newDomain");
        var asmLoaderProxy = (ProxyDomain)newDomain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(ProxyDomain).FullName);
        asmLoaderProxy.GetAssembly(assemblyPath);
    }
    class ProxyDomain : MarshalByRefObject
    {
        public void GetAssembly(string AssemblyPath)
        {
            try
            {
                Assembly.LoadFrom(AssemblyPath);
                //If you want to do anything further to that assembly, you need to do it here.
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex);
            }
        }
    }
