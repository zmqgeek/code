    public static class CollectionsUtil {
        public static List<T> EnsureSize<T>(this List<T> list, int size) {
            if (list == null) throw new ArgumentNullException("list");
            if (size < 0) throw new ArgumentOutOfRangeException("size");
            int count = list.Count;
            if (count < size) {
                int capacity = list.Capacity;
                if (capacity < size)
                    list.Capacity = Math.max(size, capacity * 2);
                while (count < size) {
                    list.Add(default(T));
                    ++count;
                }
            }
            return list;
        }
    }
