      Debug.Assert((theData.Length % 3) == 0);  // 'theData' will always be divisible by 3
      for (int i = 0; i < theData.Length; i += 3)
      {
           //grab 3 items at a time and do db insert, 
           // continue until all items are gone..
           string item1 = theData[i+0];
           string item2 = theData[i+1];
           string item3 = theData[i+2];
           // ...
      }
