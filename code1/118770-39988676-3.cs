    public byte[] FileToByteArray(string fileName)
    {
        byte[] fileData = null;
   
        using (FileStream fs = new File.OpenRead(fileName)) 
        { 
            using (BinaryReader binaryReader = new BinaryReader(fs))
            {
                fileData = binaryReader.ReadBytes((int)fs.Length); 
            }
        }
        return fileData;
    }
