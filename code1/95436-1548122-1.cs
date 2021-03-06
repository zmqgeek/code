    class DatabaseFile : IDisposable
    {
        private FileStream file;
        private static int RecordSize = 7;
        private static byte[] Deleted = new byte[] { 42 };
        private static byte[] Undeleted = new byte[] { 32 };
        public DatabaseFile(string filename)
        {
            file = new FileStream(filename, 
                FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        }
        public IEnumerable<Record> Locate(Predicate<Record> record)
        {
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position <= file.Length)
            {
                long offset = file.Position;
                byte[] buffer = new byte[DatabaseFile.RecordSize];
                file.Read(buffer, 0, DatabaseFile.RecordSize);
                Record current = Read(offset, buffer);
                if (record.Invoke(current))
                    yield return current;
            }
        }
        public void Append(Record record)
        {
            file.Seek(0, SeekOrigin.End);
            record.Deleted = false;
            record.Offset = file.Position;
            Write(record);
        }
        public void Delete(Record record)
        {
            file.Seek(record.Offset, SeekOrigin.Begin);
            record.Deleted = true;
            Write(record);
        }
        public void Update(Record record)
        {
            file.Seek(record.Offset, SeekOrigin.Begin);
            Write(record);
        }
        private Record Read(long offset, byte[] data)
        {
            byte[] ipAddress = new byte[4];
            Array.Copy(data, 1, ipAddress, 0, ipAddress.Length);
            return new Record
            {
                Offset = offset,
                Deleted = (data[0] == DatabaseFile.Deleted[0]),
                Address = new IPAddress(ipAddress), 
                Port = BitConverter.ToInt16(data, 5)
            };
        }
        private void Write(Record record)
        {
            file.Write(GetBytes(record), 0, DatabaseFile.RecordSize);
        }
        private byte[] GetBytes(Record record)
        {
            byte[] returnValue = new byte[DatabaseFile.RecordSize];
            Array.Copy(
                record.Deleted ? DatabaseFile.Deleted : DatabaseFile.Undeleted, 0, 
                returnValue, 0, 1);
            Array.Copy(record.Address.GetAddressBytes(), 0, 
                returnValue, 1, 4);
            Array.Copy(BitConverter.GetBytes(record.Port), 0, 
                returnValue, 5, 2);
            return returnValue;
        }
        public void Pack()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            if (file != null)
                file.Close();
        }
    }
