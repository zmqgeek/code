    // foreach (var item in itemsToBeLast)
    for (int i = 0; i < itemsToBeLast.Count; i++)
    {
        var matchingItem = itemsToBeLast.FirstOrDefault(item => item.Detach);
       if (matchingItem != null)
       {
          itemsToBeLast.Remove(matchingItem);
          continue;
       }
       allItems.Add(itemsToBeLast[i]);// (attachDetachItem);
    }
