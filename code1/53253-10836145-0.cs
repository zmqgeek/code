    public static class HttpExtensions
    {
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var ub = new UriBuilder(uri);
            var queryString = HttpUtility.ParseQueryString(uri.Query);
    
            queryString.Add(name, value);
    
            ub.Query = queryString.ToString();
    
            return ub.Uri;
        }
    }
