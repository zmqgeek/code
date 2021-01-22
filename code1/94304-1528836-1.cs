    public static class IEnumerableExtensions {
      public static string BuildString<T>(this IEnumerable<T> self, string delim) {
        return string.Join(",", list.Select(x => x.ToString()).ToArray())        
      }
    }
