    public static class MyListExtensions {
      public static IEnumerable<T> GetNth(this List<T>, int n) {
        for (int i=0; i<list.Count; i+=n)
          yield return list[i]
      }
    }
