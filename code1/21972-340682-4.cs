    class Test
    {
        public int n;
        public int i { get; set; }
        public void InitAnInt(out int p)
        {
            p = 100;
        }
        public Test()
        {
            InitAnInt(out n); // This is OK
            InitAnInt(out i); // ERROR: A property or indexer may not be passed 
                              // as an out or ref parameter
        }
    }
