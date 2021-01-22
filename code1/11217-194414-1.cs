    namespace SomeNameSpace
    {
        public class MyProxy : IWebProxy
        {
            public ICredentials Credentials
            {
                get { return new NetworkCredential("user", "password"); }
                //or get { return new NetworkCredential("user", "password","domain"); }
                set { }
            }
            public Uri GetProxy(Uri destination)
            {
                return new Uri("http://my.proxy:8080");
            }
            public bool IsBypassed(Uri host)
            {
                return false;
            }
        }
    }
