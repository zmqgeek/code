    BinaryReader reader = new BinaryReader(stream);
    StructType o = new StructType();
    o.FileDate = Encoding.ASCII.GetString(reader.ReadBytes(8));
    o.FileTime = Encoding.ASCII.GetString(reader.ReadBytes(8));
    ...
    ...
    ...