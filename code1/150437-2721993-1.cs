    var firstEnum = aIEnumerable.GetEnumerator();
    var secondEnum = bIEnumerable.GetEnumerator();
        
    while (firstEnum.MoveNext() & secondEnum.MoveNext())
    {
          // Do whatever.
    }
    
    if (firstEnum.MoveNext() | secondEnum.MoveNext())
    {
         Throw new Exception("One Enum is bigger");
    }
    
