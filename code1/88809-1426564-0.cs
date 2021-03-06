    public static class Enum<T>
    {
        public static T Parse(string value)
        {
            //Null check
            if(value == null) throw new ArgumentNullException("value");
            //Empty string check
            value = value.Trim();
            if(value.Length == 0) throw new ArgumentException("Must specify valid information for parsing in the string", "value");
            //Not enum check
            Type t = typeof(T);
            if(!t.IsEnum) throw new ArgumentException("Type provided must be an Enum", "TEnum");
            return (T)Enum.Parse(typeof(T), value);
        }
    }
