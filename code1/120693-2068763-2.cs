    public static string ToCsv<T>(this IEnumerable<T> source, string separator) {
        return String.Join(separator, source.Select(x => x.ToString()).ToArray());
    }
    public static string ToCsv<T>(this IEnumerable<T> source) {
        return source.ToCsv(", ");
    }
