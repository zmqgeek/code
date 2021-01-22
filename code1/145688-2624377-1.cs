    [AttributeUsage(AttributeTargets.Field)]
    public class EndianAttribute : Attribute
    {
        public Endianness Endianness { get; private set; }
        public EndianAttribute(Endianness endienness)
        {
            this.Endianness = endianness;
        }
    }
    public enum Endianness
    {
        BigEndien,
        LittleEndien
    }
    private static void RespectEndianness(Type type, byte[] data)
    {
        var fields = type.GetFields().Where(f => f.IsDefined(typeof(EndianAttribute), false))
            .Select(f => new
            {
                Field = f,
                Attribute = (EndianAttribute)f.GetCustomAttributes(typeof(EndianAttribute), false)[0],
                Offset = Marshal.OffsetOf(type, f.Name).ToInt32()
            }).ToList();
        foreach (var field in fields)
        {
            if ((field.Attribute.Endianness == Endianness.BigEndian && BitConverter.IsLittleEndian) ||
                (field.Attribute.Endianness == Endianness.LittleEndian && !BitConverter.IsLittleEndian))
            {
                Array.Reverse(data, field.Offset, Marshal.SizeOf(field.Field.FieldType));
            }
        }
    }
    private static T BytesToStruct<T>(byte[] rawData) where T : struct
    {
        T result = default(T);
        RespectEndianness(typeof(T), rawData);     
        GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
        try
        {
            IntPtr rawDataPtr = handle.AddrOfPinnedObject();
            result = (T)Marshal.PtrToStructure(rawDataPtr, typeof(T));
        }
        finally
        {
            handle.Free();
        }        
        return result;
    }
    private static byte[] StructToBytes<T>(T data) where T : struct
    {
        byte[] rawData = new byte[Marshal.SizeOf(data)];
        GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
        try
        {
            IntPtr rawDataPtr = handle.AddrOfPinnedObject();
            Marshal.StructureToPtr(data, rawDataPtr, false);
        }
        finally
        {
            handle.Free();
        }
        RespectEndianness(typeof(T), rawData);     
        return rawData;
    }
