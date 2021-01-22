    static void Main(string[] args)
    {
        MyEnum x = GetMaxValue<MyEnum>();
    }
    public static TEnum GetMaxValue<TEnum>() 
        where TEnum : IComparable, IConvertible, IFormattable
    {
        Type type = typeof(TEnum);
        if (!type.IsSubclassOf(typeof(Enum)))
            throw new
                InvalidCastException
                    ("Cannot cast '" + type.FullName + "' to System.Enum.");
        return (TEnum)Enum.ToObject(type, Enum.GetValues(type).Cast<int>().Last());
    }
    enum MyEnum
    {
        ValueOne,
        ValueTwo
    }
