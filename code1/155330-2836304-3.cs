    public static byte[] TestCode()
    {
      using(MemoryStream m = new MemoryStream())
      {
          // ...
          // ...
          // whole bunch of stuff in between
          // ...
          // ...
    
         return m.ToArray();
      }
    }
