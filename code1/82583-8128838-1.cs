    public void PrintNames(List<string> names)
    {
      foreach(string name in names)
      {
        int index; // 1. not initialized
        GetIndex(out index);
        Console.WriteLine(index.ToString() + ". " + name);
      }
    }
    public void GetIndex(out int index)
    {
      index = IndexHelper.GetLatestIndex(); // 2-3. needs to be assigned a value
    }
