    public class UniqueList<T> : List<T>
    {
        public new void Add(T obj)
        {
            if(!Contains(obj))
            {
                base.Add(obj);
            }
        }
    }
