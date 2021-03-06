    public static class EnumExtension
    {
        public static bool IsSet<T>( this T input, T matchTo ) where T:enum
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Must be an enum", "input");
            }
            return (input & matchTo) != 0;
        }
    }
