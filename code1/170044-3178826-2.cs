    private object padLock = new object();  // 1-to-1 with _ConditionalOrderList
    
    if (Monitor.TryEnter(padLock))
    {
       try 
       {
          // cancel other orders
        
          return true;
       } 
       finally 
       {
           Monitor.Exit(padLock);
       }
    }
    else
    {
       return false;
    }
