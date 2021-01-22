    public static void AddRange<T>(this ICollection<T> destination,
                                   IEnumerable<T> source)
    {
        List<T> list = destination as List<T>;
        if (list != null)
        {
            list.AddRange(source);
        }
        else
        {
            foreach (T item in source)
            {
                destination.Add(item);
            }
        }
    }
