    public static class Extensions
    {
        public static T As<T>(this Consume c) where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), c.ToString(), false);
        }
    }
