    class Example {
      private Dictionary<int,string> _map;
      public Dictionary<int,string> Map { get { return _map; } } // Semi-Colon was missing
      public Example() { _map = new Dictionary<int,string>(); }
    }
