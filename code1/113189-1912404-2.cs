    public static class TupleExtensions
    {
        public static void Unpack<T1, T2>(
            this Tuple<T1, T2> tuple,
            out T1 item1,
            out T2 item2)
        {
            item1 = tuple.Item1;
            item2 = tuple.Item2;
        }
    }
