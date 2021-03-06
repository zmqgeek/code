    public static class Serializer
    {
        public static byte[] GetBytes<T>(T structure, bool respectEndianness = true) where T : struct
        {
            var size = Marshal.SizeOf(structure);
            var bytes = new byte[size];
            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structure, ptr, true);
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            if (respectEndianness) RespectEndianness(typeof(T), bytes);  
            return bytes;
        }
        public static T FromBytes<T>(byte[] bytes, bool respectEndianness = true) where T : struct
        {
            var structure = new T();
            if (respectEndianness) RespectEndianness(typeof(T), bytes);    
            int size = Marshal.SizeOf(structure);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(bytes, 0, ptr, size);
            structure = (T)Marshal.PtrToStructure(ptr, structure.GetType());
            Marshal.FreeHGlobal(ptr);
            return structure;
        }
        private static void RespectEndianness(Type type, byte[] data, int offSet = 0)
        {
            var fields = type.GetFields()
                .Select(f => new
                {
                    Field = f,
                    Offset = Marshal.OffsetOf(type, f.Name).ToInt32(),
                }).ToList();
            foreach (var field in fields)
            {
                if (!field.Field.FieldType.IsArray)
                {
                    Array.Reverse(data, offSet + field.Offset, Marshal.SizeOf(field.Field.FieldType));
                }
                else
                {
                    var attr = field.Field.GetCustomAttributes(typeof(MarshalAsAttribute), false).FirstOrDefault();
                    var marshalAsAttribute = attr as MarshalAsAttribute;
                    if (marshalAsAttribute == null || marshalAsAttribute.SizeConst == 0)
                        throw new NotSupportedException("Array fields must be decorated with a MarshalAsAttribute with SizeConst specified.");
                    var arrayLength = marshalAsAttribute.SizeConst;
                    var elementType = field.Field.FieldType.GetElementType();
                    var elementSize = Marshal.SizeOf(elementType);
                    for (int i = field.Offset + offSet; i < elementSize * arrayLength; i += elementSize)
                    {
                        RespectEndianness(elementType, data, i);
                    }
                }
            }
        }
    }
