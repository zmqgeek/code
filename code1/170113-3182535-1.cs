    public static class StockErrors
    {
        private static readonly Error a;
        private static readonly Error b;
        private static readonly Error c;
    
        public static Error A { get { return a; } }
        public static Error A { get { return b; } }
        public static Error A { get { return c; } }
    
        static StockErrors()
        {
            a = new Error(...);
            b = new Error(...);
            c = new Error(...);
        }
    }
